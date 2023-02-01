using System.Text.Json.Serialization;

namespace ClipGPT.DTO
{
	public class ModelType
	{
		private ModelType(string value)
		{
			Value = value;
		}
		public string Value { get; private set; }

		public static ModelType TextDavinci003 => new ModelType("text-davinci-003");

		public override string ToString()
		{
			return Value;
		}
	}
	
	public class PromptRequestDto
	{
		[JsonPropertyName("model")]
		public ModelType Model { get; set; }

		[JsonPropertyName("prompt")]
		public string Prompt { get; set; }

		[JsonPropertyName("max_tokens")]
		public int MaxTokens { get; set; } = 1024;

		[JsonPropertyName("temperature")]
		public double Temperature { get; set; } = 0.9;

		public PromptRequestDto(ModelType model, string prompt)
		{
			Model = model;
			Prompt = prompt;
		}

		public PromptRequestDto(ModelType model, string prompt, int maxTokens, double temperature)
		{
			Model = model;
			Prompt = prompt;
			MaxTokens = maxTokens;
			Temperature = temperature;
		}
	}
}