using System;
using System.Windows.Forms;
using ClipGPT.Properties;

namespace ClipGPT
{
	public class TrayApplicationContext : ApplicationContext
	{
		private NotifyIcon _trayIcon;
		private ClipboardFormatListener _clipboardListener;

		public TrayApplicationContext()
		{
			_trayIcon = new NotifyIcon()
			{
				Icon = Resources.AppIcon,
				ContextMenu = new ContextMenu(new MenuItem[]
				{
					new MenuItem("Exit", Exit)
				}),
				Visible = true
			};
			_clipboardListener = new ClipboardFormatListener();
			ClipboardFormatListener.AddClipboardFormatListener(_clipboardListener.Handle);
		}
		
		private void Exit(object sender, EventArgs args)
		{
			_trayIcon.Visible = false;
			ClipboardFormatListener.RemoveClipboardFormatListener(_clipboardListener.Handle);
			Application.Exit();
		}
	}
}