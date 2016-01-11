namespace MetaMind.Engine.Components
{
    using System;
    using Entities;
    using Entities.Bases;

    public interface IMMMVCComponentRenderer<out TMVCSettings> : IMMMvcComponentComponent<TMVCSettings>, IMMUpdateableOperations, IMMDrawableComponentOperations, IDisposable 
    {
    }
}