namespace MetaMind.Engine.Core.Backend
{
    using System;
    using Entity.Common;

    public interface IMMMVCComponentRenderer<out TMVCSettings> : IMMMvcComponentComponent<TMVCSettings>, IMMUpdateableOperations, IMMDrawableComponentOperations, IDisposable 
    {
    }
}