namespace MetaMind.Engine.Core.Backend
{
    using System;

    public interface IMMGameManager : IDisposable
    {
        IMMGame Game { get; }

        void Add(IMMGame game);

        void OnExiting();
    }
}