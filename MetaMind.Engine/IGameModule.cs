namespace MetaMind.Engine
{
    using System;

    public interface IGameModule<out TModuleSettings, out TModuleLogic, out TModuleVisual> : IGameControllableComponent, IOuterUpdateableOperations, IDrawableComponentOperations, IInputableOperations, IDisposable
        where TModuleLogic  : IGameModuleLogic<TModuleSettings> 
        where TModuleVisual : IGameModuleVisual<TModuleSettings>
    {
        TModuleSettings Settings { get; }

        TModuleLogic Logic { get; }

        TModuleVisual Visual { get; }
    }
}