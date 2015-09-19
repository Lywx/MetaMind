// --------------------------------------------------------------------------------------------------------------------
// <copyright file="File.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Component.File
{
    using System.IO;
    using Setting.Loader;

    public class FileManager : IFileManager
    {
        #region Directory Settings

        public const string DataFolderPath          = @".\Data\";

        public const string SaveFolderPath          = @".\Save\";

        public const string ConfigurationFolderPath = @".\Configurations\";

        #endregion Directory Settings

        #region Constructors

        public FileManager()
        {
            this.CreateDirectory();
        }

        #endregion Constructors

        /// <summary>
        /// Get path of file data folder.
        /// </summary>
        /// <param name="relativePath">Path related to DataFolder</param>
        /// <returns></returns>
        public static string DataPath(string relativePath)
        {
            return Path.Combine(DataFolderPath, relativePath);
        }

        public static string DataRelativePath(string path)
        {
            var dataFullPath = Path.GetFullPath(DataFolderPath);
            var fullPath     = Path.GetFullPath(path);

            return fullPath.Substring(dataFullPath.Length);
        }

        public static string ConfigurationPath(IConfigurationLoader loader)
        {
            return Path.Combine(ConfigurationFolderPath, loader.ConfigurationFile);
        }

        private void CreateDirectory()
        {
            if (!Directory.Exists(SaveFolderPath))
            {
                // If it doesn't exist, create
                Directory.CreateDirectory(SaveFolderPath);
            }

            if (!Directory.Exists(DataFolderPath))
            {
                Directory.CreateDirectory(DataFolderPath);
            }
        }

        public void DeleteSaveDirectory()
        {
            if (Directory.Exists(SaveFolderPath))
            {
                Directory.Delete(SaveFolderPath, true);
            }
        }

        public void Dispose()
        {
        }
    }
}