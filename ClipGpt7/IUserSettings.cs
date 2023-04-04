using System.ComponentModel;
using ClipGpt7.Enum;

namespace ClipGpt7;

public interface IUserSettings : INotifyPropertyChanged
{
	void Save();
	void Reset();
	int Temperature { get; set; }
	int MaxTokens { get; set; }
	string ApiKey { get; set; }
	ModelType LanguageModel { get; set; }
	CompletionType CompletionMethod { get; set; }
	List<string> SavedBehaviours { get; set; }
	int SelectedBehaviour { get; set; }
}
