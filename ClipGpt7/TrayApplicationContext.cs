using ClipGpt7.Properties;

namespace ClipGpt7;

public class TrayApplicationContext : ApplicationContext
{
	private readonly NotifyIcon _trayIcon;
	private readonly IClipboardListener _clipboardListener;
	private readonly IAskGpt _askGpt;
	private readonly IUserSettings _userSettings;
	private readonly SettingsForm _settingsForm;

	public TrayApplicationContext(IClipboardListener clipboardListener, IAskGpt askGpt, IUserSettings userSettings)
	{
		_trayIcon = new NotifyIcon
		{
			Icon = Resources.AppIcon,
			ContextMenuStrip = new ContextMenuStrip
			{
				Items =
				{
					new ToolStripMenuItem("Clear Context", null, ClearContext),
					new ToolStripSeparator(),
					new ToolStripMenuItem("Settings", null, Settings),
					new ToolStripSeparator(),
					new ToolStripMenuItem("Exit", null, Exit),
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

		_settingsForm = new SettingsForm(_userSettings, _askGpt);
	}

	private async void DoRequest(object? sender, ClipboardUpdatedEventArgs args)
	{
		if (args.Data != null)
		{
			_trayIcon.Text = "Request started...";
			var result = await _askGpt.Prompt(args.Data);
			_clipboardListener.CopySafe(result);
			_trayIcon.Text = "Request finished!";
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
		if (_settingsForm.InvokeRequired)
		{
			var action = new Action(() => Settings(sender, args));
			_settingsForm.Invoke(action);
		}
		else
		{
			_clipboardListener.Deregister();
			_settingsForm.ShowDialog();
			_clipboardListener.Register();
		}
	}

	private void ClearContext(object? sender, EventArgs args) => _askGpt.ClearContext();

	private static void Exit(object? sender, EventArgs args) => Application.Exit();
}