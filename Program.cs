using System;
using System.Windows.Forms;
using System.Configuration;

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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			string apiKey;
			try
			{
				apiKey = ConfigurationManager.AppSettings["ApiKey"];
			}
			catch (ConfigurationErrorsException ex)
			{
				Console.Error.WriteLine(ex);
				return;
			}
			
			IAskGpt askGpt = new AskGpt(apiKey);
			IClipboardListener listener = new ClipboardFormatListener();
			
			Application.Run(new TrayApplicationContext(listener, askGpt));
		}
	}
}