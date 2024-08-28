namespace FAO_InFARM_Library
{
	public partial class InterpretationAndValidation
	{
		/// <summary>
		/// Container for settings required to launch the InFARM interpretaion process.
		/// </summary>
		public class ProcessArguments
		{
			public bool InterpretationMode { get; }
			public string InputFileName { get; }
			public string OutputFileName { get; }
			public bool UseClinicalBreakpoints { get; }
			public bool OverwriteExistingInterpretations { get; }

			public ProcessArguments(bool interpretationMode_, string inputFileName_, string outputFileName_, bool useClinicalBreakpoints_, bool overwriteExistingInterpretations_)
			{
				InterpretationMode = interpretationMode_;
				InputFileName = inputFileName_;
				OutputFileName = outputFileName_;
				UseClinicalBreakpoints = useClinicalBreakpoints_;
				OverwriteExistingInterpretations = overwriteExistingInterpretations_;
			}
		}
	}
}
