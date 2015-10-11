namespace MetaMind.Engine.Components
{
    using System;
    using Entities;

    public interface IMMMVCComponentController<out TMVCSettings> : IMMMvcComponentComponent<TMVCSettings>, IMMUpdateableOperations, IMMInputOperations, IDisposable 
    {
    }
}