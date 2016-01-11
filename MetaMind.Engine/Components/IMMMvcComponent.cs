namespace MetaMind.Engine.Components
{
    using System;
    using Entities;
    using Entities.Bases;

    public interface IMMMVCComponent<out TMVCSettings, out TMVCController, out TMVCRenderer> : IMMInputableComponent, IMMUpdateableOperations, IMMDrawableComponentOperations, IMMInputOperations, IDisposable
        where                            TMVCController                                      : IMMMVCComponentController<TMVCSettings> 
        where                            TMVCRenderer                                        : IMMMVCComponentRenderer<TMVCSettings>
    {
        TMVCSettings Settings { get; }

        TMVCController Controller { get; }

        TMVCRenderer Renderer { get; }
    }
}