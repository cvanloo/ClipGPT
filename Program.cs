using System;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using ClipGPT.Properties;

namespace ClipGPT
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		internal static void Main()
		{
			// Ensure only ever one instance of this application can run.
			var mutex = new Mutex(true, Resources.AppName, out var onlyInstance);
			if (!onlyInstance)
			{
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var userSettings = new ApplicationConfig();
			IAskGpt askGpt = new AskGpt(userSettings);
			IClipboardListener listener = new ClipboardFormatListener();
			
			Application.Run(new TrayApplicationContext(listener, askGpt, userSettings));
			
			// Hold on to mutex for as long as the app is running. (Keep this line at the bottom)
			GC.KeepAlive(mutex);
		}
	}
}