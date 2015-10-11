namespace MetaMind.Engine.Components
{
    using System;
    using Entities;

    public interface IMMMVCComponentRenderer<out TMVCSettings> : IMMMvcComponentComponent<TMVCSettings>, IMMUpdateableOperations, IMMDrawableComponentOperations, IDisposable 
    {
    }
}