namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IGame : IGameComponent, Microsoft.Xna.Framework.IUpdateable, Microsoft.Xna.Framework.IDrawable, IDisposable
    {
        void Run();

        void OnExiting();
    }
}