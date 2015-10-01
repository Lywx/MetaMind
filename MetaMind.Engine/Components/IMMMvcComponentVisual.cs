namespace MetaMind.Engine.Components
{
    using System;
    using Entities;

    public interface IMMMvcComponentVisual<out TMvcSettings> : IMMMvcComponentComponent<TMvcSettings>, IMMUpdateableOperations, IMMDrawableComponentOperations, IDisposable 
    {
    }
}