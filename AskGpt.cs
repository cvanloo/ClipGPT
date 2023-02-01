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
		private static string _apiKey;
		private static readonly HttpClient _client = new HttpClient
		{
			DefaultRequestHeaders =
			{
				Authorization = new AuthenticationHeaderValue("Bearer", _apiKey)
			}
		};

		public AskGpt(string key)
		{
			_apiKey = key;
		}

		public async Task<string> Prompt(string prompt)
		{
			var jsonBody = JsonSerializer.Serialize(new PromptRequestDto(ModelType.TextDavinci003, prompt));
			var contents = new StringContent(jsonBody, Encoding.UTF8, "application/json");
			var httpResponse = await _client.PostAsync(Resources.PromptRequestUri, contents);
			
			if (httpResponse.IsSuccessStatusCode == false)
			{
				return $"ERROR({httpResponse.StatusCode}): {httpResponse.ReasonPhrase}";
			}
			
			var responseJson = await httpResponse.Content.ReadAsStringAsync();
			var response = JsonSerializer.Deserialize<PromptResponseDto>(responseJson);
			return response.ResultChoices[0].Text;
		}
	}
}
