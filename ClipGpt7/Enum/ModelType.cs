namespace ClipGpt7.Enum;

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

public sealed class ModelTypeCombo
{
	public ModelType Id { get; init; }
	public string Text { get; init; }

	/// <summary>
	/// WinForms uses the `ToString` method to determine the content of the combobox items.
	/// </summary>
	/// <returns>The text to display in the combo box item.</returns>
	public override string ToString() => Text;

	public static ModelTypeCombo[] DataSource => new[]
	{
		new ModelTypeCombo {Id = ModelType.TextDavinci003, Text = "text-davinci-003"},
		new ModelTypeCombo {Id = ModelType.Gpt35Turbo, Text = "gpt-3.5-turbo"}
	};
}