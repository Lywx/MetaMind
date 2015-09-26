namespace MetaMind.Engine.Components.File
{
    using System;
    using Microsoft.Xna.Framework;

    public interface ISaveManager : IGameComponent, IDisposable
    {
        void Save();

        void Load();
    }
}