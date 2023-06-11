using System;
using System.Runtime.InteropServices;

namespace UnityFileBrowser
{
    public sealed class FileBrowser
    {
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        private const string LibName = "libfile_browser.dylib";
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        private const string LibName = "file_browser.dll";
#endif
        
        [DllImport(LibName)]
        private static extern void free_memory();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private static extern IntPtr open_file_dialog(string title, string dir, string exts, bool multiple);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private static extern IntPtr open_folder_dialog(string title, string dir, bool multiple);
        
        /// <summary>
        /// Opens a file browser to select one or more files.
        /// </summary>
        /// <param name="title">The title of the file browser.</param>
        /// <param name="directory">The directory to open the file browser in.</param>
        /// <param name="extensions">The extensions of the files that can be selected.</param>
        /// <param name="multiple">Whether or not multiple files can be selected.</param>
        /// <returns>An array of paths to the selected files.</returns>
        public static string[] OpenFileBrowser(string title, string directory, string[] extensions, bool multiple = false)
        {
            var exts = string.Join(";", extensions);
            var pathsPtr = open_file_dialog(title, directory, exts, multiple);
            var paths = Marshal.PtrToStringAnsi(pathsPtr);
            free_memory();
            return paths != null ? paths.Split(';') : Array.Empty<string>();
        }
        
        /// <summary>
        /// Opens a file browser to select one or more files.
        /// </summary>
        /// <param name="title">The title of the file browser.</param>
        /// <param name="directory">The directory to open the file browser in.</param>
        /// <param name="extension">The extension of the files that can be selected.</param>
        /// <param name="multiple">Whether or not multiple files can be selected (optional).</param>
        /// <returns>An array of paths to the selected files.</returns>
        public static string[] OpenFileBrowser(string title, string directory, string extension, bool multiple = false)
        {
            return OpenFileBrowser(title, directory, new[] { extension }, multiple);
        }
        
        /// <summary>
        /// Opens a file browser to select one or more files.
        /// </summary>
        /// <param name="title">The title of the file browser.</param>
        /// <param name="extensions">The extensions of the files that can be selected.</param>
        /// <param name="multiple">Whether or not multiple files can be selected (optional).</param>
        /// <returns>An array of paths to the selected files.</returns>
        public static string[] OpenFileBrowser(string title, string[] extensions, bool multiple = false)
        {
            return OpenFileBrowser(title, string.Empty, extensions, multiple);
        }     
        
        /// <summary>
        /// Opens a file browser to select one or more files.
        /// </summary>
        /// <param name="extensions">The extensions of the files that can be selected.</param>
        /// <param name="multiple">Whether or not multiple files can be selected (optional).</param>
        /// <returns>An array of paths to the selected files.</returns>
        public static string[] OpenFileBrowser(string[] extensions, bool multiple = false)
        {
            return OpenFileBrowser(string.Empty, extensions, multiple);
        }

        /// <summary>
        /// Opens a folder browser to select one or more folders.
        /// </summary>
        /// <param name="title">The title of the folder browser.</param>
        /// <param name="directory">The directory to open the folder browser in.</param>
        /// <param name="multiple">Whether or not multiple folders can be selected (optional).</param>
        /// <returns>An array of paths to the selected folders.</returns>
        public static string[] OpenFolderBrowser(string title, string directory, bool multiple = false)
        {
            var pathsPtr = open_folder_dialog(title, directory, multiple);
            var paths = Marshal.PtrToStringAnsi(pathsPtr);
            free_memory();
            return paths != null ? paths.Split(';') : Array.Empty<string>();
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
