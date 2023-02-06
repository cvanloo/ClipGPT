using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace ClipGPT
{
	[Serializable]
	public class UserSettings : IUserSettings
	{
		private static class Defaults
		{
			public const int Temperature = 5;
			public const int MaxTokens = 2048;
			public const string ApiKey = "";
		}
		
		private int _temperature = Defaults.Temperature;
		private int _maxTokens = Defaults.MaxTokens;
		private string _apiKey = Defaults.ApiKey;

		private static string ConfigPath
		{
			get
			{
				//var cfgDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				var exePath = System.Reflection.Assembly.GetEntryAssembly()?.Location ?? string.Empty;
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

		public void Save()
		{
			using (var fs = File.Open(ConfigPath, FileMode.Create, FileAccess.Write))
			{
				var serializer = new XmlSerializer(typeof(UserSettings));
				serializer.Serialize(fs, this);
			}
		}

		public void Reset()
		{
			Temperature = Defaults.Temperature;
			MaxTokens = Defaults.MaxTokens;
			ApiKey = Defaults.ApiKey;
		}

		public static UserSettings ReadConfig()
		{
			var config = new UserSettings();

			if (File.Exists(ConfigPath))
			{
				using (var fs = File.Open(ConfigPath, FileMode.Open, FileAccess.Read))
				{
					var xmlReader = XmlReader.Create(fs);
					var deserializer = new XmlSerializer(typeof(UserSettings));
					config = deserializer.Deserialize(xmlReader) as UserSettings;
				}
			}

			return config;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}