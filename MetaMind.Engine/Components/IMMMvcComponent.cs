namespace MetaMind.Engine.Components
{
    using System;
    using Entities;

    public interface IMMMvcComponent<out TMvcSettings, out TMvcLogic, out TMvcVisual> : IMMInputableComponent, IMMUpdateableOperations, IMMDrawableComponentOperations, IMMInputOperations, IDisposable
        where                              TMvcLogic                                    : IMMMvcComponentLogic<TMvcSettings> 
        where                              TMvcVisual                                   : IMMMvcComponentVisual<TMvcSettings>
    {
        TMvcSettings Settings { get; }

        TMvcLogic Logic { get; }

        TMvcVisual Visual { get; }
    }
}