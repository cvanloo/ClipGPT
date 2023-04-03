namespace ClipGpt7.Enum;

public enum CompletionType
{
	Completion,
	Chat
}

public sealed class CompletionTypeCombo
{
	private CompletionTypeCombo(CompletionType id, string text)
	{
		Id = id;
		Text = text;
	}

	public CompletionType Id { get; }
	public string Text { get; }

	/// <summary>
	/// WinForms uses the `ToString` method to determine the content of the combobox items.
	/// </summary>
	/// <returns>The text to display in the combo box item.</returns>
	public override string ToString() => Text;
	
	public static CompletionTypeCombo[] DataSource { get; } = {
		new(CompletionType.Completion, "Completion (no context)"),
		new(CompletionType.Chat, "Chat (with context)")
	};
}