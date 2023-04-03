namespace ClipGpt7.Enum;

public enum CompletionType
{
	Completion,
	Chat
}

public sealed class CompletionTypeCombo
{
	public CompletionType Id { get; init; }
	public string Text { get; init; }

	/// <summary>
	/// WinForms uses the `ToString` method to determine the content of the combobox items.
	/// </summary>
	/// <returns>The text to display in the combo box item.</returns>
	public override string ToString() => Text;

	public static CompletionTypeCombo[] DataSource = {
		new() {Id = CompletionType.Completion, Text = "Completion (no context)"},
		new() {Id = CompletionType.Chat, Text = "Chat (with context)"}
	};
}