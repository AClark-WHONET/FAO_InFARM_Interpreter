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
			TabContainer = new TabControl();
			InterpretationTab = new TabPage();
			Interp_ECOFFsRadioButton = new RadioButton();
			Interp_ClinicalBreakpointsRadioButton = new RadioButton();
			Interp_OutputFileLabel = new Label();
			Interp_BrowseOutputFileButton = new Button();
			Interp_OutputFileTextBox = new TextBox();
			Interp_OverwriteExistingInterpretationsCheckbox = new CheckBox();
			Interp_BrowseForInputFileButton = new Button();
			Interp_InputFileTextBox = new TextBox();
			Interp_InputFileLabel = new Label();
			ValidationTab = new TabPage();
			Validation_BrowseForInputFileButton = new Button();
			Validation_InputFileTextBox = new TextBox();
			Validation_InputFileLabel = new Label();
			ExecuteButton = new Button();
			Cancel_Button = new Button();
			BackgroundProcessProgressBar = new ProgressBar();
			BackgroundProcessProgressPercentageLabel = new Label();
			TabContainer.SuspendLayout();
			InterpretationTab.SuspendLayout();
			ValidationTab.SuspendLayout();
			SuspendLayout();
			// 
			// TabContainer
			// 
			TabContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TabContainer.Controls.Add(InterpretationTab);
			TabContainer.Controls.Add(ValidationTab);
			TabContainer.Location = new Point(12, 12);
			TabContainer.Name = "TabContainer";
			TabContainer.SelectedIndex = 0;
			TabContainer.Size = new Size(776, 199);
			TabContainer.TabIndex = 0;
			// 
			// InterpretationTab
			// 
			InterpretationTab.Controls.Add(Interp_ECOFFsRadioButton);
			InterpretationTab.Controls.Add(Interp_ClinicalBreakpointsRadioButton);
			InterpretationTab.Controls.Add(Interp_OutputFileLabel);
			InterpretationTab.Controls.Add(Interp_BrowseOutputFileButton);
			InterpretationTab.Controls.Add(Interp_OutputFileTextBox);
			InterpretationTab.Controls.Add(Interp_OverwriteExistingInterpretationsCheckbox);
			InterpretationTab.Controls.Add(Interp_BrowseForInputFileButton);
			InterpretationTab.Controls.Add(Interp_InputFileTextBox);
			InterpretationTab.Controls.Add(Interp_InputFileLabel);
			InterpretationTab.Location = new Point(4, 24);
			InterpretationTab.Name = "InterpretationTab";
			InterpretationTab.Padding = new Padding(3);
			InterpretationTab.Size = new Size(768, 171);
			InterpretationTab.TabIndex = 0;
			InterpretationTab.Text = "Data file interpretation";
			InterpretationTab.UseVisualStyleBackColor = true;
			// 
			// Interp_ECOFFsRadioButton
			// 
			Interp_ECOFFsRadioButton.AutoSize = true;
			Interp_ECOFFsRadioButton.Location = new Point(6, 124);
			Interp_ECOFFsRadioButton.Name = "Interp_ECOFFsRadioButton";
			Interp_ECOFFsRadioButton.Size = new Size(87, 19);
			Interp_ECOFFsRadioButton.TabIndex = 7;
			Interp_ECOFFsRadioButton.Text = "Use ECOFFs";
			Interp_ECOFFsRadioButton.UseVisualStyleBackColor = true;
			// 
			// Interp_ClinicalBreakpointsRadioButton
			// 
			Interp_ClinicalBreakpointsRadioButton.AutoSize = true;
			Interp_ClinicalBreakpointsRadioButton.Checked = true;
			Interp_ClinicalBreakpointsRadioButton.Location = new Point(6, 99);
			Interp_ClinicalBreakpointsRadioButton.Name = "Interp_ClinicalBreakpointsRadioButton";
			Interp_ClinicalBreakpointsRadioButton.Size = new Size(149, 19);
			Interp_ClinicalBreakpointsRadioButton.TabIndex = 6;
			Interp_ClinicalBreakpointsRadioButton.TabStop = true;
			Interp_ClinicalBreakpointsRadioButton.Text = "Use clinical breakpoints";
			Interp_ClinicalBreakpointsRadioButton.UseVisualStyleBackColor = true;
			// 
			// Interp_OutputFileLabel
			// 
			Interp_OutputFileLabel.AutoSize = true;
			Interp_OutputFileLabel.Location = new Point(6, 52);
			Interp_OutputFileLabel.Name = "Interp_OutputFileLabel";
			Interp_OutputFileLabel.Size = new Size(154, 15);
			Interp_OutputFileLabel.TabIndex = 6;
			Interp_OutputFileLabel.Text = "Choose an output file name";
			// 
			// Interp_BrowseOutputFileButton
			// 
			Interp_BrowseOutputFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			Interp_BrowseOutputFileButton.Location = new Point(618, 70);
			Interp_BrowseOutputFileButton.Name = "Interp_BrowseOutputFileButton";
			Interp_BrowseOutputFileButton.Size = new Size(144, 23);
			Interp_BrowseOutputFileButton.TabIndex = 5;
			Interp_BrowseOutputFileButton.Text = "Browse";
			Interp_BrowseOutputFileButton.UseVisualStyleBackColor = true;
			Interp_BrowseOutputFileButton.Click += Interp_BrowseOutputFileButton_Click;
			// 
			// Interp_OutputFileTextBox
			// 
			Interp_OutputFileTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Interp_OutputFileTextBox.Location = new Point(6, 70);
			Interp_OutputFileTextBox.Name = "Interp_OutputFileTextBox";
			Interp_OutputFileTextBox.Size = new Size(606, 23);
			Interp_OutputFileTextBox.TabIndex = 4;
			// 
			// Interp_OverwriteExistingInterpretationsCheckbox
			// 
			Interp_OverwriteExistingInterpretationsCheckbox.AutoSize = true;
			Interp_OverwriteExistingInterpretationsCheckbox.Checked = true;
			Interp_OverwriteExistingInterpretationsCheckbox.CheckState = CheckState.Checked;
			Interp_OverwriteExistingInterpretationsCheckbox.Location = new Point(357, 100);
			Interp_OverwriteExistingInterpretationsCheckbox.Name = "Interp_OverwriteExistingInterpretationsCheckbox";
			Interp_OverwriteExistingInterpretationsCheckbox.Size = new Size(255, 19);
			Interp_OverwriteExistingInterpretationsCheckbox.TabIndex = 8;
			Interp_OverwriteExistingInterpretationsCheckbox.Text = "Overwrite existing test result interpretations";
			Interp_OverwriteExistingInterpretationsCheckbox.UseVisualStyleBackColor = true;
			// 
			// Interp_BrowseForInputFileButton
			// 
			Interp_BrowseForInputFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			Interp_BrowseForInputFileButton.Location = new Point(618, 26);
			Interp_BrowseForInputFileButton.Name = "Interp_BrowseForInputFileButton";
			Interp_BrowseForInputFileButton.Size = new Size(144, 23);
			Interp_BrowseForInputFileButton.TabIndex = 2;
			Interp_BrowseForInputFileButton.Text = "Browse";
			Interp_BrowseForInputFileButton.UseVisualStyleBackColor = true;
			Interp_BrowseForInputFileButton.Click += Interp_BrowseForInputFileButton_Click;
			// 
			// Interp_InputFileTextBox
			// 
			Interp_InputFileTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Interp_InputFileTextBox.Location = new Point(6, 26);
			Interp_InputFileTextBox.Name = "Interp_InputFileTextBox";
			Interp_InputFileTextBox.Size = new Size(606, 23);
			Interp_InputFileTextBox.TabIndex = 1;
			// 
			// Interp_InputFileLabel
			// 
			Interp_InputFileLabel.AutoSize = true;
			Interp_InputFileLabel.Location = new Point(6, 8);
			Interp_InputFileLabel.Name = "Interp_InputFileLabel";
			Interp_InputFileLabel.Size = new Size(191, 15);
			Interp_InputFileLabel.TabIndex = 0;
			Interp_InputFileLabel.Text = "Select an InFARM Model A data file";
			// 
			// ValidationTab
			// 
			ValidationTab.Controls.Add(Validation_BrowseForInputFileButton);
			ValidationTab.Controls.Add(Validation_InputFileTextBox);
			ValidationTab.Controls.Add(Validation_InputFileLabel);
			ValidationTab.Location = new Point(4, 24);
			ValidationTab.Name = "ValidationTab";
			ValidationTab.Padding = new Padding(3);
			ValidationTab.Size = new Size(768, 171);
			ValidationTab.TabIndex = 1;
			ValidationTab.Text = "Data file validation";
			ValidationTab.UseVisualStyleBackColor = true;
			// 
			// Validation_BrowseForInputFileButton
			// 
			Validation_BrowseForInputFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			Validation_BrowseForInputFileButton.Location = new Point(618, 26);
			Validation_BrowseForInputFileButton.Name = "Validation_BrowseForInputFileButton";
			Validation_BrowseForInputFileButton.Size = new Size(144, 23);
			Validation_BrowseForInputFileButton.TabIndex = 5;
			Validation_BrowseForInputFileButton.Text = "Browse";
			Validation_BrowseForInputFileButton.UseVisualStyleBackColor = true;
			// 
			// Validation_InputFileTextBox
			// 
			Validation_InputFileTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Validation_InputFileTextBox.Location = new Point(6, 26);
			Validation_InputFileTextBox.Name = "Validation_InputFileTextBox";
			Validation_InputFileTextBox.Size = new Size(606, 23);
			Validation_InputFileTextBox.TabIndex = 4;
			// 
			// Validation_InputFileLabel
			// 
			Validation_InputFileLabel.AutoSize = true;
			Validation_InputFileLabel.Location = new Point(6, 8);
			Validation_InputFileLabel.Name = "Validation_InputFileLabel";
			Validation_InputFileLabel.Size = new Size(191, 15);
			Validation_InputFileLabel.TabIndex = 3;
			Validation_InputFileLabel.Text = "Select an InFARM Model A data file";
			// 
			// ExecuteButton
			// 
			ExecuteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			ExecuteButton.Location = new Point(520, 217);
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
			Cancel_Button.Location = new Point(657, 217);
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
			BackgroundProcessProgressBar.Location = new Point(12, 217);
			BackgroundProcessProgressBar.Name = "BackgroundProcessProgressBar";
			BackgroundProcessProgressBar.Size = new Size(164, 23);
			BackgroundProcessProgressBar.TabIndex = 3;
			// 
			// BackgroundProcessProgressPercentageLabel
			// 
			BackgroundProcessProgressPercentageLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BackgroundProcessProgressPercentageLabel.AutoSize = true;
			BackgroundProcessProgressPercentageLabel.Location = new Point(182, 221);
			BackgroundProcessProgressPercentageLabel.Name = "BackgroundProcessProgressPercentageLabel";
			BackgroundProcessProgressPercentageLabel.Size = new Size(23, 15);
			BackgroundProcessProgressPercentageLabel.TabIndex = 4;
			BackgroundProcessProgressPercentageLabel.Text = "0%";
			// 
			// MainInterfaceForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 252);
			Controls.Add(BackgroundProcessProgressPercentageLabel);
			Controls.Add(BackgroundProcessProgressBar);
			Controls.Add(Cancel_Button);
			Controls.Add(ExecuteButton);
			Controls.Add(TabContainer);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "MainInterfaceForm";
			Text = "FAO InFARM Model A Data File Interpreter";
			TabContainer.ResumeLayout(false);
			InterpretationTab.ResumeLayout(false);
			InterpretationTab.PerformLayout();
			ValidationTab.ResumeLayout(false);
			ValidationTab.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TabControl TabContainer;
		private TabPage InterpretationTab;
		private TabPage ValidationTab;
		private Label Interp_InputFileLabel;
		private TextBox Interp_InputFileTextBox;
		private Button Interp_BrowseForInputFileButton;
		private CheckBox Interp_OverwriteExistingInterpretationsCheckbox;
		private Button ExecuteButton;
		private Button Cancel_Button;
		private Label Interp_OutputFileLabel;
		private Button Interp_BrowseOutputFileButton;
		private TextBox Interp_OutputFileTextBox;
		private Button Validation_BrowseForInputFileButton;
		private TextBox Validation_InputFileTextBox;
		private Label Validation_InputFileLabel;
		private ProgressBar BackgroundProcessProgressBar;
		private RadioButton Interp_ClinicalBreakpointsRadioButton;
		private RadioButton Interp_ECOFFsRadioButton;
		private Label BackgroundProcessProgressPercentageLabel;
	}
}
