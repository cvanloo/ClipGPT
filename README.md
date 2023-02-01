# ClipGPT

Your teacher will be clueless...

## Features

- .NET Framework 4.7.2 (Windows only!)
- Hides as a little tray icon.
- Uses `User32.dll` functions to register a clipboard format listener.
- Sends any copied text to ChatGPT, places response back into the clipboard.

## Usage

1. Save your API key into the `Resources.ApiKey` resource.

```powershell
dotnet build -c Release
bin/Release/ClipGPT.exe
```

To stop the application right-click the tray icon and press "Exit".
