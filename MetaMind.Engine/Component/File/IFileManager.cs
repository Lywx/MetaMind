namespace MetaMind.Engine.Component.File
{
    using System;

    public interface IFileManager : IDisposable
    {
        void DeleteSaveDirectory();
    }
}