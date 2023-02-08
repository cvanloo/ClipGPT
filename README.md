# ClipGPT

Your teacher will be clueless...

## (Anti-) Features

- .NET Framework 4.7.2 (C# 7.3) (**Windows only!**)
- Hides as a little tray icon.
- Uses `User32.dll` functions to register a clipboard format listener.
- Sends any copied text to ChatGPT, places response back into the clipboard.

## Showcase

![Showcase](https://videos.shmalls.pw/w/kvy5Jhb9MAw1rbsbpe85Eq)

## Prerequisites

- .NET Framework 4.7.2

## Installation

Run the installer and follow its instructions.

```shell
Output/installer.exe
```

## Usage

1. Use Rider or Visual Studio to compile the project.

   (To do so, first open the solution file `rider ClipGPT.sln`)

2. Run the application `bin/(Debug|Release)/ClipGPT.exe`.

3. Click &ldquo;Settings&rdquo; in the tray icon to open the settings window.

   Use it to configure your API key and other options.

4. To stop the application right-click the tray icon and press &ldquo;Exit&rdquo;.

## Known Problems

- Opening the settings window _sometimes_ takes extremely long the first time only.\
  (Up to a full minute!)
- Settings: Sometimes an exception is thrown that the invalid value of '0' can't be
  set in the config, even though we tried to set a (valid) value like '6'.
