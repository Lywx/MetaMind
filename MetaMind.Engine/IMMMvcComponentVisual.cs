namespace MetaMind.Engine
{
    using System;

    public interface IMMMvcComponentVisual<out TMvcSettings> : IMMMvcComponentComponent<TMvcSettings>, IMMUpdateableOperations, IMMDrawableComponentOperations, IDisposable 
    {
    }
}