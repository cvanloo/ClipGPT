using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using ClipGPT.Properties;

namespace ClipGPT
{
	public class AskGPT
	{
		private static readonly HttpClient client = new HttpClient()
		{
			DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", Resources.ApiKey)}
		};
		private const string RequestUri = "https://api.openai.com/v1/completions";

		private class PromptDto
		{
			[JsonPropertyName("model")]
			public string Model { get; set; }
			[JsonPropertyName("prompt")]
			public string Prompt { get; set; }

			[JsonPropertyName("max_tokens")]
			public int MaxTokens { get; set; } = 1024;

			[JsonPropertyName("temperature")]
			public double Temperature { get; set; } = 0.9;

			public PromptDto(string model, string prompt)
			{
				Model = model;
				Prompt = prompt;
			}
		}

		private class PromptResponseDto
		{
			public class Choices
			{
				[JsonPropertyName("text")]
				public string Text { get; set; }
				[JsonPropertyName("finish_reason")]
				public string FinishReason { get; set; }
			}

			[JsonPropertyName("choices")]
			public Choices[] ResultChoices { get; set;  }
		}
		
		public async Task<string> AskPrompt(string prompt)
		{
			var jsonBody = JsonSerializer.Serialize(new PromptDto("text-davinci-003", prompt));
			var contents = new StringContent(jsonBody, Encoding.UTF8, "application/json");
			var httpResponse = await client.PostAsync(RequestUri, contents);
			var responseJson = await httpResponse.Content.ReadAsStringAsync();
			var response = JsonSerializer.Deserialize<PromptResponseDto>(responseJson);
			
			return response.ResultChoices[0].Text;
		}
	}
}