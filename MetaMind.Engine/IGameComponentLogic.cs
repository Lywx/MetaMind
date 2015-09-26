namespace MetaMind.Engine
{
    using System;

    public interface IGameComponentLogic<out TModuleSettings> : IGameComponentModuleComponent<TModuleSettings>, IOuterUpdateableOperations, IInputableOperations, IDisposable 
    {
    }
}