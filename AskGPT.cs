using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using ClipGPT.DTO;
using ClipGPT.Properties;

namespace ClipGPT
{
	public sealed class AskGpt : IAskGpt
	{
		private readonly IUserSettings _userSettings;
		private readonly HttpClient _client; 

		public AskGpt(IUserSettings userSettings)
		{
			_userSettings = userSettings;
			_userSettings.PropertyChanged += (sender, args) => 
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
			var jsonBody = JsonSerializer.Serialize(new PromptRequestDto(ModelType.TextDavinci003, prompt, _userSettings));
			var contents = new StringContent(jsonBody, Encoding.UTF8, "application/json");
			var httpResponse = await _client.PostAsync(Resources.PromptRequestUri, contents);
			
			if (httpResponse.IsSuccessStatusCode == false)
			{
				return $"ERROR ({httpResponse.ReasonPhrase}), Request: {jsonBody}, Key: {_userSettings.ApiKey}";
			}
			
			var responseJson = await httpResponse.Content.ReadAsStringAsync();
			var response = JsonSerializer.Deserialize<PromptResponseDto>(responseJson);
			return response.ResultChoices[0].Text;
		}
	}
}