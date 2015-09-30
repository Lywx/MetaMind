namespace MetaMind.Engine
{
    using System;

    public interface IGameMvcComponentLogic<out TMvcSettings> : IGameComponentModuleComponent<TMvcSettings>, IOuterUpdateableOperations, IInputableOperations, IDisposable 
    {
    }
}