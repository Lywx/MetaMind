namespace MetaMind.Engine.Components
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;

    public interface IGameGraphics : IGameComponent, IGameGraphicsService, IDisposable 
    {
    }
}