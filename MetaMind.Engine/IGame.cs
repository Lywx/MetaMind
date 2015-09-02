namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IGame : IGameComponent, IUpdateable, Microsoft.Xna.Framework.IDrawable, IDisposable
    {
        void Run();

        void OnExiting();
    }
}