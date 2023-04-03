﻿using System.ComponentModel;

namespace ClipGpt7;

partial class SettingsForm
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
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
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		buttonSaveAndClose = new Button();
		labelTitle = new Label();
		labelTemperature = new Label();
		trackBarTemperature = new TrackBar();
		labelMaxTokens = new Label();
		labelApiKey = new Label();
		textBoxApiKey = new TextBox();
		numericUpDownMaxTokens = new NumericUpDown();
		buttonReset = new Button();
		labelInfo = new Label();
		labelLanguageModel = new Label();
		labelCompletionMethod = new Label();
		comboBoxLanguageModel = new ComboBox();
		comboBoxCompletionMethod = new ComboBox();
		((ISupportInitialize)trackBarTemperature).BeginInit();
		((ISupportInitialize)numericUpDownMaxTokens).BeginInit();
		SuspendLayout();
		// 
		// buttonSaveAndClose
		// 
		buttonSaveAndClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		buttonSaveAndClose.Location = new Point(14, 356);
		buttonSaveAndClose.Margin = new Padding(4, 3, 4, 3);
		buttonSaveAndClose.Name = "buttonSaveAndClose";
		buttonSaveAndClose.Size = new Size(420, 51);
		buttonSaveAndClose.TabIndex = 0;
		buttonSaveAndClose.Text = "Save and Close";
		buttonSaveAndClose.UseVisualStyleBackColor = true;
		buttonSaveAndClose.Click += buttonClose_Click;
		// 
		// labelTitle
		// 
		labelTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		labelTitle.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
		labelTitle.Location = new Point(14, 10);
		labelTitle.Margin = new Padding(4, 0, 4, 0);
		labelTitle.Name = "labelTitle";
		labelTitle.Size = new Size(420, 36);
		labelTitle.TabIndex = 1;
		labelTitle.Text = "ClipGPT - Settings";
		labelTitle.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// labelTemperature
		// 
		labelTemperature.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
		labelTemperature.Location = new Point(33, 231);
		labelTemperature.Margin = new Padding(4, 0, 4, 0);
		labelTemperature.Name = "labelTemperature";
		labelTemperature.Size = new Size(120, 27);
		labelTemperature.TabIndex = 2;
		labelTemperature.Text = "Temperature";
		labelTemperature.TextAlign = ContentAlignment.MiddleRight;
		// 
		// trackBarTemperature
		// 
		trackBarTemperature.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		trackBarTemperature.Location = new Point(160, 231);
		trackBarTemperature.Margin = new Padding(4, 3, 4, 3);
		trackBarTemperature.Maximum = 20;
		trackBarTemperature.Minimum = 1;
		trackBarTemperature.Name = "trackBarTemperature";
		trackBarTemperature.Size = new Size(245, 45);
		trackBarTemperature.TabIndex = 3;
		trackBarTemperature.Value = 10;
		// 
		// labelMaxTokens
		// 
		labelMaxTokens.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
		labelMaxTokens.Location = new Point(39, 202);
		labelMaxTokens.Margin = new Padding(4, 0, 4, 0);
		labelMaxTokens.Name = "labelMaxTokens";
		labelMaxTokens.Size = new Size(114, 27);
		labelMaxTokens.TabIndex = 4;
		labelMaxTokens.Text = "MaxTokens";
		labelMaxTokens.TextAlign = ContentAlignment.MiddleRight;
		// 
		// labelApiKey
		// 
		labelApiKey.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
		labelApiKey.Location = new Point(39, 115);
		labelApiKey.Margin = new Padding(4, 0, 4, 0);
		labelApiKey.Name = "labelApiKey";
		labelApiKey.Size = new Size(114, 27);
		labelApiKey.TabIndex = 5;
		labelApiKey.Text = "ApiKey";
		labelApiKey.TextAlign = ContentAlignment.MiddleRight;
		// 
		// textBoxApiKey
		// 
		textBoxApiKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		textBoxApiKey.Location = new Point(160, 115);
		textBoxApiKey.Margin = new Padding(4, 3, 4, 3);
		textBoxApiKey.Name = "textBoxApiKey";
		textBoxApiKey.Size = new Size(244, 23);
		textBoxApiKey.TabIndex = 6;
		// 
		// numericUpDownMaxTokens
		// 
		numericUpDownMaxTokens.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		numericUpDownMaxTokens.Increment = new decimal(new int[] { 2, 0, 0, 0 });
		numericUpDownMaxTokens.Location = new Point(160, 202);
		numericUpDownMaxTokens.Margin = new Padding(4, 3, 4, 3);
		numericUpDownMaxTokens.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
		numericUpDownMaxTokens.Minimum = new decimal(new int[] { 16, 0, 0, 0 });
		numericUpDownMaxTokens.Name = "numericUpDownMaxTokens";
		numericUpDownMaxTokens.Size = new Size(245, 23);
		numericUpDownMaxTokens.TabIndex = 7;
		numericUpDownMaxTokens.ThousandsSeparator = true;
		numericUpDownMaxTokens.Value = new decimal(new int[] { 16, 0, 0, 0 });
		// 
		// buttonReset
		// 
		buttonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		buttonReset.Location = new Point(14, 310);
		buttonReset.Margin = new Padding(4, 3, 4, 3);
		buttonReset.Name = "buttonReset";
		buttonReset.Size = new Size(419, 33);
		buttonReset.TabIndex = 8;
		buttonReset.Text = "Restore Defaults";
		buttonReset.UseVisualStyleBackColor = true;
		buttonReset.Click += buttonReset_Click;
		// 
		// labelInfo
		// 
		labelInfo.ForeColor = Color.Red;
		labelInfo.Location = new Point(19, 60);
		labelInfo.Margin = new Padding(4, 0, 4, 0);
		labelInfo.Name = "labelInfo";
		labelInfo.Size = new Size(414, 22);
		labelInfo.TabIndex = 9;
		labelInfo.Text = "Clipboard listener is paused while this window is open.";
		labelInfo.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// labelLanguageModel
		// 
		labelLanguageModel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
		labelLanguageModel.Location = new Point(4, 171);
		labelLanguageModel.Margin = new Padding(4, 0, 4, 0);
		labelLanguageModel.Name = "labelLanguageModel";
		labelLanguageModel.Size = new Size(148, 27);
		labelLanguageModel.TabIndex = 10;
		labelLanguageModel.Text = "Language Model";
		labelLanguageModel.TextAlign = ContentAlignment.MiddleRight;
		// 
		// labelCompletionMethod
		// 
		labelCompletionMethod.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
		labelCompletionMethod.Location = new Point(4, 140);
		labelCompletionMethod.Margin = new Padding(4, 0, 4, 0);
		labelCompletionMethod.Name = "labelCompletionMethod";
		labelCompletionMethod.Size = new Size(148, 27);
		labelCompletionMethod.TabIndex = 11;
		labelCompletionMethod.Text = "Completion Method";
		labelCompletionMethod.TextAlign = ContentAlignment.MiddleRight;
		// 
		// comboBoxLanguageModel
		// 
		comboBoxLanguageModel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		comboBoxLanguageModel.FormattingEnabled = true;
		comboBoxLanguageModel.Location = new Point(159, 173);
		comboBoxLanguageModel.Name = "comboBoxLanguageModel";
		comboBoxLanguageModel.Size = new Size(244, 23);
		comboBoxLanguageModel.TabIndex = 12;
		// 
		// comboBoxCompletionMethod
		// 
		comboBoxCompletionMethod.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		comboBoxCompletionMethod.FormattingEnabled = true;
		comboBoxCompletionMethod.Location = new Point(159, 144);
		comboBoxCompletionMethod.Name = "comboBoxCompletionMethod";
		comboBoxCompletionMethod.Size = new Size(244, 23);
		comboBoxCompletionMethod.TabIndex = 13;
		comboBoxCompletionMethod.SelectedIndexChanged += comboBoxCompletionMethod_SelectedIndexChanged;
		// 
		// SettingsForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(448, 421);
		Controls.Add(comboBoxCompletionMethod);
		Controls.Add(comboBoxLanguageModel);
		Controls.Add(labelCompletionMethod);
		Controls.Add(labelLanguageModel);
		Controls.Add(labelInfo);
		Controls.Add(buttonReset);
		Controls.Add(numericUpDownMaxTokens);
		Controls.Add(textBoxApiKey);
		Controls.Add(labelApiKey);
		Controls.Add(labelMaxTokens);
		Controls.Add(trackBarTemperature);
		Controls.Add(labelTemperature);
		Controls.Add(labelTitle);
		Controls.Add(buttonSaveAndClose);
		FormBorderStyle = FormBorderStyle.FixedSingle;
		Margin = new Padding(4, 3, 4, 3);
		Name = "SettingsForm";
		Text = "Settings - ClipGPT";
		((ISupportInitialize)trackBarTemperature).EndInit();
		((ISupportInitialize)numericUpDownMaxTokens).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	private System.Windows.Forms.Label labelInfo;

	private System.Windows.Forms.Button buttonReset;

	private System.Windows.Forms.NumericUpDown numericUpDownMaxTokens;

	private System.Windows.Forms.Label labelMaxTokens;
	private System.Windows.Forms.Label labelApiKey;
	private System.Windows.Forms.TextBox textBoxApiKey;

	private System.Windows.Forms.TrackBar trackBarTemperature;

	private System.Windows.Forms.Label labelTemperature;

	private System.Windows.Forms.Label labelTitle;

	private System.Windows.Forms.Button buttonSaveAndClose;

	#endregion

	private Label labelLanguageModel;
	private Label labelCompletionMethod;
	private ComboBox comboBoxLanguageModel;
	private ComboBox comboBoxCompletionMethod;
}