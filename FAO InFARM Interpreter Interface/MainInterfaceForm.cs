using System.ComponentModel;
using System.Diagnostics;

namespace FAO_InFARM_Interpreter_Interface
{
	public partial class MainInterfaceForm : Form
	{

		#region Private

		private BackgroundWorker? Worker;

		private readonly Stopwatch BackgroundWorkerTimer = new();

		#endregion

		#region Init

		public MainInterfaceForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Events

		#region General

		private void ExecuteButton_Click(object sender, EventArgs e)
		{
			if (ValidateUserOptions())
			{
				ToggleUI(false);

				// Launch the selected background process.
				try
				{
					Worker = new BackgroundWorker
					{
						WorkerReportsProgress = true,
						WorkerSupportsCancellation = true
					};

					Worker.RunWorkerCompleted += BackgroundProcessCompletedHandler;
					Worker.ProgressChanged += ProgressMeterEventHandler;

					if (TabContainer.SelectedTab == InterpretationTab)
					{
						// Interpretation mode.
						string inputFile = Interp_InputFileTextBox.Text.Trim();
						string outputFile = Interp_OutputFileTextBox.Text.Trim();
						bool useClinicalBreakpoints = Interp_ClinicalBreakpointsRadioButton.Checked;
						bool overwriteExistingInterpretations = Interp_OverwriteExistingInterpretationsCheckbox.Checked;

						FAO_InFARM_Library.Interpretation.InterpretationProcessArguments interpArgs =
							new(inputFile, outputFile, useClinicalBreakpoints, overwriteExistingInterpretations);

						Worker.DoWork += FAO_InFARM_Library.Interpretation.InterpretDataFile;
						
						BackgroundWorkerTimer.Restart();
						Worker.RunWorkerAsync(interpArgs);
					}
					else
					{
						// Validation mode.
					}
				}
				catch
				{
					// Ensure that user control returns if there is an exception during setup.
					ToggleUI(true);
					throw;
				}
			}
		}

		/// <summary>
		/// Inform the user of the process completion status and enable the UI.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		private void BackgroundProcessCompletedHandler(object? s, RunWorkerCompletedEventArgs e)
		{
			BackgroundWorkerTimer.Stop();

			if (e.Cancelled)
				BackgroundProcessProgressPercentageLabel.Text += " - " + "Processing cancelled.";

			else
			{
				if (e.Error != null)
					MessageBox.Show("Processing error: " + e.Error.Message);

				else
				{
					string formattedTime;
					if (BackgroundWorkerTimer.Elapsed.TotalMinutes > 60)
						formattedTime = BackgroundWorkerTimer.Elapsed.ToString(@"h\:mm\:ss");
					else if (BackgroundWorkerTimer.Elapsed.TotalMinutes >= 1)
						formattedTime = BackgroundWorkerTimer.Elapsed.ToString(@"mm\:ss");
					else
						formattedTime = string.Format("{0} seconds", BackgroundWorkerTimer.Elapsed.Seconds);

					MessageBox.Show("Rows processed: " + ((long?)e.Result).ToString() + Environment.NewLine + "Duration: " + formattedTime);
				}
			}

			// Enable the UI again now that the background work has completed.
			ToggleUI(true);
		}

		private void ProgressMeterEventHandler(object? s, ProgressChangedEventArgs e)
		{
			BackgroundProcessProgressBar.Value = e.ProgressPercentage;
			BackgroundProcessProgressPercentageLabel.Text = string.Format("{0}%", e.ProgressPercentage);
		}

		#endregion

		#region Interp

		private void Interp_BrowseForInputFileButton_Click(object sender, EventArgs e)
		{
			using OpenFileDialog openFile = new()
			{
				Filter = "InFARM Model A (*.csv)|*.csv"
			};

			if (openFile.ShowDialog() == DialogResult.OK)
				Interp_InputFileTextBox.Text = openFile.FileName;
		}

		private void Interp_BrowseOutputFileButton_Click(object sender, EventArgs e)
		{
			using SaveFileDialog saveFile = new()
			{
				Filter = "InFARM Model A (*.csv)|*.csv"
			};

			if (saveFile.ShowDialog() == DialogResult.OK)
				Interp_OutputFileTextBox.Text = saveFile.FileName;
		}

		#endregion

		#region Validation

		#endregion

		#endregion

		#region Library

		/// <summary>
		/// Enable or disable the UI.
		/// </summary>
		/// <param name="enabled"></param>
		private void ToggleUI(bool enabled)
		{
			ExecuteButton.Enabled = enabled;
			TabContainer.Enabled = enabled;
			Cancel_Button.Enabled = !enabled;
		}

		private bool ValidateUserOptions()
		{
			if (TabContainer.SelectedTab == InterpretationTab)
			{
				// Interpretation mode.

				// Make sure the input and output file names are present,
				// and that they don't match. (Overwritting the input file is not allowed.)
				if (string.IsNullOrWhiteSpace(Interp_InputFileTextBox.Text) || string.IsNullOrWhiteSpace(Interp_OutputFileTextBox.Text))
					MessageBox.Show("You must provide both input and output file names.");

				else if (Interp_InputFileTextBox.Text.Trim().Equals(Interp_OutputFileTextBox.Text.Trim(), StringComparison.InvariantCultureIgnoreCase))
					MessageBox.Show("You may not overwrite the input file. Please choose another output file name.");

				else
					return true;
			}
			else
			{
				// Validation mode.

				// No validation implemented yet.
				return true;
			}

			return false;
		}

		#endregion

		// Cancel the background task.
		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			if (Worker != null && Worker.IsBusy)
			{
				Cancel_Button.Enabled = false;
				Worker.CancelAsync();
			}
		}
	}
}
