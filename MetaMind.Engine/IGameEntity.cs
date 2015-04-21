namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Services;

    public interface IGameEntity : IUpdateable, IDisposable
    {
        void LoadContent(IGameInteropService interop);

        void LoadGraphics(IGameGraphicsService graphics);

        void LoadInterop(Services.IGameInteropService interop);

        void UnloadContent(IGameInteropService interop);

        void UnloadGraphics(IGameGraphicsService graphics);

        void UnloadInterop(Services.IGameInteropService interop);
    }
}