using System.ComponentModel;
using ClipGpt7.Enum;

namespace ClipGpt7;

public partial class SettingsForm : Form
{
	private readonly IUserSettings _userSettings;
	private IBindingList? _chatBehaviourList;

	public SettingsForm(IUserSettings userSettings)
	{
		InitializeComponent();
		_userSettings = userSettings;
	}

	protected override void OnShown(EventArgs args) => Reload();

	private void buttonClose_Click(object sender, EventArgs e)
	{
		if (comboBoxChatBehaviour.SelectedIndex < 0)
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
		// _userSettings.SavedBehaviours is directly bound to the DataSource and therefore updates automatically.
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

		_chatBehaviourList = new BindingList<string>(_userSettings.SavedBehaviours);
		comboBoxChatBehaviour.DataSource = _chatBehaviourList;
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
		if (_chatBehaviourList == null) return;
		
		switch (e.KeyCode)
		{
			case Keys.Enter:
				var addLine = comboBoxChatBehaviour.Text;
				if (!string.IsNullOrWhiteSpace(addLine))
				{
					_chatBehaviourList.Add(addLine);
					comboBoxChatBehaviour.SelectedIndex = _chatBehaviourList.Count - 1;
				}
				break;
			case Keys.Delete:
				if (comboBoxChatBehaviour.SelectedIndex > -1)
				{
					_chatBehaviourList.RemoveAt(comboBoxChatBehaviour.SelectedIndex);
				}
				break;
		}
	}
}