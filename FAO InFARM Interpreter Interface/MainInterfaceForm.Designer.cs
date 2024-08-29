namespace FAO_InFARM_Interpreter_Interface
{
	partial class MainInterfaceForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainInterfaceForm));
			Interp_ECOFFsRadioButton = new RadioButton();
			Interp_ClinicalBreakpointsRadioButton = new RadioButton();
			Interp_OutputFileLabel = new Label();
			Interp_BrowseOutputFileButton = new Button();
			Interp_OutputFileTextBox = new TextBox();
			Interp_OverwriteExistingInterpretationsCheckbox = new CheckBox();
			Interp_BrowseForInputFileButton = new Button();
			Interp_InputFileTextBox = new TextBox();
			Interp_InputFileLabel = new Label();
			ExecuteButton = new Button();
			Cancel_Button = new Button();
			BackgroundProcessProgressBar = new ProgressBar();
			BackgroundProcessProgressPercentageLabel = new Label();
			BreakpointGroupBox = new GroupBox();
			UtilityModeGroupBox = new GroupBox();
			ValidateDataFileRadioButton = new RadioButton();
			InterpretDataFileRadioButton = new RadioButton();
			MainPanel = new Panel();
			BreakpointGroupBox.SuspendLayout();
			UtilityModeGroupBox.SuspendLayout();
			MainPanel.SuspendLayout();
			SuspendLayout();
			// 
			// Interp_ECOFFsRadioButton
			// 
			Interp_ECOFFsRadioButton.AutoSize = true;
			Interp_ECOFFsRadioButton.Location = new Point(6, 47);
			Interp_ECOFFsRadioButton.Name = "Interp_ECOFFsRadioButton";
			Interp_ECOFFsRadioButton.Size = new Size(87, 19);
			Interp_ECOFFsRadioButton.TabIndex = 1;
			Interp_ECOFFsRadioButton.Text = "Use ECOFFs";
			Interp_ECOFFsRadioButton.UseVisualStyleBackColor = true;
			// 
			// Interp_ClinicalBreakpointsRadioButton
			// 
			Interp_ClinicalBreakpointsRadioButton.AutoSize = true;
			Interp_ClinicalBreakpointsRadioButton.Checked = true;
			Interp_ClinicalBreakpointsRadioButton.Location = new Point(6, 22);
			Interp_ClinicalBreakpointsRadioButton.Name = "Interp_ClinicalBreakpointsRadioButton";
			Interp_ClinicalBreakpointsRadioButton.Size = new Size(149, 19);
			Interp_ClinicalBreakpointsRadioButton.TabIndex = 0;
			Interp_ClinicalBreakpointsRadioButton.TabStop = true;
			Interp_ClinicalBreakpointsRadioButton.Text = "Use clinical breakpoints";
			Interp_ClinicalBreakpointsRadioButton.UseVisualStyleBackColor = true;
			// 
			// Interp_OutputFileLabel
			// 
			Interp_OutputFileLabel.AutoSize = true;
			Interp_OutputFileLabel.Location = new Point(3, 140);
			Interp_OutputFileLabel.Name = "Interp_OutputFileLabel";
			Interp_OutputFileLabel.Size = new Size(154, 15);
			Interp_OutputFileLabel.TabIndex = 6;
			Interp_OutputFileLabel.Text = "Choose an output file name";
			// 
			// Interp_BrowseOutputFileButton
			// 
			Interp_BrowseOutputFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			Interp_BrowseOutputFileButton.Location = new Point(640, 158);
			Interp_BrowseOutputFileButton.Name = "Interp_BrowseOutputFileButton";
			Interp_BrowseOutputFileButton.Size = new Size(131, 23);
			Interp_BrowseOutputFileButton.TabIndex = 5;
			Interp_BrowseOutputFileButton.Text = "Browse";
			Interp_BrowseOutputFileButton.UseVisualStyleBackColor = true;
			Interp_BrowseOutputFileButton.Click += Interp_BrowseOutputFileButton_Click;
			// 
			// Interp_OutputFileTextBox
			// 
			Interp_OutputFileTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Interp_OutputFileTextBox.Location = new Point(3, 158);
			Interp_OutputFileTextBox.Name = "Interp_OutputFileTextBox";
			Interp_OutputFileTextBox.Size = new Size(631, 23);
			Interp_OutputFileTextBox.TabIndex = 4;
			// 
			// Interp_OverwriteExistingInterpretationsCheckbox
			// 
			Interp_OverwriteExistingInterpretationsCheckbox.AutoSize = true;
			Interp_OverwriteExistingInterpretationsCheckbox.Checked = true;
			Interp_OverwriteExistingInterpretationsCheckbox.CheckState = CheckState.Checked;
			Interp_OverwriteExistingInterpretationsCheckbox.Location = new Point(7, 47);
			Interp_OverwriteExistingInterpretationsCheckbox.Name = "Interp_OverwriteExistingInterpretationsCheckbox";
			Interp_OverwriteExistingInterpretationsCheckbox.Size = new Size(255, 19);
			Interp_OverwriteExistingInterpretationsCheckbox.TabIndex = 2;
			Interp_OverwriteExistingInterpretationsCheckbox.Text = "Overwrite existing test result interpretations";
			Interp_OverwriteExistingInterpretationsCheckbox.UseVisualStyleBackColor = true;
			// 
			// Interp_BrowseForInputFileButton
			// 
			Interp_BrowseForInputFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			Interp_BrowseForInputFileButton.Location = new Point(640, 114);
			Interp_BrowseForInputFileButton.Name = "Interp_BrowseForInputFileButton";
			Interp_BrowseForInputFileButton.Size = new Size(131, 23);
			Interp_BrowseForInputFileButton.TabIndex = 3;
			Interp_BrowseForInputFileButton.Text = "Browse";
			Interp_BrowseForInputFileButton.UseVisualStyleBackColor = true;
			Interp_BrowseForInputFileButton.Click += Interp_BrowseForInputFileButton_Click;
			// 
			// Interp_InputFileTextBox
			// 
			Interp_InputFileTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Interp_InputFileTextBox.Location = new Point(3, 114);
			Interp_InputFileTextBox.Name = "Interp_InputFileTextBox";
			Interp_InputFileTextBox.Size = new Size(631, 23);
			Interp_InputFileTextBox.TabIndex = 2;
			// 
			// Interp_InputFileLabel
			// 
			Interp_InputFileLabel.AutoSize = true;
			Interp_InputFileLabel.Location = new Point(3, 96);
			Interp_InputFileLabel.Name = "Interp_InputFileLabel";
			Interp_InputFileLabel.Size = new Size(191, 15);
			Interp_InputFileLabel.TabIndex = 0;
			Interp_InputFileLabel.Text = "Select an InFARM Model A data file";
			// 
			// ExecuteButton
			// 
			ExecuteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			ExecuteButton.Location = new Point(515, 208);
			ExecuteButton.Name = "ExecuteButton";
			ExecuteButton.Size = new Size(131, 23);
			ExecuteButton.TabIndex = 1;
			ExecuteButton.Text = "Execute";
			ExecuteButton.UseVisualStyleBackColor = true;
			ExecuteButton.Click += ExecuteButton_Click;
			// 
			// Cancel_Button
			// 
			Cancel_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			Cancel_Button.Enabled = false;
			Cancel_Button.Location = new Point(652, 208);
			Cancel_Button.Name = "Cancel_Button";
			Cancel_Button.Size = new Size(131, 23);
			Cancel_Button.TabIndex = 2;
			Cancel_Button.Text = "Cancel";
			Cancel_Button.UseVisualStyleBackColor = true;
			Cancel_Button.Click += Cancel_Button_Click;
			// 
			// BackgroundProcessProgressBar
			// 
			BackgroundProcessProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BackgroundProcessProgressBar.Location = new Point(12, 208);
			BackgroundProcessProgressBar.Name = "BackgroundProcessProgressBar";
			BackgroundProcessProgressBar.Size = new Size(164, 23);
			BackgroundProcessProgressBar.TabIndex = 3;
			// 
			// BackgroundProcessProgressPercentageLabel
			// 
			BackgroundProcessProgressPercentageLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BackgroundProcessProgressPercentageLabel.AutoSize = true;
			BackgroundProcessProgressPercentageLabel.Location = new Point(182, 212);
			BackgroundProcessProgressPercentageLabel.Name = "BackgroundProcessProgressPercentageLabel";
			BackgroundProcessProgressPercentageLabel.Size = new Size(23, 15);
			BackgroundProcessProgressPercentageLabel.TabIndex = 4;
			BackgroundProcessProgressPercentageLabel.Text = "0%";
			// 
			// BreakpointGroupBox
			// 
			BreakpointGroupBox.Controls.Add(Interp_ClinicalBreakpointsRadioButton);
			BreakpointGroupBox.Controls.Add(Interp_ECOFFsRadioButton);
			BreakpointGroupBox.Location = new Point(498, 3);
			BreakpointGroupBox.Name = "BreakpointGroupBox";
			BreakpointGroupBox.Size = new Size(273, 78);
			BreakpointGroupBox.TabIndex = 1;
			BreakpointGroupBox.TabStop = false;
			BreakpointGroupBox.Text = "Breakpoints";
			// 
			// UtilityModeGroupBox
			// 
			UtilityModeGroupBox.Controls.Add(ValidateDataFileRadioButton);
			UtilityModeGroupBox.Controls.Add(InterpretDataFileRadioButton);
			UtilityModeGroupBox.Controls.Add(Interp_OverwriteExistingInterpretationsCheckbox);
			UtilityModeGroupBox.Location = new Point(3, 3);
			UtilityModeGroupBox.Name = "UtilityModeGroupBox";
			UtilityModeGroupBox.Size = new Size(489, 78);
			UtilityModeGroupBox.TabIndex = 0;
			UtilityModeGroupBox.TabStop = false;
			UtilityModeGroupBox.Text = "Mode";
			// 
			// ValidateDataFileRadioButton
			// 
			ValidateDataFileRadioButton.AutoSize = true;
			ValidateDataFileRadioButton.Location = new Point(204, 22);
			ValidateDataFileRadioButton.Name = "ValidateDataFileRadioButton";
			ValidateDataFileRadioButton.Size = new Size(190, 19);
			ValidateDataFileRadioButton.TabIndex = 1;
			ValidateDataFileRadioButton.TabStop = true;
			ValidateDataFileRadioButton.Text = "Validate existing interpretations";
			ValidateDataFileRadioButton.UseVisualStyleBackColor = true;
			ValidateDataFileRadioButton.CheckedChanged += ValidateDataFileRadioButton_CheckedChanged;
			// 
			// InterpretDataFileRadioButton
			// 
			InterpretDataFileRadioButton.AutoSize = true;
			InterpretDataFileRadioButton.Checked = true;
			InterpretDataFileRadioButton.Location = new Point(7, 22);
			InterpretDataFileRadioButton.Name = "InterpretDataFileRadioButton";
			InterpretDataFileRadioButton.Size = new Size(115, 19);
			InterpretDataFileRadioButton.TabIndex = 0;
			InterpretDataFileRadioButton.TabStop = true;
			InterpretDataFileRadioButton.Text = "Interpret data file";
			InterpretDataFileRadioButton.UseVisualStyleBackColor = true;
			// 
			// MainPanel
			// 
			MainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			MainPanel.Controls.Add(Interp_InputFileLabel);
			MainPanel.Controls.Add(BreakpointGroupBox);
			MainPanel.Controls.Add(UtilityModeGroupBox);
			MainPanel.Controls.Add(Interp_InputFileTextBox);
			MainPanel.Controls.Add(Interp_BrowseForInputFileButton);
			MainPanel.Controls.Add(Interp_OutputFileTextBox);
			MainPanel.Controls.Add(Interp_BrowseOutputFileButton);
			MainPanel.Controls.Add(Interp_OutputFileLabel);
			MainPanel.Location = new Point(12, 12);
			MainPanel.Name = "MainPanel";
			MainPanel.Size = new Size(780, 190);
			MainPanel.TabIndex = 0;
			// 
			// MainInterfaceForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(795, 243);
			Controls.Add(MainPanel);
			Controls.Add(BackgroundProcessProgressPercentageLabel);
			Controls.Add(BackgroundProcessProgressBar);
			Controls.Add(Cancel_Button);
			Controls.Add(ExecuteButton);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MinimumSize = new Size(811, 282);
			Name = "MainInterfaceForm";
			Text = "FAO InFARM Model A Data File Utility";
			BreakpointGroupBox.ResumeLayout(false);
			BreakpointGroupBox.PerformLayout();
			UtilityModeGroupBox.ResumeLayout(false);
			UtilityModeGroupBox.PerformLayout();
			MainPanel.ResumeLayout(false);
			MainPanel.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Label Interp_InputFileLabel;
		private TextBox Interp_InputFileTextBox;
		private Button Interp_BrowseForInputFileButton;
		private CheckBox Interp_OverwriteExistingInterpretationsCheckbox;
		private Button ExecuteButton;
		private Button Cancel_Button;
		private Label Interp_OutputFileLabel;
		private Button Interp_BrowseOutputFileButton;
		private TextBox Interp_OutputFileTextBox;
		private ProgressBar BackgroundProcessProgressBar;
		private RadioButton Interp_ClinicalBreakpointsRadioButton;
		private RadioButton Interp_ECOFFsRadioButton;
		private Label BackgroundProcessProgressPercentageLabel;
		private GroupBox BreakpointGroupBox;
		private GroupBox UtilityModeGroupBox;
		private RadioButton InterpretDataFileRadioButton;
		private RadioButton ValidateDataFileRadioButton;
		private Panel MainPanel;
	}
}
