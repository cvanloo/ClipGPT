using System;
using System.Windows.Forms;
using ClipGPT.Properties;

namespace ClipGPT
{
	public sealed class TrayApplicationContext : ApplicationContext
	{
		private readonly NotifyIcon _trayIcon;
		private readonly IClipboardListener _clipboardListener;
		private readonly IAskGpt _askGpt;
		private readonly ApplicationConfig _userSettings;

		public TrayApplicationContext(IClipboardListener listener, IAskGpt askGpt, ApplicationConfig userSettings)
		{
			_trayIcon = new NotifyIcon
			{
				Icon = Resources.AppIcon,
				ContextMenu = new ContextMenu(new[]
				{
					new MenuItem("Exit", Exit),
					new MenuItem("-"), // creates a separator
					new MenuItem("Settings", OpenSettings)
				}),
				Text = Resources.AppName,
				Visible = true
			};
			Application.ApplicationExit += OnApplicationExitHandler;
			_userSettings = userSettings;
			_askGpt = askGpt;
			_clipboardListener = listener;
			_clipboardListener.ClipboardUpdated += DoRequest;
			_clipboardListener.Register();
		}

		private async void DoRequest(object sender, ClipboardUpdatedEventArgs e)
		{
			var result = await _askGpt.Prompt(e.Data);
			_clipboardListener.CopySafe(result);
		}
		
		private void OnApplicationExitHandler(object sender, EventArgs e)
		{
			if (_trayIcon != null)
			{
				_trayIcon.Visible = false;
				_trayIcon.Dispose();
			}
			_clipboardListener.Deregister();
			_clipboardListener.ClipboardUpdated -= DoRequest;
		}

		private void OpenSettings(object sender, EventArgs args)
		{
			// FIXME: Why does this take so long the first time?
			var settingsForm = new SettingsForm(_userSettings);
			if (settingsForm.InvokeRequired)
			{
				var action = new Action(() => OpenSettings(sender, args));
				settingsForm.Invoke(action);
			}
			else
			{
				_clipboardListener.Deregister();
				settingsForm.ShowDialog();
				_clipboardListener.Register();
			}
		}

		private static void Exit(object sender, EventArgs args)
		{
			Application.Exit();
		}
	}
}