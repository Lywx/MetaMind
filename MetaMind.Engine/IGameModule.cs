namespace MetaMind.Engine
{
    using System;

    public interface IGameModule<out TModuleSettings, out TModuleLogic, out TModuleVisual> : IOuterUpdateableOperations, IDrawableOperations, IInputableOperations
        where TModuleLogic  : IGameModuleLogic<TModuleSettings> 
        where TModuleVisual : IGameModuleVisual<TModuleSettings>
    {
        TModuleSettings Settings { get; }

        TModuleLogic Logic { get; }

        TModuleVisual Visual { get; }

        void Initialize();
    }
}