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

	private readonly List<PromptRequestChat.Message> _chatContext = new()
		{new PromptRequestChat.Message {Role = Role.System, Content = "You are a helpful assistant."}};
	
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
			}
		};
	}

	public async Task<string> PromptCompletion(string prompt)
	{
		var jsonBody = JsonSerializer.Serialize(new PromptRequestCompletion(_languageModel, prompt, _userSettings));
		var contents = new StringContent(jsonBody, Encoding.UTF8, "application/json");

		try
		{
			var httpResponse = await _client.PostAsync(Resources.RequestUriCompletion, contents);

			if (httpResponse.IsSuccessStatusCode == false)
			{
				var key = _userSettings.ApiKey.Length < 6
					? "Missing/Invalid Key!"
					: _userSettings.ApiKey[..5] + "***" +
					  _userSettings.ApiKey[^5..];
				return $"ERROR ({httpResponse.ReasonPhrase ?? "XXX"}), Request: {jsonBody}, Key: {key}";
			}

			var responseJson = await httpResponse.Content.ReadAsStringAsync();
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
	}
	
	public async Task<string> PromptChat(string prompt)
	{
		_chatContext.Add(new PromptRequestChat.Message {Role = Role.User, Content = prompt});
		var jsonBody = JsonSerializer.Serialize(new PromptRequestChat(_languageModel, _chatContext, _userSettings));
		var contents = new StringContent(jsonBody, Encoding.UTF8, "application/json");

		try
		{
			var httpResponse = await _client.PostAsync(Resources.RequestUriChat, contents);

			if (httpResponse.IsSuccessStatusCode == false)
			{
				var key = _userSettings.ApiKey.Length < 6
					? "Missing/Invalid Key!"
					: _userSettings.ApiKey[..5] + "***" +
					  _userSettings.ApiKey[^5..];
				return $"ERROR ({httpResponse.ReasonPhrase ?? "XXX"}), Request: {jsonBody}, Key: {key}";
			}

			var responseJson = await httpResponse.Content.ReadAsStringAsync();
			var response = JsonSerializer.Deserialize<PromptResponseChat>(responseJson);
			
			var responseMessage = response?.ResultChoices?[0].Response;
			if (responseMessage == null) return "ERROR Faulty Response Body";
			
			_chatContext.Add(new PromptRequestChat.Message
			{
				Role = responseMessage.Role,
				Content = responseMessage.Content
			});
			return responseMessage.Content;
		}
		catch (HttpRequestException ex)
		{
			return $"ERROR ({ex.StatusCode}): Request failed, Reason: {ex.Message}";
		}
		catch (TaskCanceledException ex)
		{
			return $"ERROR: Request timed out, Reason: {ex.Message}";
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
}
