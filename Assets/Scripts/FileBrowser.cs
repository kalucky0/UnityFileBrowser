using System;

namespace UnityFileBrowser
{
    public sealed class FileBrowser
    {
        /// <summary>
        /// Opens a file browser to select one or more files.
        /// </summary>
        /// <param name="title">The title of the file browser.</param>
        /// <param name="directory">The directory to open the file browser in.</param>
        /// <param name="extensions">The extensions of the files that can be selected.</param>
        /// <param name="multiselect">Whether or not multiple files can be selected.</param>
        /// <returns>An array of paths to the selected files.</returns>
        public static string[] OpenFileBrowser(string title, string directory, string[] extensions, bool multiselect = false)
        {
            return Array.Empty<string>();
        }
        
        /// <summary>
        /// Opens a file browser to select one or more files.
        /// </summary>
        /// <param name="title">The title of the file browser.</param>
        /// <param name="directory">The directory to open the file browser in.</param>
        /// <param name="extension">The extension of the files that can be selected.</param>
        /// <param name="multiselect">Whether or not multiple files can be selected (optional).</param>
        /// <returns>An array of paths to the selected files.</returns>
        public static string[] OpenFileBrowser(string title, string directory, string extension, bool multiselect = false)
        {
            return OpenFileBrowser(title, directory, new[] { extension }, multiselect);
        }
        
        /// <summary>
        /// Opens a file browser to select one or more files.
        /// </summary>
        /// <param name="title">The title of the file browser.</param>
        /// <param name="extensions">The extensions of the files that can be selected.</param>
        /// <param name="multiselect">Whether or not multiple files can be selected (optional).</param>
        /// <returns>An array of paths to the selected files.</returns>
        public static string[] OpenFileBrowser(string title, string[] extensions, bool multiselect = false)
        {
            return OpenFileBrowser(title, string.Empty, extensions, multiselect);
        }     
        
        /// <summary>
        /// Opens a file browser to select one or more files.
        /// </summary>
        /// <param name="extensions">The extensions of the files that can be selected.</param>
        /// <param name="multiselect">Whether or not multiple files can be selected (optional).</param>
        /// <returns>An array of paths to the selected files.</returns>
        public static string[] OpenFileBrowser(string[] extensions, bool multiselect = false)
        {
            return OpenFileBrowser(string.Empty, extensions, multiselect);
        }

        /// <summary>
        /// Opens a folder browser to select one or more folders.
        /// </summary>
        /// <param name="title">The title of the folder browser.</param>
        /// <param name="directory">The directory to open the folder browser in.</param>
        /// <param name="multiselect">Whether or not multiple folders can be selected (optional).</param>
        /// <returns>An array of paths to the selected folders.</returns>
        public static string[] OpenFolderBrowser(string title, string directory, bool multiselect = false)
        {
            return Array.Empty<string>();
        }

        /// <summary>
        /// Opens a file browser to select a destination for saving a file.
        /// </summary>
        /// <param name="title">The title of the file browser.</param>
        /// <param name="directory">The directory to open the file browser in.</param>
        /// <param name="defaultName">The default name of the file to save.</param>
        /// <param name="extensions">The extensions that the file can be saved with.</param>
        /// <returns>The path to where the file should be saved.</returns>
        public static string SaveFileBrowser(string title, string directory, string defaultName, string[] extensions)
        {
            return string.Empty;
        }
    }
}
