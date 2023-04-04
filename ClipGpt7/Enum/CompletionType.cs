namespace ClipGpt7.Enum;

/// <summary>
/// Type of AI completion to use.
/// </summary>
public enum CompletionType
{
	/// <summary>
	/// Normal completion, does not keep context.
	/// </summary>
	Completion,
	
	/// <summary>
	/// Chat completion, keeps context of most recent chat history.
	/// </summary>
	Chat
}

/// <summary>
/// Adapts a CompletionType for a WinForms ComboBox.
/// </summary>
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
	
	/// <summary>
	/// DataSource for a ComboBox.
	/// </summary>
	public static CompletionTypeCombo[] DataSource { get; } = {
		new(CompletionType.Completion, "Completion (no context)"),
		new(CompletionType.Chat, "Chat (with context)")
	};
}