using System.Text.Json.Serialization;

namespace ClipGpt7.Dto;

public class PromptResponseCompletion
{
	public class Choices
	{
		[JsonPropertyName("text")]
		public string? Text { get; init; }

		[JsonPropertyName("finish_reason")]
		public string? FinishReason { get; init; }
	}

	[JsonPropertyName("choices")]
	public Choices[]? ResultChoices { get; init; }
}
