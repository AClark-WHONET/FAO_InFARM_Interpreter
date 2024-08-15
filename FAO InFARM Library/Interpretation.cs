using AMR_Engine;
using System.ComponentModel;
using Microsoft.VisualBasic.FileIO;

namespace FAO_InFARM_Library
{
	public class Interpretation
	{
		public class ProcessArguments
		{
			public string InputFileName { get; }
			public string OutputFileName { get; }
			public bool UseClinicalBreakpoints { get; }
			public bool OverwriteExistingInterpretations { get; }

			public ProcessArguments(string inputFileName_, string outputFileName_, bool useClinicalBreakpoints_, bool overwriteExistingInterpretations_)
			{
				InputFileName = inputFileName_;
				OutputFileName = outputFileName_;
				UseClinicalBreakpoints = useClinicalBreakpoints_;
				OverwriteExistingInterpretations = overwriteExistingInterpretations_;
			}
		}



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

			using TextFieldParser parser =
				new(args.InputFileName)
				{
					TextFieldType = FieldType.Delimited,
					HasFieldsEnclosedInQuotes = true
				};
			parser.SetDelimiters(Constants.InFARM_Delimiter);

			using StreamWriter writer = 
				new(args.OutputFileName);

			while (!parser.EndOfData)
			{
				if (parser.LineNumber == 0)
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

					// Convert relevant fields from this InFARM row over to a structure and code set readable by the interpreter.
					// Relevant fields include: Organism (converted to WHONET codes), antibiotics (converted from InFARM format to WHONET).
					// Don't include antibiotics with interpretations if overwrite mode is not indicated.
					Dictionary<string, string> convertedInterpretationInput = new();



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

					// Substitute the new interpretation values (optionally overwritting as specified).

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
		}

		#region private

		private static string ToLine(string[]? values)
		{
			if (values == null || values.Length == 0)
				return string.Empty;

			return string.Join(Constants.InFARM_Delimiter,
				values.Select(v => 
				{
					v = v.Replace(Constants.DoubleQuote, Constants.TwoDoubleQuotes);

					if (v.Contains(Constants.InFARM_Delimiter))
					{
						v = Constants.DoubleQuote + v + Constants.DoubleQuote;
					}

					return v;
				}));
		}

		#endregion
	}
}
