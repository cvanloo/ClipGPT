using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClipGpt7.Dto;

/// <summary>
/// Imitates a string enum.
/// </summary>
public sealed class ModelType
{
	private ModelType(string value) => Value = value;

	public static ModelType FromEnum(Enum.ModelType type)
	{
		return type switch
		{
			Enum.ModelType.TextDavinci003 => TextDavinci003,
			Enum.ModelType.Gpt35Turbo => Gpt35Turbo,
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}

	/// <summary>
	/// Get the name of the model.
	/// </summary>
	public string Value { get; }

	/// <summary>
	/// Can do any language task with better quality, longer output, and consistent instruction-following than the curie,
	/// babbage, or ada models. Also supports inserting completions within text.
	/// (via https://platform.openai.com/docs/models/gpt-3-5)
	/// </summary>
	public static ModelType TextDavinci003 => new("text-davinci-003");
	
	/// <summary>
	/// Most capable GPT-3.5 model and optimized for chat at 1/10th the cost of text-davinci-003.
	/// Will be updated with our latest model iteration.
	/// (via https://platform.openai.com/docs/models/gpt-3-5)
	/// </summary>
	public static ModelType Gpt35Turbo => new("gpt-3.5-turbo");

	public override string ToString() => Value;
}

public sealed class ModelTypeConverter : JsonConverter<ModelType>
{
	public override ModelType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var type = reader.GetString();
		return type switch
		{
			"text-davinci-003" => ModelType.TextDavinci003,
			"gpt-3.5-turbo" => ModelType.Gpt35Turbo,
			_ => throw new ArgumentOutOfRangeException(nameof(reader), type, null)
		};
	}

	public override void Write(Utf8JsonWriter writer, ModelType value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.Value);
	}
}