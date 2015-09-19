namespace MetaMind.Engine.Component
{
    using System;
    using Microsoft.Xna.Framework;
    using Service;

    public interface IGameGraphics : IGameComponent, IGameGraphicsService, IDisposable 
    {
    }
}