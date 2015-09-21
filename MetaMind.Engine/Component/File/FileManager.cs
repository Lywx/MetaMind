// --------------------------------------------------------------------------------------------------------------------
// <copyright file="File.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Component.File
{
    using System;
    using System.IO;
    using Setting.Loader;

    public class FileManager : IFileManager
    {
        #region Directory Settings

        public readonly string ConfigurationDirectory = @".\Configurations\";

        public readonly string ContentDirectory       = @".\Content\";

        public readonly string DataDirectory          = @".\Data\";

        public readonly string SaveDirectory          = @".\Save\";

        #endregion Directory Settings

        #region Constructors

        public FileManager()
        {
            this.CreateDirectory();
        }

        #endregion Constructors

        #region Path

        public string ContentPath(string relativePath) => Path.Combine(ContentDirectory, relativePath);

        /// <summary>
        /// Get path of file data folder.
        /// </summary>
        /// <param name="relativePath">Path related to DataFolder</param>
        /// <returns></returns>
        public string DataPath(string relativePath) => Path.Combine(DataDirectory, relativePath);

        public string DataRelativePath(string path)
        {
            var dataFullPath = Path.GetFullPath(DataDirectory);
            var fullPath     = Path.GetFullPath(path);

            return fullPath.Substring(dataFullPath.Length);
        }

        public string ConfigurationPath(IConfigurationLoader loader) => Path.Combine(ConfigurationDirectory, loader.ConfigurationFile);

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