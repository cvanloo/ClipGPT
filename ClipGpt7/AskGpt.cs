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

		try
		{
			var httpResponse = await _client.PostAsync(Resources.PromptRequestUri, contents);

			if (httpResponse.IsSuccessStatusCode == false)
			{
				var key = _userSettings.ApiKey.Length < 6
					? "Missing/Invalid Key!"
					: _userSettings.ApiKey[..5] + "***" +
					  _userSettings.ApiKey[^5..];
				return $"ERROR ({httpResponse.ReasonPhrase ?? "XXX"}), Request: {jsonBody}, Key: {key}";
			}

			var responseJson = await httpResponse.Content.ReadAsStringAsync();
			var response = JsonSerializer.Deserialize<PromptResponse>(responseJson);
			return response?.ResultChoices?[0].Text ?? "ERROR Faulty Response Body";
		}
		catch (HttpRequestException ex)
		{
			return $"ERROR ({ex.StatusCode}): Request failed, Reason: {ex.Message}";
		}
		catch (TaskCanceledException ex)
		{
			return $"ERROR: Request timed out, Reason: {ex.Message}";
		}
	}
}
