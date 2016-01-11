namespace MetaMind.Engine.Components
{
    using System;
    using Entities;
    using Entities.Bases;

    public interface IMMMVCComponentController<out TMVCSettings> : IMMMvcComponentComponent<TMVCSettings>, IMMUpdateableOperations, IMMInputOperations, IDisposable 
    {
    }
}