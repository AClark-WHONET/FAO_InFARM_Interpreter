using AMR_Engine;
using System.ComponentModel;
using Microsoft.VisualBasic.FileIO;

namespace FAO_InFARM_Library
{
	public partial class InterpretationAndValidation
	{
		/// <summary>
		/// Generate interpretations for the data file.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		/// <exception cref="IOException"></exception>
		public static void ProcessDataFile(object? s, DoWorkEventArgs e)
		{
			BackgroundWorker worker;
			if (s != null)
				worker = (BackgroundWorker)s;
			else
			{
				e.Cancel = true;
				return;
			}

			ProcessArguments args;
			if (e.Argument != null)
				args = (ProcessArguments)e.Argument;
			else
			{
				e.Cancel = true;
				return;
			}

			// Reset progress from earlier runs.
			int lastReportedProgressPercentage = 0;
			worker.ReportProgress(lastReportedProgressPercentage);

			// Configure interpretation system.
			InterpretationConfiguration interpConfig =
				InterpretationConfiguration.DefaultConfiguration();

			if (!args.UseClinicalBreakpoints)
			{
				// Use ECOFFs instead of clinical breakpoints.
				interpConfig.PrioritizedBreakpointTypes.Clear();
				interpConfig.PrioritizedBreakpointTypes.Add(Breakpoint.BreakpointTypes.ECOFF);
			}

			// Get a line count without having to read the file into memory or loop.
			long totalLines =
				File.ReadLines(args.InputFileName).LongCount();

			Dictionary<string, int> headerLookup = new();

			List<int> FixedColumns = new();

			using TextFieldParser parser =
				new(args.InputFileName)
				{
					TextFieldType = FieldType.Delimited,
					HasFieldsEnclosedInQuotes = true
				};
			parser.SetDelimiters(Constants.InFARM_Delimiter);

			using StreamWriter writer =
				new(args.OutputFileName);

			Dictionary<string, HashSet<string>> currentIsolateGuidelinesAndMethods = new();

			List<string[]> isolateRows = new();
			while (!parser.EndOfData && !worker.CancellationPending)
			{
				if (parser.LineNumber == 1)
				{
					string[]? headerArray =
						parser.ReadFields() ?? throw new IOException("Invalid InFARM data file provided.");

					for (int x = 0; x < headerArray.Length; x++)
						headerLookup.Add(headerArray[x].ToString(), x);

					// Retain a list of indicies corresponding to the non-antibiotic fields.
					// These fields should be fixed for a given block of rows that represents a single isolate.
					// We will use differences in these columns to indicate the borders between isolates.
					FixedColumns = headerLookup.
						Where(h => !DataFields.AntibioticFields.Contains(h.Key)).
						Select(h => h.Value).ToList();

					// Write the header to the output file.
					writer.WriteLine(ToLine(headerArray));
				}
				else
				{
					// Process the rows for this isolate as one block (because of proxy interpretations, etc.).

					// Read the new row of input data.
					string[]? rowValues = parser.ReadFields();

					string? infarmGuideline = rowValues?[headerLookup[DataFields.GUIDELINE.InFARM_Name]];
					string? infarmTestMethod = rowValues?[headerLookup[DataFields.MET_AST.InFARM_Name]];

					if (isolateRows.Count == 0)
					{
						isolateRows.Add(rowValues);

						if (infarmGuideline != null && infarmTestMethod != null)
							currentIsolateGuidelinesAndMethods.Add(infarmGuideline, new HashSet<string> { infarmTestMethod });
					}

					else if (rowValues == null
							|| FixedColumns.Any(idx => rowValues?[idx] != isolateRows[0][idx])
							|| (infarmGuideline != null && infarmTestMethod != null && currentIsolateGuidelinesAndMethods.ContainsKey(infarmGuideline) && currentIsolateGuidelinesAndMethods[infarmGuideline].Contains(infarmTestMethod)))
					{
						// Any of the three above conditions indicates an isolate boundary.
						// The last condition ensures that duplicate isolates in sequence in the file are handled independently.
						if (isolateRows.Any())
							// Process the existing data.
							ProcessIsolateRows(writer, headerLookup, isolateRows, args, interpConfig);

						// Start a new isolate with the current rowValues.
						isolateRows.Clear();
						currentIsolateGuidelinesAndMethods.Clear();

						if (rowValues != null)
						{
							isolateRows.Add(rowValues);

							if (infarmGuideline != null && infarmTestMethod != null)
								currentIsolateGuidelinesAndMethods.Add(infarmGuideline, new HashSet<string> { infarmTestMethod });
						}
					}
					else
					{
						// This row is part of the same isolate.
						isolateRows.Add(rowValues);

						if (infarmGuideline != null && infarmTestMethod != null)
						{
							if (currentIsolateGuidelinesAndMethods.ContainsKey(infarmGuideline))
								currentIsolateGuidelinesAndMethods[infarmGuideline].Add(infarmTestMethod);
							else
								currentIsolateGuidelinesAndMethods.Add(infarmGuideline, new HashSet<string> { infarmTestMethod });
						}
					}

					if (parser.LineNumber == -1 && isolateRows.Any())
						ProcessIsolateRows(writer, headerLookup, isolateRows, args, interpConfig);
				}

				if (!worker.CancellationPending)
				{
					// Avoid reporting progress if the user has requested cancellation since the beginning of this iteration.
					int currentProgressPercentage;
					if (parser.LineNumber == -1)
						currentProgressPercentage = 100;
					else
						currentProgressPercentage = Convert.ToInt32((parser.LineNumber - 1L) * 100L / totalLines);

					if (currentProgressPercentage > lastReportedProgressPercentage)
					{
						lastReportedProgressPercentage = currentProgressPercentage;
						worker.ReportProgress(lastReportedProgressPercentage);
					}
				}
			}

			if (worker.CancellationPending)
				e.Cancel = true;
			else
				e.Result = totalLines;
		}

		#region private

		/// <summary>
		/// Given a block of rows representing one isolate, generate the interpretations and substitute the results back into the original data array.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="headerLookup"></param>
		/// <param name="isolateRows"></param>
		/// <param name="overwriteExistingInterpretations"></param>
		/// <param name="interpConfig"></param>
		private static void ProcessIsolateRows(StreamWriter writer, Dictionary<string, int> headerLookup, List<string[]> isolateRows,
			ProcessArguments processArgs, InterpretationConfiguration interpConfig)
		{
			bool differenceDiscovered = false;

			if (isolateRows is not null)
			{
				// Convert relevant fields from this InFARM row over to a structure and code set readable by the interpreter.
				// Relevant fields include: Organism (converted to WHONET codes), antibiotics (converted from InFARM format to WHONET).
				// Don't include antibiotics with interpretations if overwrite mode is not indicated.
				Dictionary<string, string>? convertedInterpretationInput =
				ConvertToInterpreterFormat(headerLookup, isolateRows, processArgs.OverwriteExistingInterpretations);

				// The conversion routine above won't populate the dictionary if the data row is missing
				// the organism code, guideline, or does not contain any quantitative results.
				// In that case, the row will be copied as-is to the output file.
				if (convertedInterpretationInput is not null)
				{
					// Interpret this row.
					Dictionary<string, string> results =
						new IsolateInterpretation(convertedInterpretationInput,
							convertedInterpretationInput.Keys.ToList(),
							interpConfig.EnabledExpertInterpretationRules,
							interpConfig.UserDefinedBreakpoints,
							guidelineYear: Convert.ToInt32(interpConfig.GuidelineYear),
							prioritizedBreakpointTypes: interpConfig.PrioritizedBreakpointTypes,
							prioritizedSitesOfInfection: interpConfig.PrioritizedSitesOfInfection).
							GetAllInterpretations();

					// Substitute the new interpretation values.
					foreach (KeyValuePair<string, string> result in results)
					{
						Tuple<string, HashSet<string>>? infarmGuidelineAndMethods =
							DataFields.GetInFARM_GuidelineAndMethod(result.Key);

						if (infarmGuidelineAndMethods != null)
						{
							string drugCode = result.Key.Substring(0, 3);
							string infarmFieldName = DataFields.GetInFARM_DrugName(drugCode, false);

							// Remove interpretation comments. Convert any SDD's to S's for the protocol.
							string cleanInterp = result.Value.
								Replace("*", string.Empty).
								Replace("!", string.Empty).
								Replace("SDD", "S");

							if (string.IsNullOrEmpty(cleanInterp) || cleanInterp == "?")
								cleanInterp = "NI";
							else
								cleanInterp = cleanInterp.Replace("?", string.Empty);

							// Loop over the rows for this isolate to find the one matching this guideline and test method.
							for (int x = 0; x < isolateRows.Count; x++)
							{
								if (isolateRows[x][headerLookup[DataFields.GUIDELINE.InFARM_Name]] == infarmGuidelineAndMethods.Item1
									&& infarmGuidelineAndMethods.Item2.Contains(isolateRows[x][headerLookup[DataFields.MET_AST.InFARM_Name]]))
								{
									if (processArgs.InterpretationMode)
									{
										isolateRows[x][headerLookup[infarmFieldName]] = cleanInterp;
										break;
									}
									else
									{
										// Validation mode.
										if (isolateRows[x][headerLookup[infarmFieldName]] != cleanInterp)
										{
											// Leave the original interpretation (or lack there of) and append ours.
											isolateRows[x][headerLookup[infarmFieldName]] += string.Format("<>{0}", cleanInterp);

											if (!differenceDiscovered)
												differenceDiscovered = true;

											break;
										}
									}
								}
							}
						}
					}
				}

				// Always write output in interpretation mode.
				// When validating, we will only write the isolate when an interpretation difference was discovered on any row for this isolate.
				if (processArgs.InterpretationMode || differenceDiscovered)
				{
					// Write the rows for this isolate.
					foreach (string[]? row in isolateRows)
						writer.WriteLine(ToLine(row));
				}
			}
			else if (processArgs.InterpretationMode)
				// Don't write empty lines unless in interpretation mode.
				writer.WriteLine();
		}

		/// <summary>
		/// Convert an array into the CSV format.
		/// Note that the InFARM protocol requires all fields, even empty ones, to be double quoted.
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		private static string ToLine(string[]? values)
		{
			if (values == null || values.Length == 0)
				return string.Empty;

			return string.Join(Constants.InFARM_Delimiter,
				values.Select(v =>
				{
					// Escape any quotes that are present in the real field value.
					v = v.Replace(Constants.DoubleQuote, Constants.TwoDoubleQuotes);

					// Quote every field per the InFARM requirements.
					v = Constants.DoubleQuote + v + Constants.DoubleQuote;
					return v;
				}));
		}

		/// <summary>
		/// Convert the InFARM data row to the data structure used by the interpretation library.
		/// </summary>
		/// <param name="headerLookup"></param>
		/// <param name="isolateRows"></param>
		/// <param name="overwriteExistingInterpretations"></param>
		/// <returns></returns>
		private static Dictionary<string, string>? ConvertToInterpreterFormat(Dictionary<string, int> headerLookup,
			List<string[]> isolateRows,
			bool overwriteExistingInterpretations)
		{
			if (isolateRows == null || isolateRows.Count == 0)
				return null;

			string? infarmOrganismCode = isolateRows[0][headerLookup[DataFields.MICROORG.InFARM_Name]];

			if (infarmOrganismCode == null || !OrganismLookup.InFARM_To_WHONET_Map.ContainsKey(infarmOrganismCode))
				return null;

			// Convert the organism code to the WHONET version.
			// This dictionary will contain only the relevant fields for interpretations.
			Dictionary<string, string> output = new()
			{
				[DataFields.MICROORG.WHONET_Name] = OrganismLookup.InFARM_To_WHONET_Map[infarmOrganismCode]
			};

			foreach (string[] row in isolateRows)
			{
				foreach (string drugCode in DataFields.InFARM_Antibiotics)
				{
					string infarmValueFieldName = DataFields.GetInFARM_DrugName(drugCode, true);
					string infarmInterpFieldName = DataFields.GetInFARM_DrugName(drugCode, false);

					string? infarmGuideline = row[headerLookup[DataFields.GUIDELINE.InFARM_Name]];
					string? infarmTestMethod = row[headerLookup[DataFields.MET_AST.InFARM_Name]];
					string? infarmQuantitativeResult = row[headerLookup[infarmValueFieldName]];
					string? infarmInterpretation = row[headerLookup[infarmInterpFieldName]];

					// No interpretation required for this drug when there is no quantitative result, or if there is an existing interpretation which we should not overwrite.
					if (string.IsNullOrWhiteSpace(infarmGuideline) || string.IsNullOrWhiteSpace(infarmTestMethod) || string.IsNullOrWhiteSpace(infarmQuantitativeResult)
						|| (!overwriteExistingInterpretations && !string.IsNullOrWhiteSpace(infarmInterpretation)))
						continue;

					string whonetFieldName =
						DataFields.GetWHONET_DrugCode(drugCode, infarmGuideline, infarmTestMethod);

					if (whonetFieldName is null)
						continue;

					// Add this drug result to the list of requested interpretations.
					if (!output.ContainsKey(whonetFieldName))
						output.Add(whonetFieldName, infarmQuantitativeResult);
					else if (string.IsNullOrWhiteSpace(output[whonetFieldName]) && !string.IsNullOrWhiteSpace(infarmQuantitativeResult))
						output[whonetFieldName] = infarmQuantitativeResult;
				}
			}

			if (output.Count > 1)
				return output;
			else
				return null;
		}

		#endregion
	}
}
