using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using ClipGpt7.Enum;

namespace ClipGpt7;

[Serializable]
public class XmlUserSettings : IUserSettings
{
	private static class Defaults
	{
		public const int Temperature = 8;
		public const int MaxTokens = 16;
		public const string ApiKey = "";
		public const ModelType LanguageModel = ModelType.Gpt35Turbo;
		public const CompletionType CompletionMethod = CompletionType.Chat;

		public static readonly string[] SavedBehaviours = {
			"You are a helpful assistant.",
			"You are a crazy scientist.",
			"You are playing devil's advocate."
		};

		public const int SelectedBehaviour = 0;
	}

	private int _temperature = Defaults.Temperature;
	private int _maxTokens = Defaults.MaxTokens;
	private string _apiKey = Defaults.ApiKey;
	private ModelType _languageModel = Defaults.LanguageModel;
	private CompletionType _completionMethod = Defaults.CompletionMethod;
	private List<string> _savedBehaviours = Defaults.SavedBehaviours.ToList(); // @fixme: overwrite when parsing config file
	private int _selectedBehaviour = Defaults.SelectedBehaviour;

	private static string ConfigPath
	{
		get
		{
			//var cfgDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var exePath = AppContext.BaseDirectory;
			var cfgDir = Path.GetDirectoryName(exePath) ?? string.Empty;
			var cfgPath = Path.Combine(cfgDir, "UserSettings.xml");
			return cfgPath;
		}
	}

	[XmlElement]
	public int Temperature
	{
		get => _temperature;
		set => SetField(ref _temperature, value);
	}

	[XmlElement]
	public int MaxTokens
	{
		get => _maxTokens;
		set => SetField(ref _maxTokens, value);
	}

	[XmlElement]
	public string ApiKey
	{
		get => _apiKey;
		set => SetField(ref _apiKey, value);
	}

	[XmlElement]
	public ModelType LanguageModel
	{
		get => _languageModel;
		set => SetField(ref _languageModel, value);
	}

	[XmlElement]
	public CompletionType CompletionMethod
	{
		get => _completionMethod;
		set => SetField(ref _completionMethod, value);
	}

	[XmlElement]
	public List<string> SavedBehaviours
	{
		get => _savedBehaviours;
		set => SetField(ref _savedBehaviours, value);
	}

	[XmlElement]
	public int SelectedBehaviour
	{
		get => _selectedBehaviour;
		set => SetField(ref _selectedBehaviour, value);
	}

	public void Save()
	{
		using var fs = File.Open(ConfigPath, FileMode.Create, FileAccess.Write);
		var serializer = new XmlSerializer(typeof(XmlUserSettings));
		serializer.Serialize(fs, this);
	}

	public void Reset()
	{
		Temperature = Defaults.Temperature;
		MaxTokens = Defaults.MaxTokens;
		ApiKey = Defaults.ApiKey;
		LanguageModel = Defaults.LanguageModel;
		CompletionMethod = Defaults.CompletionMethod;
		SavedBehaviours = Defaults.SavedBehaviours.ToList();
		SelectedBehaviour = Defaults.SelectedBehaviour;
	}

	public static XmlUserSettings ReadConfig()
	{
		var config = new XmlUserSettings();

		if (File.Exists(ConfigPath))
		{
			using var fs = File.Open(ConfigPath, FileMode.Open, FileAccess.Read);
			var xmlReader = XmlReader.Create(fs);
			var deserializer = new XmlSerializer(typeof(XmlUserSettings));
			config = deserializer.Deserialize(xmlReader) as XmlUserSettings
			         ?? throw new Exception("failed to parse configuration");
		}

		return config;
	}

	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
}