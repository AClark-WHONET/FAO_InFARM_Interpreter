using AMR_Engine;
using System.ComponentModel;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;

namespace FAO_InFARM_Library
{
	public class Interpretation
	{
		public class ProcessArguments
		{
			public string InputFileName { get; }
			public string OutputFileName { get; }
			public bool OverwriteExistingInterpretations { get; }

			public ProcessArguments(string inputFileName_, string outputFileName_, bool overwriteExistingInterpretations_)
			{
				InputFileName = inputFileName_;
				OutputFileName = outputFileName_;
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
			worker.ReportProgress(0);

			// Load the input data file into memory.
			string[]? headers = null;
			List<Dictionary<string, string>> rowData =
				LoadInputFile(args.InputFileName, ref headers);

			// Configure interpretation system.
			InterpretationConfiguration interpConfig =
				InterpretationConfiguration.DefaultConfiguration();

			// Include ECOFFs for InFARM.
			interpConfig.PrioritizedBreakpointTypes.Add(Breakpoint.BreakpointTypes.ECOFF);

			int totalRows = rowData.Count;
			int lastReportedProgress = 0;
			for (int x = 0; x < totalRows; x++)
			{
				// Convert this InFARM row over to a structure and code set readable by the interpreter.

				// Write the data row.

				int currentProgress = Convert.ToInt32((x + 1) * 100 / totalRows);
				if (currentProgress > lastReportedProgress)
				{
					lastReportedProgress = currentProgress;
					worker.ReportProgress(lastReportedProgress);
				}
			}
		}

		#region private

		private static List<Dictionary<string, string>> LoadInputFile(string inputFileName, ref string[]? headers)
		{
			List<Dictionary<string, string>> rowValueSets = 
				new List<Dictionary<string, string>>();

			using TextFieldParser parser =
				new(inputFileName)
				{
					TextFieldType = FieldType.Delimited,
					HasFieldsEnclosedInQuotes = true
				};
			parser.SetDelimiters(",");

			headers = [];

			while (!parser.EndOfData)
			{
				if (headers?.Length == 0)
					headers = parser.ReadFields();

				else
				{
					// Process this data row.
					string[]? rowValues = parser.ReadFields();

					// Load the data row into the dictionary used by the interpreter.
					Dictionary<string, string> newRow = new();
					for(int x = 0; x < rowValues?.Length; x++)
						if (x < headers?.Length && !string.IsNullOrEmpty(headers[x]) && !string.IsNullOrWhiteSpace(rowValues[x]))
							newRow.Add(headers[x], rowValues[x].Trim());

					rowValueSets.Add(newRow);
				}
			}

			return rowValueSets;
		}

		#endregion
	}
}
