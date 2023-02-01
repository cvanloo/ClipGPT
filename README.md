# ClipGPT

Your teacher will be clueless...

## (Anti-) Features

- .NET Framework 4.7.2 (C# 7.3) (**Windows only!**)
- Hides as a little tray icon.
- Uses `User32.dll` functions to register a clipboard format listener.
- Sends any copied text to ChatGPT, places response back into the clipboard.

## Prerequisites

- .NET Framework 4.7.2

## Usage

1. Configure your API key.

   (This command may only work when using WSL.)

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

4. To stop the application right-click the tray icon and press &ldquo;Exit&rdquo;.
