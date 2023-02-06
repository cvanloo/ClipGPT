using System;
using System.Windows.Forms;

namespace ClipGPT
{
	public partial class SettingsForm : Form
	{
		private readonly ApplicationConfig _userSettings;
		public SettingsForm(ApplicationConfig userSettings)
		{
			InitializeComponent();
			_userSettings = userSettings;
			// FIXME: Sometimes windows form seems to attempt to set a value of '0' which is outside of the valid range,
			//   therefore throwing an exception.
			//   It's a mystery to me, why it would do that.
			trackBarTemperature.DataBindings.Add("Value", _userSettings, "Temperature");
			numericUpDownMaxTokens.DataBindings.Add("Value", _userSettings, "MaxTokens");
			textBoxApiKey.DataBindings.Add("Text", _userSettings, "ApiKey");
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			_userSettings.Save();
			Close();
		}

		private void buttonReset_Click(object sender, EventArgs e)
		{
			_userSettings.Reset();
		}
	}
}