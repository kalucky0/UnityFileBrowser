# Unity File Browser

![GitHub package.json version (branch)](https://img.shields.io/github/package-json/v/kalucky0/UnityFileBrowser/upm)
![GitHub](https://img.shields.io/github/license/kalucky0/UnityFileBrowser)
![GitHub issues](https://img.shields.io/github/issues/kalucky0/UnityFileBrowser)

> **Note**
> This package is still in development and may not be stable. Use at your own risk. Linux and Windows support is not yet implemented.

A simple package for Unity that allows you to open files and folders using the default file browser on the target device. It supports Windows, Mac and Linux and works both in the editor and at runtime.

## Installation

You can install the package via the Unity Package Manager by adding the following line to your `manifest.json` file located within your project's `Packages` directory:

```json
"dev.kalucky0.filebrowser": "https://github.com/kalucky0/UnityFileBrowser.git#upm"
```

or by using Package Manager window and selecting "Add package from git URL..." option and pasting the link to this repository.

Read more about [Git dependencies](https://docs.unity3d.com/2022.3/Documentation/Manual/upm-ui-giturl.html) in Unity Package Manager documentation.

## Usage

```csharp
// Open a file browser to select a file
var paths = FileBrowser.OpenFileBrowser(new[] { "png", "jpg" });

// Open a file browser to select a folder
var paths = FileBrowser.OpenFolderBrowser();

// Open a file browser to select a save location
var paths = FileBrowser.OpenSaveFileBrowser(new[] { "png", "jpg" });
```

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/kalucky0/UnityFileBrowser/blob/master/LICENSE.md) file for details.