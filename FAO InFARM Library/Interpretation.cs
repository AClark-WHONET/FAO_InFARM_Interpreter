using AMR_Engine;
using System.ComponentModel;
using Microsoft.VisualBasic.FileIO;

namespace FAO_InFARM_Library
{
	public partial class Interpretation
	{
		/// <summary>
		/// Generate interpretations for the data file.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		/// <exception cref="IOException"></exception>
		public static void InterpretDataFile(object? s, DoWorkEventArgs e)
		{
			BackgroundWorker worker;
			if (s != null)
				worker = (BackgroundWorker)s;
			else
			{
				e.Cancel = true;
				return;
			}

			InterpretationProcessArguments args;
			if (e.Argument != null)
				args = (InterpretationProcessArguments)e.Argument;
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

			using TextFieldParser parser =
				new(args.InputFileName)
				{
					TextFieldType = FieldType.Delimited,
					HasFieldsEnclosedInQuotes = true
				};
			parser.SetDelimiters(Constants.InFARM_Delimiter);

			using StreamWriter writer = 
				new(args.OutputFileName);

			while (!parser.EndOfData && !worker.CancellationPending)
			{
				if (parser.LineNumber == 1)
				{
					string[]? headerArray = 
						parser.ReadFields() ?? throw new IOException("Invalid InFARM data file provided.");

					for (int x = 0; x < headerArray.Length; x++)
						headerLookup.Add(headerArray[x].ToString(), x);

					// Write the header to the output file.
					writer.WriteLine(ToLine(headerArray));
				}
				else
				{
					// Process this data row.

					// Read the new row of input data.
					string[]? rowValues = parser.ReadFields();

					if (rowValues is not null)
					{
						// Convert relevant fields from this InFARM row over to a structure and code set readable by the interpreter.
						// Relevant fields include: Organism (converted to WHONET codes), antibiotics (converted from InFARM format to WHONET).
						// Don't include antibiotics with interpretations if overwrite mode is not indicated.
						Dictionary<string, string>? convertedInterpretationInput = 
						ConvertToInterpreterFormat(headerLookup, rowValues, args.OverwriteExistingInterpretations);

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
								string drugCode = result.Key.Substring(0, 3);
								string infarmFieldName = DataFields.GetInFARM_DrugName(drugCode, false);

								string cleanInterp = result.Value.
									Replace("*", string.Empty).
									Replace("!", string.Empty);

								if (string.IsNullOrEmpty(cleanInterp) || cleanInterp == "?")
									cleanInterp = "NI";

								cleanInterp = cleanInterp.Replace("?", string.Empty);

								rowValues[headerLookup[infarmFieldName]] = cleanInterp;
							}
						}
					}

					// Write the data row.
					writer.WriteLine(ToLine(rowValues));
				}

				int currentProgressPercentage = 
					Convert.ToInt32(parser.LineNumber * 100L / totalLines);

				if (currentProgressPercentage > lastReportedProgressPercentage)
				{
					lastReportedProgressPercentage = currentProgressPercentage;
					worker.ReportProgress(lastReportedProgressPercentage);
				}
			}

			if (worker.CancellationPending)
				e.Cancel = true;
			else
				e.Result = totalLines;
		}

		#region private

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

		private static Dictionary<string, string>? ConvertToInterpreterFormat(Dictionary<string, int> headerLookup, 
			string[]? rowValues, 
			bool overwriteExistingInterpretations)
		{
			string? infarmOrganismCode = rowValues?[headerLookup[DataFields.MICROORG.InFARM_Name]];

			if (infarmOrganismCode == null || !OrganismLookup.InFARM_To_WHONET_Map.ContainsKey(infarmOrganismCode))
				return null;

			// Convert the organism code to the WHONET version.
			// This dictionary will contain only the relevant fields for interpretations.
			Dictionary<string, string> output = new() { 
				[DataFields.MICROORG.WHONET_Name] = OrganismLookup.InFARM_To_WHONET_Map[infarmOrganismCode] 
			};

			foreach (string drugCode in DataFields.InFARM_Antibiotics)
			{
				string infarmValueFieldName = DataFields.GetInFARM_DrugName(drugCode, true);
				string infarmInterpFieldName = DataFields.GetInFARM_DrugName(drugCode, false);

				string? infarmGuideline = rowValues?[headerLookup[DataFields.GUIDELINE.InFARM_Name]];
				string? infarmTestMethod = rowValues?[headerLookup[DataFields.MET_AST.InFARM_Name]];
				string? infarmQuantitativeResult = rowValues?[headerLookup[infarmValueFieldName]];
				string? infarmInterpretation = rowValues?[headerLookup[infarmInterpFieldName]];
				
				// No interpretation required for this drug when there is no quantitative result, or if there is an existing interpretation which we should not overwrite.
				if (string.IsNullOrWhiteSpace(infarmGuideline) || string.IsNullOrWhiteSpace(infarmTestMethod) || string.IsNullOrWhiteSpace(infarmQuantitativeResult) 
					|| (!overwriteExistingInterpretations && !string.IsNullOrWhiteSpace(infarmInterpretation)))
					continue;

				string whonetFieldName = 
					DataFields.GetWHONET_DrugName(drugCode, infarmGuideline, infarmTestMethod);

				if (whonetFieldName is null)
					continue;

				// Add this drug result to the list of requested interpretations.
				output.Add(whonetFieldName, infarmQuantitativeResult);
			}

			if (output.Count() > 1)
				return output;
			else
				return null;
		}

		#endregion
	}
}
