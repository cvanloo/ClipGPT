using System;
using System.Windows.Forms;

namespace ClipGPT
{
	public partial class SettingsForm : Form
	{
		private readonly IUserSettings _userSettings;
		public SettingsForm(IUserSettings userSettings)
		{
			InitializeComponent();
			_userSettings = userSettings;
			Reload();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			_userSettings.Temperature = trackBarTemperature.Value;
			_userSettings.MaxTokens = (int) numericUpDownMaxTokens.Value; 
			_userSettings.ApiKey = textBoxApiKey.Text;
			_userSettings.Save();
			Close();
		}

		private void buttonReset_Click(object sender, EventArgs e)
		{
			_userSettings.Reset();
			Reload();
		}

		private void Reload()
		{
			trackBarTemperature.Value = _userSettings.Temperature;
			numericUpDownMaxTokens.Value = _userSettings.MaxTokens;
			textBoxApiKey.Text = _userSettings.ApiKey;
		}
	}
}