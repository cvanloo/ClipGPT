using System.ComponentModel;

namespace ClipGPT
{
	public interface IUserSettings : INotifyPropertyChanged
	{
		void Save();
		void Reset();
		int Temperature { get; set; }
		int MaxTokens { get; set; }
		string ApiKey { get; set; }
	}
}