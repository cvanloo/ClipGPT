using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ClipGPT
{
	public class ClipboardFormatListener : Control
	{
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool AddClipboardFormatListener(IntPtr hwnd);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool EmptyClipboard();

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetClipboardOwner();
		
		[DllImport("user32.dll")]
		internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("user32.dll")]
		internal static extern bool CloseClipboard();

		[DllImport("user32.dll")]
		internal static extern bool SetClipboardData(uint uFormat, IntPtr data);

		private const int WM_CLIPBOARDUPDATE = 0x031D;
		private const uint FORMAT_UNICODE = 13;

		private readonly AskGPT _askGpt = new AskGPT();
		private string _lastRequest = string.Empty;

		private async void DoRequest(string prompt)
		{
			RemoveClipboardFormatListener(this.Handle);
			var result = await _askGpt.AskPrompt(prompt);
			Clipboard.SetText(result);
			AddClipboardFormatListener(this.Handle);
			//OpenClipboard(Handle);
			//byte[] strBytes = Encoding.Unicode.GetBytes(result);
			//IntPtr hglobal = Marshal.AllocHGlobal(strBytes.Length);
			//Marshal.Copy(strBytes, 0, hglobal, strBytes.Length);
			//OpenClipboard(IntPtr.Zero);
			//EmptyClipboard();
			//SetClipboardData(FORMAT_UNICODE, hglobal);
			//CloseClipboard();
			//Marshal.FreeHGlobal(hglobal);
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_CLIPBOARDUPDATE:
					//var owner = GetClipboardOwner();
					//if (owner == Handle)
					//	goto default;
					
					var iData = Clipboard.GetDataObject();
					if (iData != null && iData.GetDataPresent(DataFormats.Text))
					{
						var data = (string) iData.GetData(DataFormats.Text);

						if (data == _lastRequest)
						{
							goto default;
						}
						_lastRequest = data;

						DoRequest(data);
					}
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}
	}
}