namespace MetaMind.Engine.Component.File
{
    using System;
    using Setting.Loader;

    public interface IFileManager : IDisposable
    {
        #region Path

        string ConfigurationPath(IConfigurationLoader loader);

        string ContentPath(string relativePath);

        string DataPath(string relativePath);

        string DataRelativePath(string path);

        #endregion

        #region Directory

        void DeleteSaveDirectory();

        #endregion
    }
}