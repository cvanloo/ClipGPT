# ClipGPT

Your teacher will be clueless...

## (Anti-) Features

- **Windows only!**
- .NET 7 (C# 11) | self-contained, single-file EXE
- WinForms
- Hides as a little tray icon.
- Uses `User32.dll` functions to register a clipboard format listener.
- Sends any copied text to ChatGPT, places response back into the clipboard.

## Showcase

TODO

## Building

The application may be built as a self-contained executable, enabling it to run even on computers that do not have the
.NET 7 runtime installed.

- Install .NET 7 (Framework & Runtime)
- From the project root:

    ```bash
    dotnet publish -c Release \
                   -r win-x64 \
                   --self-contained true \
                   -p:PublishSingleFile=true \
                   -p:IncludeNativeLibrariesForSelfExtract=true \
                   -p:PublishReadyToRun=true
    ```
  
    Alternatively for Powershell:
  ```shell
  dotnet publish -c Release `
                 -r win-x64 `
                 --self-contained true `
                 -p:PublishSingleFile=true `
                 -p:IncludeNativeLibrariesForSelfExtract=true `
                 -p:PublishReadyToRun=true
  ```

The resulting binary and DLL:s are located in `ClipGpt7\bin\Release\net7.0-windows\win-x64\publish`.

## Installation

TODO!

## Usage

1. (If you haven't already) Create a ChatGPT account and generate an API token (https://platform.openai.com/account/api-keys).
2. Start the application `ClipGpt7.exe`
3. Locate the tray icon with the white question mark on black background.
4. Right-click → Settings
5. Paste the API token.
6. Click "Save and Close"
7. Now simply **copy** your questions, wait a bit, then **paste** GPTs answer.

## Known Problems

- "We are not final because we are infallible, but we are infallible because we are final." -- <cite>Robert Jackson</cite>