namespace MetaMind.Engine.Component
{
    using System;

    public interface IGameManager : IDisposable
    {
        IGame Game { get; }

        void Add(IGame game);

        void OnExiting();
    }
}