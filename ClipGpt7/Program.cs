namespace ClipGpt7;

internal static class Program
{
	[STAThread]
	internal static void Main()
	{
		// ensure only a single instance of this app can run
		var mutex = new Mutex(true, "ClipGpt", out var onlyInstance);
		if (!onlyInstance) throw new Exception("App is already running!");
		
		// This setup must be done before we instantiate the ClipboardFormatListener, since it
		// inherits from IWin32Window.
		// Calling it afterwards would produce the exception: "SetCompatibleTextRenderingDefault must be
		//   called before the first IWin32Window object is created in the application."
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		IUserSettings userSettings = XmlUserSettings.ReadConfig();
		IAskGpt askGpt = new AskGpt(userSettings);
		IClipboardListener clipboardListener = new ClipboardFormatListener();

		//ApplicationConfiguration.Initialize();
		Application.Run(new TrayApplicationContext(clipboardListener, askGpt, userSettings));
		
		// hold on to mutex for as long as app is running (keep this line at the bottom)
		GC.KeepAlive(mutex);
	}
}