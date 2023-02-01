using System.ComponentModel;

namespace ClipGPT
{
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
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.labelTitle = new System.Windows.Forms.Label();
			this.labelTemperature = new System.Windows.Forms.Label();
			this.trackBarTemperature = new System.Windows.Forms.TrackBar();
			this.labelMaxTokens = new System.Windows.Forms.Label();
			this.labelApiKey = new System.Windows.Forms.Label();
			this.textBoxApiKey = new System.Windows.Forms.TextBox();
			this.numericUpDownMaxTokens = new System.Windows.Forms.NumericUpDown();
			this.buttonReset = new System.Windows.Forms.Button();
			this.labelInfo = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize) (this.trackBarTemperature)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDownMaxTokens)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSaveAndClose.Location = new System.Drawing.Point(12, 305);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(360, 44);
			this.buttonSaveAndClose.TabIndex = 0;
			this.buttonSaveAndClose.Text = "Save and Close";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// labelTitle
			// 
			this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.labelTitle.Location = new System.Drawing.Point(12, 9);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(360, 31);
			this.labelTitle.TabIndex = 1;
			this.labelTitle.Text = "ClipGPT - Settings";
			this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTemperature
			// 
			this.labelTemperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.labelTemperature.Location = new System.Drawing.Point(35, 97);
			this.labelTemperature.Name = "labelTemperature";
			this.labelTemperature.Size = new System.Drawing.Size(98, 23);
			this.labelTemperature.TabIndex = 2;
			this.labelTemperature.Text = "Temperature";
			this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// trackBarTemperature
			// 
			this.trackBarTemperature.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBarTemperature.Location = new System.Drawing.Point(139, 97);
			this.trackBarTemperature.Minimum = 1;
			this.trackBarTemperature.Name = "trackBarTemperature";
			this.trackBarTemperature.Size = new System.Drawing.Size(210, 45);
			this.trackBarTemperature.TabIndex = 3;
			this.trackBarTemperature.Value = 1;
			// 
			// labelMaxTokens
			// 
			this.labelMaxTokens.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.labelMaxTokens.Location = new System.Drawing.Point(35, 148);
			this.labelMaxTokens.Name = "labelMaxTokens";
			this.labelMaxTokens.Size = new System.Drawing.Size(98, 23);
			this.labelMaxTokens.TabIndex = 4;
			this.labelMaxTokens.Text = "MaxTokens";
			this.labelMaxTokens.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelApiKey
			// 
			this.labelApiKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.labelApiKey.Location = new System.Drawing.Point(35, 174);
			this.labelApiKey.Name = "labelApiKey";
			this.labelApiKey.Size = new System.Drawing.Size(98, 23);
			this.labelApiKey.TabIndex = 5;
			this.labelApiKey.Text = "ApiKey";
			this.labelApiKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBoxApiKey
			// 
			this.textBoxApiKey.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxApiKey.Location = new System.Drawing.Point(139, 174);
			this.textBoxApiKey.Name = "textBoxApiKey";
			this.textBoxApiKey.Size = new System.Drawing.Size(210, 20);
			this.textBoxApiKey.TabIndex = 6;
			// 
			// numericUpDownMaxTokens
			// 
			this.numericUpDownMaxTokens.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownMaxTokens.Increment = new decimal(new int[] {2, 0, 0, 0});
			this.numericUpDownMaxTokens.Location = new System.Drawing.Point(139, 148);
			this.numericUpDownMaxTokens.Maximum = new decimal(new int[] {2048, 0, 0, 0});
			this.numericUpDownMaxTokens.Minimum = new decimal(new int[] {16, 0, 0, 0});
			this.numericUpDownMaxTokens.Name = "numericUpDownMaxTokens";
			this.numericUpDownMaxTokens.Size = new System.Drawing.Size(210, 20);
			this.numericUpDownMaxTokens.TabIndex = 7;
			this.numericUpDownMaxTokens.ThousandsSeparator = true;
			this.numericUpDownMaxTokens.Value = new decimal(new int[] {16, 0, 0, 0});
			// 
			// buttonReset
			// 
			this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReset.Location = new System.Drawing.Point(12, 265);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(359, 29);
			this.buttonReset.TabIndex = 8;
			this.buttonReset.Text = "Restore Defaults";
			this.buttonReset.UseVisualStyleBackColor = true;
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// labelInfo
			// 
			this.labelInfo.ForeColor = System.Drawing.Color.Red;
			this.labelInfo.Location = new System.Drawing.Point(16, 52);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(355, 19);
			this.labelInfo.TabIndex = 9;
			this.labelInfo.Text = "Clipboard listener is paused while this window is open.";
			this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 361);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.buttonReset);
			this.Controls.Add(this.numericUpDownMaxTokens);
			this.Controls.Add(this.textBoxApiKey);
			this.Controls.Add(this.labelApiKey);
			this.Controls.Add(this.labelMaxTokens);
			this.Controls.Add(this.trackBarTemperature);
			this.Controls.Add(this.labelTemperature);
			this.Controls.Add(this.labelTitle);
			this.Controls.Add(this.buttonSaveAndClose);
			this.Name = "SettingsForm";
			this.Text = "SettingsForm";
			((System.ComponentModel.ISupportInitialize) (this.trackBarTemperature)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDownMaxTokens)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
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
	}
}