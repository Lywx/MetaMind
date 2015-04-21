namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Services;

    public interface IGameEntity : IUpdateable, IDisposable
    {
        void LoadContent(IGameFile gameFile);

        void LoadGraphics(IGameGraphicsService graphics);

        void LoadInterop(IGameInteropService interop);

        void UnloadContent(IGameFile gameFile);

        void UnloadGraphics(IGameGraphicsService graphics);

        void UnloadInterop(IGameInteropService interop);
    }
}