namespace MetaMind.Engine
{
    using System;

    public interface IGameModuleLogic<out TModuleSettings> : IGameModuleComponent<TModuleSettings>, IOuterUpdateableOperations, IInputableOperations, IDisposable 
    {
    }
}