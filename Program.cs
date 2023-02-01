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

			// FIXME: Not sure if this is the idiomatic way to do this...
			string devApiKey;
			try
			{
				devApiKey = ConfigurationManager.AppSettings["ApiKey"];
			}
			catch (ConfigurationErrorsException ex)
			{
				Console.Error.WriteLine(ex);
				return;
			}

			var userSettings = new ApplicationConfig();
			if (!string.IsNullOrWhiteSpace(devApiKey))
			{
				userSettings.ApiKey = devApiKey;
			}
			IAskGpt askGpt = new AskGpt(userSettings);
			IClipboardListener listener = new ClipboardFormatListener();
			
			Application.Run(new TrayApplicationContext(listener, askGpt, userSettings));
			GC.KeepAlive(mutex);
		}
	}
}