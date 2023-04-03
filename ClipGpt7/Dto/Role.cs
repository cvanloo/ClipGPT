using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClipGpt7.Dto;

public sealed class Role
{
	private Role(string value) => Value = value;
	
	public string Value { get; }

	public static Role System = new("system");
	public static Role User = new("user");
	public static Role Assistant = new("assistant");

	public override string ToString() => Value;
}

public sealed class RoleConverter : JsonConverter<Role>
{
	public override Role Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.GetString() switch
		{
			"system" => Role.System,
			"user" => Role.User,
			"assistant" => Role.Assistant,
			_ => throw new ArgumentOutOfRangeException($"unknown enum value {reader.GetString()}")
		};
	}

	public override void Write(Utf8JsonWriter writer, Role value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.Value);
	}
}