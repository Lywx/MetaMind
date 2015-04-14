namespace MetaMind.Engine
{
    using System;

    public interface IGameEntity : IUpdateable, IDisposable
    {
        void LoadContent(IGameFile gameFile);

        void LoadGraphics(IGameGraphics gameGraphics);

        void LoadInterop(IGameInterop gameInterop);

        void UnloadContent(IGameFile gameFile);

        void UnloadGraphics(IGameGraphics gameGraphics);

        void UnloadInterop(IGameInterop gameInterop);
    }
}