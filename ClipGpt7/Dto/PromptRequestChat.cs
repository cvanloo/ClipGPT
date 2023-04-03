using System.Text.Json.Serialization;

namespace ClipGpt7.Dto;

public class PromptRequestChat
{
	public class Message
	{
		[JsonPropertyName("role")]
		[JsonConverter(typeof(RoleConverter))]
		public Role? Role { get; init; }
		
		[JsonPropertyName("content")]
		public string? Content { get; init; }
	}
	
	[JsonPropertyName("model")]
	[JsonConverter(typeof(ModelTypeConverter))]
	public ModelType Model { get; init; }
	
	[JsonPropertyName("messages")]
	public IEnumerable<Message> Messages { get; init; }
	
	[JsonPropertyName("max_tokens")]
	public int MaxTokens { get; init; }

	[JsonPropertyName("temperature")]
	public decimal Temperature { get; init; }
	
	public PromptRequestChat(ModelType model, IEnumerable<Message> context, IUserSettings config)
	{
		Model = model;
		Messages = context;
		MaxTokens = config.MaxTokens;
		Temperature = new decimal((double) config.Temperature / 10);
	}
}