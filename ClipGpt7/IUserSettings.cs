using System.ComponentModel;

namespace ClipGpt7;

public interface IUserSettings : INotifyPropertyChanged
{
	void Save();
	void Reset();
	int Temperature { get; set; }
	int MaxTokens { get; set; }
	string ApiKey { get; set; }
}
