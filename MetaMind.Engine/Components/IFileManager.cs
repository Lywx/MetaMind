namespace MetaMind.Engine.Components
{
    using System;

    public interface IFileManager : IDisposable
    {
        void DeleteSaveDirectory();
    }
}