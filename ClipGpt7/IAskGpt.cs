﻿namespace ClipGpt7;

/// <summary>
/// Implements methods to send requests to ChatGPT.
/// </summary>
public interface IAskGpt
{
	/// <summary>
	/// Ask ChatGPT something.
	/// </summary>
	/// <param name="prompt">The prompt to send to ChatGPT.</param>
	/// <returns>ChatGPT's answer.</returns>
	Task<string> Prompt(string prompt);

	/// <summary>
	/// Clear the chat context.
	/// </summary>
	void ClearContext();
}
