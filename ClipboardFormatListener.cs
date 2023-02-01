using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ClipGPT
{
	public sealed class ClipboardFormatListener : Control, IClipboardListener
	{
		#region User32.dll functions
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		internal static extern bool AddClipboardFormatListener(IntPtr hwnd);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		internal static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		internal static extern bool EmptyClipboard();

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr GetClipboardOwner();
		
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern bool CloseClipboard();

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern bool SetClipboardData(uint uFormat, IntPtr data);
		#endregion

		#region User32.dll constants
		private const int WM_CLIPBOARDUPDATE = 0x031D;
		private const uint FORMAT_UNICODE = 13;
		#endregion

		private string _lastRequest = string.Empty;
		
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_CLIPBOARDUPDATE:
					var iData = Clipboard.GetDataObject();
					if (iData != null && iData.GetDataPresent(DataFormats.Text))
					{
						var data = (string) iData.GetData(DataFormats.Text);

						if (!string.Equals(_lastRequest, data, StringComparison.OrdinalIgnoreCase))
						{
							_lastRequest = data;
							OnClipboardUpdated(new ClipboardUpdatedEventArgs { Data = data });
						}
					}
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}

		private void OnClipboardUpdated(ClipboardUpdatedEventArgs e)
		{
			ClipboardUpdated?.Invoke(this, e);
		}

		public event EventHandler<ClipboardUpdatedEventArgs> ClipboardUpdated;

		public void Register()
		{
			AddClipboardFormatListener(Handle);
		}

		public void Deregister()
		{
			RemoveClipboardFormatListener(Handle);
		}

		public void CopySafe(string data)
		{
			RemoveClipboardFormatListener(Handle);
			Clipboard.SetText(data);
			AddClipboardFormatListener(Handle);
		}
	}
}