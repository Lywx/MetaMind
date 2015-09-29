namespace MetaMind.Engine.Components.Interop
{
    using System;
    using Microsoft.Xna.Framework;

    public interface ISaveManager : IGameComponent, IDisposable
    {
        void Save();

        void Load();
    }
}