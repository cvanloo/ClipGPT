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

		public TrayApplicationContext(IClipboardListener listener, IAskGpt askGpt)
		{
			_trayIcon = new NotifyIcon()
			{
				Icon = Resources.AppIcon,
				ContextMenu = new ContextMenu(new[]
				{
					new MenuItem("Exit", Exit)
				}),
				Visible = true
			};
			_askGpt = askGpt;
			_clipboardListener = listener;
			_clipboardListener.ClipboardUpdated += DoRequest;
			_clipboardListener.Register();
		}
		
		private void Exit(object sender, EventArgs args)
		{
			_trayIcon.Visible = false;
			_clipboardListener.Deregister();
			_clipboardListener.ClipboardUpdated -= DoRequest;
			Application.Exit();
		}
		
		private async void DoRequest(object sender, ClipboardUpdatedEventArgs e)
		{
			_clipboardListener.Deregister();
			var result = await _askGpt.Prompt(e.Data);
			Clipboard.SetText(result);
			_clipboardListener.Register();
		}
	}
}