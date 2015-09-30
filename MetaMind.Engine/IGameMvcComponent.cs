namespace MetaMind.Engine
{
    using System;

    public interface IGameMvcComponent<out TMvcSettings, out TMvcLogic, out TMvcVisual> : IGameInputableComponent, IOuterUpdateableOperations, IDrawableComponentOperations, IInputableOperations, IDisposable
        where                              TMvcLogic                                    : IGameMvcComponentLogic<TMvcSettings> 
        where                              TMvcVisual                                   : IGameMvcComponentVisual<TMvcSettings>
    {
        TMvcSettings Settings { get; }

        TMvcLogic Logic { get; }

        TMvcVisual Visual { get; }
    }
}