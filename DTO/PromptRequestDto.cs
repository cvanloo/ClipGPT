using System.Text.Json.Serialization;

namespace ClipGPT.DTO
{
	/// <summary>
	/// Imitates a string enum.
	/// </summary>
	public class ModelType
	{
		private ModelType(string value)
		{
			Value = value;
		}
		
		/// <summary>
		/// Get the name of the model.
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// The text-davinci-003 model.
		/// </summary>
		public static ModelType TextDavinci003 => new ModelType("text-davinci-003");

		public override string ToString()
		{
			return Value;
		}
	}
	
	public class PromptRequestDto
	{
		[JsonPropertyName("model")]
		public string Model { get; set; }

		[JsonPropertyName("prompt")]
		public string Prompt { get; set; }

		[JsonPropertyName("max_tokens")]
		public int MaxTokens { get; set; }

		[JsonPropertyName("temperature")]
		public decimal Temperature { get; set; }
		
		public PromptRequestDto(ModelType model, string prompt, IUserSettings config)
		{
			Model = model.Value;
			Prompt = prompt;
			MaxTokens = config.MaxTokens;
			Temperature = new decimal(((double) config.Temperature) / 10);
		}

		public PromptRequestDto(ModelType model, string prompt, int maxTokens, decimal temperature)
		{
			Model = model.Value;
			Prompt = prompt;
			MaxTokens = maxTokens;
			Temperature = temperature;
		}
	}
}