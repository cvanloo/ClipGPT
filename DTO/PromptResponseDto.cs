using System.Text.Json.Serialization;

namespace ClipGPT.DTO
{
	public class PromptResponseDto
	{
		public class Choices
		{
			[JsonPropertyName("text")]
			public string Text { get; set; }

			[JsonPropertyName("finish_reason")]
			public string FinishReason { get; set;  }
		}

		[JsonPropertyName("choices")]
		public Choices[] ResultChoices { get; set;  }
	}
}