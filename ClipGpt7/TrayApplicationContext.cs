using System.Net.Mime;
using ClipGpt7.Properties;

namespace ClipGpt7;

public class TrayApplicationContext : ApplicationContext
{
	private readonly NotifyIcon _trayIcon;
	private readonly IClipboardListener _clipboardListener;
	private readonly IAskGpt _askGpt;
	private readonly IUserSettings _userSettings;

	public TrayApplicationContext(IClipboardListener clipboardListener, IAskGpt askGpt, IUserSettings userSettings)
	{
		_trayIcon = new NotifyIcon
		{
			Icon = Resources.AppIcon,
			ContextMenuStrip = new ContextMenuStrip
			{
				Items =
				{
					new ToolStripMenuItem("Exit", null, Exit),
					new ToolStripSeparator(),
					new ToolStripMenuItem("Settings", null, Settings)
				}
			},
			Visible = true,
			Text = Resources.AppName
		};

		Application.ApplicationExit += OnApplicationExitHandler;
		_clipboardListener = clipboardListener;
		_askGpt = askGpt;
		_userSettings = userSettings;
		_clipboardListener.ClipboardUpdated += DoRequest;
		_clipboardListener.Register();
	}

	private async void DoRequest(object? sender, ClipboardUpdatedEventArgs args)
	{
		if (args?.Data != null)
		{
			var result = await _askGpt.Prompt(args.Data);
			_clipboardListener.SafeCopy(result);
		}
	}

	private void OnApplicationExitHandler(object? sender, EventArgs args)
	{
		_clipboardListener.Deregister();
		_clipboardListener.ClipboardUpdated -= DoRequest;
		_trayIcon.Visible = false;
		_trayIcon.Dispose();
	}

	private void Settings(object? sender, EventArgs args)
	{
		var settingsForm = new SettingsForm(_userSettings);
		if (settingsForm.InvokeRequired)
		{
			var action = new Action(() => Settings(sender, args));
			settingsForm.Invoke(action);
		}
		else
		{
			_clipboardListener.Deregister();
			settingsForm.ShowDialog();
			_clipboardListener.Register();
		}
	}

	private static void Exit(object? sender, EventArgs args) => Application.Exit();
}