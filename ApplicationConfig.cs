using System.Configuration;

namespace ClipGPT
{
	public sealed class ApplicationConfig : ApplicationSettingsBase
	{
		[UserScopedSetting]
		[DefaultSettingValue("5")]
		public int Temperature
		{
			get => (int) this["Temperature"];
			set => this["Temperature"] = (int) value;
		}

		[UserScopedSetting]
		[DefaultSettingValue("2048")]
		public int MaxTokens
		{
			get => (int) this["MaxTokens"];
			set => this["MaxTokens"] = (int) value;
		}

		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string ApiKey
		{
			get => (string) this["ApiKey"];
			set => this["ApiKey"] = (string) value;
		}
	}
}