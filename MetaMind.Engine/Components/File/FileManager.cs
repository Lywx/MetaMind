// --------------------------------------------------------------------------------------------------------------------
// <copyright file="File.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.File
{
    using System;
    using System.IO;
    using Setting.Loader;

    public class FileManager : IFileManager
    {
        #region Directory Settings

        public static readonly string ConfigurationDirectory = @".\Configurations\";

        public static readonly string ContentDirectory       = @".\Content\";

        public static readonly string DataDirectory          = @".\Data\";

        public static readonly string SaveDirectory          = @".\Save\";

        #endregion Directory Settings

        #region Constructors

        public FileManager()
        {
            this.CreateDirectory();
        }

        #endregion Constructors

        #region Path

        public static string ConfigurationPath(IConfigurationLoader loader) => Path.Combine(ConfigurationDirectory, loader.ConfigurationFile);

        public static string ContentPath(string relativePath) => Path.Combine(ContentDirectory, relativePath);

        public static string SavePath(string relativePath) => Path.Combine(SaveDirectory, relativePath);

        /// <summary>
        /// Get path of file data folder.
        /// </summary>
        /// <param name="relativePath">Path related to DataFolder</param>
        /// <returns></returns>
        public static string DataPath(string relativePath) => Path.Combine(DataDirectory, relativePath);

        public static string DataRelativePath(string path)
        {
            var dataFullPath = Path.GetFullPath(DataDirectory);
            var fullPath     = Path.GetFullPath(path);

            return fullPath.Substring(dataFullPath.Length);
        }

        #endregion

        #region Directory

        private void CreateDirectory()
        {
            if (!Directory.Exists(SaveDirectory))
            {
                // If it doesn't exist, create
                Directory.CreateDirectory(SaveDirectory);
            }

            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
        }

        public void DeleteSaveDirectory()
        {
            if (Directory.Exists(SaveDirectory))
            {
                Directory.Delete(SaveDirectory, true);
            }
        }

        #endregion


        #region IDisposable

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        private bool IsDisposed { get; set; }

        protected virtual void Dispose(bool disposing) { }

        #endregion

    }
}