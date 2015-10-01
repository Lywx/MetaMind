namespace MetaMind.Engine.Components
{
    using System;
    using Entities;

    public interface IMMMvcComponentLogic<out TMvcSettings> : IMMMvcComponentComponent<TMvcSettings>, IMMUpdateableOperations, IMMInputOperations, IDisposable 
    {
    }
}