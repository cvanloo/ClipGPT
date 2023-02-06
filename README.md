# ClipGPT

Your teacher will be clueless...

## (Anti-) Features

- .NET Framework 4.7.2 (C# 7.3) (**Windows only!**)
- Hides as a little tray icon.
- Uses `User32.dll` functions to register a clipboard format listener.
- Sends any copied text to ChatGPT, places response back into the clipboard.

## Showcase

## Prerequisites

- .NET Framework 4.7.2

## Installation

Run the installer and follow its instructions.

```shell
InnoSetup/Output/mysetup.exe
```

## Usage

1. Configure your API key. (Optional - Developer only)

   Create the file `C:\Secrets\secrets.xml` with the contents:
   ```
   <root>
      <secrets ver="1.0">
         <secret name="ApiKey" value="your-api-key-here"/>
      </secrets>
   </root>
   ```
   Make sure to fill in your own API key.

2. Use Rider or Visual Studio to compile the project.

   (To do so, first open the solution file `rider ClipGPT.sln`)

3. Run the application `bin/(Debug|Release)/ClipGPT.exe`.

4. Click &ldquo;Settings&rdquo; in the tray icon to open the settings window.

   Use it to configure your API key and other options.

5. To stop the application right-click the tray icon and press &ldquo;Exit&rdquo;.

## Known Problems

- Opening the settings window _sometimes_ takes extremely long the first time only.\
  (Up to a full minute!)
- Settings: Sometimes an exception is thrown that the invalid value of '0' can't be
  set in the config, even though we tried to set a (valid) value like '6'.