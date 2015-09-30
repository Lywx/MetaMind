namespace MetaMind.Engine
{
    using System;

    public interface IMMMvcComponent<out TMvcSettings, out TMvcLogic, out TMvcVisual> : IMMInputableComponent, IMMUpdateableOperations, IMMDrawableComponentOperations, IMMInputableOperations, IDisposable
        where                              TMvcLogic                                    : IMMMvcComponentLogic<TMvcSettings> 
        where                              TMvcVisual                                   : IMMMvcComponentVisual<TMvcSettings>
    {
        TMvcSettings Settings { get; }

        TMvcLogic Logic { get; }

        TMvcVisual Visual { get; }
    }
}