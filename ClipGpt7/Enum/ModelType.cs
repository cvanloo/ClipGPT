﻿namespace ClipGpt7.Enum;

public enum ModelType
{
	/// <summary>
	/// Can do any language task with better quality, longer output, and consistent instruction-following than the curie,
	/// babbage, or ada models. Also supports inserting completions within text.
	/// (via https://platform.openai.com/docs/models/gpt-3-5)
	/// </summary>
	TextDavinci003,
	
	/// <summary>
	/// Most capable GPT-3.5 model and optimized for chat at 1/10th the cost of text-davinci-003.
	/// Will be updated with our latest model iteration.
	/// (via https://platform.openai.com/docs/models/gpt-3-5)
	/// </summary>
	Gpt35Turbo,
}

/// <summary>
/// Adapts a ModelType for a WinForms ComboBox.
/// </summary>
public sealed class ModelTypeCombo
{
	private ModelTypeCombo(ModelType id, string text)
	{
		Id = id;
		Text = text;
	}
	
	public ModelType Id { get; }
	public string Text { get; }

	/// <summary>
	/// WinForms uses the `ToString` method to determine the content of the combobox items.
	/// </summary>
	/// <returns>The text to display in the combo box item.</returns>
	public override string ToString() => Text;

	private static readonly ModelTypeCombo[] _completionDataSource =
	{
		new(ModelType.TextDavinci003, "text-davinci-003")
	};

	private static readonly ModelTypeCombo[] _chatDataSource =
	{
		new(ModelType.Gpt35Turbo, "gpt-3.5-turbo")
	};

	/// <summary>
	/// Returns the DataSource for a ComboBox.
	/// The available options depend on the completion method.
	/// </summary>
	/// <param name="type">The completion method to use.</param>
	/// <returns>DataSource for use in a ComboBox.</returns>
	/// <exception cref="ArgumentOutOfRangeException">An invalid completion type was passed.</exception>
	public static ModelTypeCombo[] DataSource(CompletionType type)
	{
		return type switch
		{
			CompletionType.Completion => _completionDataSource,
			CompletionType.Chat => _chatDataSource,
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
}