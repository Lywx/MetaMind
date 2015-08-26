namespace MetaMind.Engine.Components
{
    using System;

    public interface IGameManager : IDisposable
    {
        void Plug(IGame game);

        void OnExiting();
    }
}