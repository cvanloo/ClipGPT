using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ClipGpt7.Dto;
using ClipGpt7.Properties;

namespace ClipGpt7;

public sealed class AskGpt : IAskGpt
{
	private readonly IUserSettings _userSettings;
	private readonly HttpClient _client;

	public AskGpt(IUserSettings userSettings)
	{
		_userSettings = userSettings;
		_userSettings.PropertyChanged += (_, args) =>
		{
			if (_client != null && args.PropertyName == nameof(_userSettings.ApiKey))
			{
				_client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", _userSettings.ApiKey);
			}
		};

		_client = new HttpClient
		{
			DefaultRequestHeaders =
			{
				Authorization = new AuthenticationHeaderValue("Bearer", _userSettings.ApiKey)
			}
		};
	}

	public async Task<string> Prompt(string prompt)
	{
		var jsonBody = JsonSerializer.Serialize(new PromptRequest(ModelType.TextDavinci003, prompt, _userSettings));
		var contents = new StringContent(jsonBody, Encoding.UTF8, "application/json");
		
		var httpResponse = await _client.PostAsync(Resources.PromptRequestUri, contents);

		if (httpResponse.IsSuccessStatusCode == false)
		{
			return $"ERROR ({httpResponse.ReasonPhrase ?? "XXX"}), Request: {jsonBody}, Key: {_userSettings.ApiKey[..5]}***";
		}

		var responseJson = await httpResponse.Content.ReadAsStringAsync();
		var response = JsonSerializer.Deserialize<PromptResponse>(responseJson);
		return response?.ResultChoices?[0].Text ?? "ERROR Faulty Response Body";
	}
}
