namespace MetaMind.Engine.Components
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

    public interface IGameGraphics : IGameComponent, IGameGraphicsService, IDisposable 
    {
    }
}