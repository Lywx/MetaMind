namespace MetaMind.Engine.Core.Backend.IO
{
    using System;

    public interface IMMDirectoryManager : IDisposable
    {
        void DeleteSaveDirectory();
    }
}