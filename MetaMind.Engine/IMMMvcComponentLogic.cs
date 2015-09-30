namespace MetaMind.Engine
{
    using System;

    public interface IMMMvcComponentLogic<out TMvcSettings> : IMMMvcComponentComponent<TMvcSettings>, IMMUpdateableOperations, IMMInputableOperations, IDisposable 
    {
    }
}