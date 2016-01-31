namespace MetaMind.Engine.Core.Backend
{
    using System;
    using Entity.Common;

    public interface IMMMVCComponentController<out TMVCSettings> : IMMMvcComponentComponent<TMVCSettings>, IMMUpdateableOperations, IMMInputtableOperations, IDisposable 
    {
    }
}