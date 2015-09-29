namespace MetaMind.Engine
{
    using System;

    public interface IGameComponentModule<out TModuleSettings, out TModuleLogic, out TModuleVisual> : IGameInputableComponent, IOuterUpdateableOperations, IDrawableComponentOperations, IInputableOperations, IDisposable
        where TModuleLogic                                                                          : IGameComponentLogic<TModuleSettings> 
        where TModuleVisual                                                                         : IGameComponentVisual<TModuleSettings>
    {
        TModuleSettings Settings { get; }

        TModuleLogic Logic { get; }

        TModuleVisual Visual { get; }
    }
}