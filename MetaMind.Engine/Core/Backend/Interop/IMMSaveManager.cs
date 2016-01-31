namespace MetaMind.Engine.Core.Backend.Interop
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IMMSaveManager : IGameComponent, IDisposable
    {
        void Save();

        void Load();
    }
}