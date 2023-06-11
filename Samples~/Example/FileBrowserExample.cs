using System;
using System.Collections.Generic;
using UnityEngine;
using UnityFileBrowser;

internal sealed class FileBrowserExample : MonoBehaviour
{
    private static string _path = "";
    private readonly List<Button> _buttons = new()
    {
        new Button("Open File", () =>
        {
            var paths = FileBrowser.OpenFileBrowser("Open Image File", new[] { "png", "jpg" }, true);
            _path = string.Join("\n", paths);
        }),
        new Button("Open Multiple Files", () =>
        {
            var paths = FileBrowser.OpenFileBrowser("Open Text Files", new[] { "txt", "md" }, true);
            _path = string.Join("\n", paths);
        }),
        new Button("Open Folder", () =>
        {
            var paths = FileBrowser.OpenFolderBrowser("Open Folder");
            _path = string.Join("\n", paths);
        }),
        new Button("Save File", () =>
        {
            var path = FileBrowser.SaveFileBrowser(
                "Save File",
                Application.dataPath,
                "your_file.txt",
                new[] { "txt", "md" }
            );
            _path = path;
        }),
    };

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(16, 16, Screen.width - 32, Screen.height - 32));
        GUILayout.BeginVertical();

        foreach (var button in _buttons)
        {
            if (GUILayout.Button(button.Text, GUILayout.Width(200)))
                button.OnClick();
            GUILayout.Space(8);
        }
        
        GUILayout.Space(8);
        GUILayout.Label("Selected Path(s):");
        GUILayout.Label(_path);

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private sealed class Button
    {
        public string Text { get; }
        public Action OnClick { get; }

        public Button(string text, Action onClick)
        {
            Text = text;
            OnClick = onClick;
        }
    }
}