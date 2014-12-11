// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FolderManager.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System.IO;

    public class FolderManager
    {
        #region Directory Settings

        public const string DataFolderPath = @".\Data\";

        public const string SaveFolderPath = @".\Save\";

        #endregion Directory Settings

        #region Singleton

        private static FolderManager singleton;

        public static FolderManager GetInstance()
        {
            return singleton ?? (singleton = new FolderManager());
        }

        #endregion Singleton

        #region Constructors

        private FolderManager()
        {
            this.CreateDirectory();
        }

        #endregion Constructors

        public static string RelativePath(string path)
        {
            var dataFullPath = Path.GetFullPath(DataFolderPath);
            var fullPath     = Path.GetFullPath(path);

            return fullPath.Substring(dataFullPath.Length);
        }

        private void CreateDirectory()
        {
            if (!Directory.Exists(SaveFolderPath))
            {
                // if it doesn't exist, create
                Directory.CreateDirectory(SaveFolderPath);
            }

            if (!Directory.Exists(DataFolderPath))
            {
                Directory.CreateDirectory(DataFolderPath);
            }
        }
    }
}