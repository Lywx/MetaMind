namespace MetaMind.Engine
{
    using System;

    public interface IGameModule<out TGroupSettings>: IOuterUpdateableOperations, IDrawableOperations, IInputableOperations, IDisposable
    {
        TGroupSettings Settings { get; }

        IGameModuleLogic Logic { get; }

        IGameModuleVisual Visual { get; }

        void Initialize();
    }
}