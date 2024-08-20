namespace FAO_InFARM_Library
{
	public partial class Interpretation
	{
		/// <summary>
		/// Container for settings required to launch the InFARM interpretaion process.
		/// </summary>
		public class InterpretationProcessArguments
		{
			public string InputFileName { get; }
			public string OutputFileName { get; }
			public bool UseClinicalBreakpoints { get; }
			public bool OverwriteExistingInterpretations { get; }

			public InterpretationProcessArguments(string inputFileName_, string outputFileName_, bool useClinicalBreakpoints_, bool overwriteExistingInterpretations_)
			{
				InputFileName = inputFileName_;
				OutputFileName = outputFileName_;
				UseClinicalBreakpoints = useClinicalBreakpoints_;
				OverwriteExistingInterpretations = overwriteExistingInterpretations_;
			}
		}
	}
}
