namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Services;

    public interface IGameEntity : IUpdateable, IDisposable
    {
        void LoadContent(IGameInteropService interop);
        
        void UnloadContent(IGameInteropService interop);
    }
}