using System.Text.Json.Serialization;

namespace ClipGpt7.Dto;

public class PromptRequestCompletion
{
	[JsonPropertyName("model")]
	[JsonConverter(typeof(ModelTypeConverter))]
	public ModelType Model { get; init; }

	[JsonPropertyName("prompt")]
	public string Prompt { get; init; }

	[JsonPropertyName("max_tokens")]
	public int MaxTokens { get; init; }

	[JsonPropertyName("temperature")]
	public decimal Temperature { get; init; }

	public PromptRequestCompletion(ModelType model, string prompt, IUserSettings config)
	{
		Model = model;
		Prompt = prompt;
		MaxTokens = config.MaxTokens;
		Temperature = new decimal((double) config.Temperature / 10);
	}
}
