using System.ComponentModel;
using ClipGpt7.Enum;

namespace ClipGpt7;

public partial class SettingsForm : Form
{
	private readonly IUserSettings _userSettings;
	private readonly IAskGpt _askGpt;

	private List<string>? _chatBehaviours;
	private IBindingList? _chatBehavioursData;

	public SettingsForm(IUserSettings userSettings, IAskGpt askGpt)
	{
		InitializeComponent();
		_userSettings = userSettings;
		_askGpt = askGpt;
	}

	protected override void OnShown(EventArgs args) => Reload();

	private void buttonClose_Click(object sender, EventArgs e)
	{
		if (_chatBehaviours == null || comboBoxChatBehaviour.SelectedIndex < 0)
		{
			MessageBox.Show("Select a Chat Behaviour.", "Validation Error", MessageBoxButtons.OK,
				MessageBoxIcon.Warning);
			return;
		}

		_userSettings.Temperature = trackBarTemperature.Value;
		_userSettings.MaxTokens = (int)numericUpDownMaxTokens.Value;
		_userSettings.ApiKey = textBoxApiKey.Text;
		_userSettings.LanguageModel = ((ModelTypeCombo)comboBoxLanguageModel.SelectedItem).Id;
		_userSettings.CompletionMethod = ((CompletionTypeCombo)comboBoxCompletionMethod.SelectedItem).Id;
		_userSettings.SavedBehaviours = _chatBehaviours;
		_userSettings.SelectedBehaviour = comboBoxChatBehaviour.SelectedIndex;
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

		comboBoxCompletionMethod.DataSource = CompletionTypeCombo.DataSource;
		comboBoxCompletionMethod.SelectedItem = CompletionTypeCombo.DataSource
			.First(ct => ct.Id == _userSettings.CompletionMethod);

		var languageModelSource = ModelTypeCombo.DataSource(_userSettings.CompletionMethod);
		comboBoxLanguageModel.DataSource = languageModelSource;
		comboBoxLanguageModel.SelectedItem = languageModelSource.First(lm => lm.Id == _userSettings.LanguageModel);

		_chatBehaviours = new List<string>(_userSettings.SavedBehaviours);
		_chatBehavioursData = new BindingList<string>(_chatBehaviours);
		comboBoxChatBehaviour.DataSource = _chatBehavioursData;
		comboBoxChatBehaviour.SelectedIndex = _userSettings.SelectedBehaviour;
	}

	private void comboBoxCompletionMethod_SelectedIndexChanged(object sender, EventArgs e)
	{
		var selectedMethod = ((CompletionTypeCombo)comboBoxCompletionMethod.SelectedItem).Id;
		comboBoxLanguageModel.DataSource = ModelTypeCombo.DataSource(selectedMethod);
		comboBoxLanguageModel.SelectedIndex = 0;
	}

	private void comboBoxChatBehaviour_KeyDown(object sender, KeyEventArgs e)
	{
		if (_chatBehavioursData == null) return;

		switch (e.KeyCode)
		{
			case Keys.Enter:
				var addLine = comboBoxChatBehaviour.Text;
				if (!string.IsNullOrWhiteSpace(addLine))
				{
					_chatBehavioursData.Add(addLine);
					comboBoxChatBehaviour.SelectedIndex = _chatBehavioursData.Count - 1;
				}
				break;
			case Keys.Delete:
				if (comboBoxChatBehaviour.SelectedIndex > -1)
				{
					_chatBehavioursData.RemoveAt(comboBoxChatBehaviour.SelectedIndex);
				}
				break;
		}
	}

	private async void btnCheckApiKey_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(textBoxApiKey.Text))
		{
		retry_auth:
			var result = await _askGpt.VerifyAuthorization(textBoxApiKey.Text);
			if (result)
			{
				btnCheckApiKey.BackColor = Color.LimeGreen;
			}
			else
			{
				var choice = MessageBox.Show("Invalid API Key", "Unauthorized", MessageBoxButtons.RetryCancel,
					MessageBoxIcon.Error);
				if (choice == DialogResult.Retry) goto retry_auth;
				// if (choice == DialogResult.Cancel)  do nothing
			}
		}
	}

	private void textBoxApiKey_Click(object sender, EventArgs e)
	{
		btnCheckApiKey.BackColor = Color.Empty;
	}
}