﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ClipGpt7.Dto;
using ClipGpt7.Enum;
using ClipGpt7.Properties;
using ModelType = ClipGpt7.Dto.ModelType;

namespace ClipGpt7;

public sealed class AskGpt : IAskGpt
{
	private readonly IUserSettings _userSettings;
	private readonly HttpClient _client;
	private readonly List<PromptRequestChat.Message> _chatContext;
	
	private ModelType _languageModel;

	public AskGpt(IUserSettings userSettings)
	{
		_userSettings = userSettings;
		_client = new HttpClient
		{
			DefaultRequestHeaders =
			{
				Authorization = new AuthenticationHeaderValue("Bearer", _userSettings.ApiKey)
			}
		};
		_languageModel = ModelType.FromEnum(_userSettings.LanguageModel);
		{
			var content = _userSettings.SavedBehaviours[_userSettings.SelectedBehaviour];
			_chatContext = new List<PromptRequestChat.Message>
			{
				new() {Role = Role.System, Content = content}
			};
		}
		
		_userSettings.PropertyChanged += (_, args) =>
		{
			switch (args.PropertyName)
			{
				case nameof(_userSettings.ApiKey):
					_client.DefaultRequestHeaders.Authorization =
						new AuthenticationHeaderValue("Bearer", _userSettings.ApiKey);
					break;
				case nameof(_userSettings.LanguageModel):
					_languageModel = ModelType.FromEnum(_userSettings.LanguageModel);
					break;
				case nameof(_userSettings.SelectedBehaviour):
					var content = _userSettings.SavedBehaviours[_userSettings.SelectedBehaviour];
					_chatContext.Add(new PromptRequestChat.Message {Role = Role.System, Content = content});
					break;
			}
		};
	}

	/// <summary>
	/// Verify that authentication to the API works.
	/// </summary>
	/// <returns>True if authentication was successful.</returns>
	public async Task<bool> VerifyAuthorization(string? authToken = null)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, Resources.RequestUriModel)
		{
			Headers = {Authorization = new AuthenticationHeaderValue("Bearer", authToken ?? _userSettings.ApiKey)}
		};
		var httpResponse = await _client.SendAsync(request);
		return httpResponse.IsSuccessStatusCode;
	}

	/// <summary>
	/// Do a completion using the Completion method.
	/// </summary>
	/// <param name="prompt">Prompt to ask ChatGPT.</param>
	/// <returns>ChatGPT's answer.</returns>
	private async Task<string> PromptCompletion(string prompt)
	{
		var jsonBody = JsonSerializer.Serialize(new PromptRequestCompletion(_languageModel, prompt, _userSettings));
		var contents = new StringContent(jsonBody, Encoding.UTF8, "application/json");

		try
		{
			var httpResponse = await _client.PostAsync(Resources.RequestUriCompletion, contents);
			var responseJson = await httpResponse.Content.ReadAsStringAsync();

			if (httpResponse.IsSuccessStatusCode == false)
			{
				var key = _userSettings.ApiKey.Length < 6
					? "Missing/Invalid Key!"
					: _userSettings.ApiKey[..5] + "***" +
					  _userSettings.ApiKey[^5..];
				var errorResponse = JsonSerializer.Deserialize<ErrorContainer>(responseJson);
				return $"ERROR ({httpResponse.ReasonPhrase ?? "[Empty]"})\nRequest: {jsonBody}\nResponse: {errorResponse?.Error?.Message ?? "[Empty]"}\nKey: {key}";
			}

			var response = JsonSerializer.Deserialize<PromptResponseCompletion>(responseJson);
			return response?.ResultChoices?[0].Text ?? "ERROR Faulty Response Body";
		}
		catch (HttpRequestException ex)
		{
			return $"ERROR ({ex.StatusCode}): Request failed, Reason: {ex.Message}";
		}
		catch (TaskCanceledException ex)
		{
			return $"ERROR: Request timed out, Reason: {ex.Message}";
		}
		catch (Exception ex)
		{
			return $"UNEXPECTED ERROR: {ex.Message}";
		}
	}
	
	/// <summary>
	/// Do a completion using the Chat method.
	/// </summary>
	/// <param name="prompt">Prompt to ask ChatGPT.</param>
	/// <returns>ChatGPT's answer.</returns>
	private async Task<string> PromptChat(string prompt)
	{
	retry:
		_chatContext.Add(new PromptRequestChat.Message {Role = Role.User, Content = prompt});
		var jsonBody = JsonSerializer.Serialize(new PromptRequestChat(_languageModel, _chatContext, _userSettings));
		var contents = new StringContent(jsonBody, Encoding.UTF8, "application/json");

		try
		{
			var httpResponse = await _client.PostAsync(Resources.RequestUriChat, contents);
			var responseJson = await httpResponse.Content.ReadAsStringAsync();

			if (httpResponse.IsSuccessStatusCode == false)
			{
				var key = _userSettings.ApiKey.Length < 6
					? "Missing/Invalid Key!"
					: _userSettings.ApiKey[..5] + "***" +
					  _userSettings.ApiKey[^5..];
				var errorResponse = JsonSerializer.Deserialize<ErrorContainer>(responseJson);
				if (errorResponse?.Error?.Code == "context_length_exceeded")
				{
					var l = Math.Min(5, _chatContext.Count - 1);
					_chatContext.RemoveRange(1, l);
					goto retry;
				}
				return $"ERROR ({httpResponse.ReasonPhrase ?? "[Empty]"})\nRequest: {jsonBody}\nResponse: {errorResponse?.Error?.Message ?? "[Empty]"}\nKey: {key}";
			}

			var response = JsonSerializer.Deserialize<PromptResponseChat>(responseJson);

			var responseMessage = response?.ResultChoices?[0].Response;
			if (responseMessage == null) return "ERROR Faulty Response Body";

			_chatContext.Add(new PromptRequestChat.Message
			{
				Role = responseMessage.Role,
				Content = responseMessage.Content
			});
			return responseMessage.Content ?? "[Empty]";
		}
		catch (HttpRequestException ex)
		{
			return $"ERROR ({ex.StatusCode}): Request failed, Reason: {ex.Message}";
		}
		catch (TaskCanceledException ex)
		{
			return $"ERROR: Request timed out, Reason: {ex.Message}";
		}
		catch (Exception ex)
		{
			return $"UNEXPECTED ERROR: {ex.Message}";
		}
	}

	public async Task<string> Prompt(string prompt)
	{
		return _userSettings.CompletionMethod switch
		{
			CompletionType.Completion => await PromptCompletion(prompt),
			CompletionType.Chat => await PromptChat(prompt),
			_ => throw new ArgumentOutOfRangeException($"no completion method for {_userSettings.CompletionMethod}")
		};
	}

	public void ClearContext()
	{
		_chatContext.Clear();
		var content = _userSettings.SavedBehaviours[_userSettings.SelectedBehaviour];
		_chatContext.Add(new PromptRequestChat.Message {Role = Role.System, Content = content});
	}
}