using ClipGpt7.Enum;

namespace ClipGpt7;

public partial class SettingsForm : Form
{
	private readonly IUserSettings _userSettings;

	public SettingsForm(IUserSettings userSettings)
	{
		InitializeComponent();
		_userSettings = userSettings;
	}

	protected override void OnShown(EventArgs args) => Reload();

	private void buttonClose_Click(object sender, EventArgs e)
	{
		_userSettings.Temperature = trackBarTemperature.Value;
		_userSettings.MaxTokens = (int)numericUpDownMaxTokens.Value;
		_userSettings.ApiKey = textBoxApiKey.Text;
		_userSettings.LanguageModel = ((ModelTypeCombo)comboBoxLanguageModel.SelectedItem).Id;
		_userSettings.CompletionMethod = ((CompletionTypeCombo)comboBoxCompletionMethod.SelectedItem).Id;
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
		//comboBoxCompletionMethod.SelectedIndex = (int)_userSettings.CompletionMethod;
		
		var languageModelSource = ModelTypeCombo.DataSource(_userSettings.CompletionMethod);
		comboBoxLanguageModel.DataSource = languageModelSource;
		comboBoxLanguageModel.SelectedItem = languageModelSource.First(lm => lm.Id == _userSettings.LanguageModel);
	}

	private void comboBoxCompletionMethod_SelectedIndexChanged(object sender, EventArgs e)
	{
		var selectedMethod = ((CompletionTypeCombo) comboBoxCompletionMethod.SelectedItem).Id;
		comboBoxLanguageModel.DataSource = ModelTypeCombo.DataSource(selectedMethod);
		comboBoxLanguageModel.SelectedIndex = 0;
	}
}