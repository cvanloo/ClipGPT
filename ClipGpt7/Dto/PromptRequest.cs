using System.Text.Json.Serialization;

namespace ClipGpt7.Dto;

/// <summary>
/// Imitates a string enum.
/// </summary>
public class ModelType
{
	private ModelType(string value) => Value = value;

	/// <summary>
	/// Get the name of the model.
	/// </summary>
	public string Value { get; }

	/// <summary>
	/// The text-davinci-003 model.
	/// </summary>
	public static ModelType TextDavinci003 => new("text-davinci-003");

	public override string ToString() => Value;
}

public class PromptRequest
{
	[JsonPropertyName("model")]
	public string Model { get; init; }

	[JsonPropertyName("prompt")]
	public string Prompt { get; init; }

	[JsonPropertyName("max_tokens")]
	public int MaxTokens { get; init; }

	[JsonPropertyName("temperature")]
	public decimal Temperature { get; init; }

	public PromptRequest(ModelType model, string prompt, IUserSettings config)
	{
		Model = model.Value;
		Prompt = prompt;
		MaxTokens = config.MaxTokens;
		Temperature = new decimal((double) config.Temperature / 10);
	}

	public PromptRequest(ModelType model, string prompt, int maxTokens, decimal temperature)
	{
		Model = model.Value;
		Prompt = prompt;
		MaxTokens = maxTokens;
		Temperature = temperature;
	}
}
