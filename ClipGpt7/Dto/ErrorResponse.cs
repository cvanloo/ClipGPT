using System.Text.Json.Serialization;

namespace ClipGpt7.Dto;

public sealed class ErrorContainer
{
	[JsonPropertyName("error")]
	public ErrorResponse? Error { get; init; }
}

public sealed class ErrorResponse
{
	[JsonPropertyName("message")]
	public string? Message { get; init; }
	
	[JsonPropertyName("type")]
	public string? Type { get; init; }
	
	[JsonPropertyName("param")]
	public string? Param { get; init; }
	
	[JsonPropertyName("code")]
	public string? Code { get; init; }
}