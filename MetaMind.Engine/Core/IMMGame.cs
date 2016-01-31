namespace MetaMind.Engine.Core
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IMMGame : IGameComponent, IUpdateable, IDrawable, IDisposable
    {
        void Run();

        void OnExiting();
    }
}