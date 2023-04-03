using System.Text.Json.Serialization;

namespace ClipGpt7.Dto;

public class PromptResponseChat
{
	public class Choices
	{
		public class Message
		{
			[JsonPropertyName("role")]
			[JsonConverter(typeof(RoleConverter))]
			public Role? Role { get; init; }
			
			[JsonPropertyName("content")]
			public string? Content { get; init; }
		}

		[JsonPropertyName("message")]
		public Message? Response { get; init; }

		[JsonPropertyName("finish_reason")]
		public string? FinishReason { get; init; }
	}

	[JsonPropertyName("choices")]
	public Choices[]? ResultChoices { get; init; }
}