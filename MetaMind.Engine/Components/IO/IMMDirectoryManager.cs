namespace MetaMind.Engine.Components.IO
{
    using System;

    public interface IMMDirectoryManager : IDisposable
    {
        void DeleteSaveDirectory();
    }
}