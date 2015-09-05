namespace MetaMind.Engine
{
    using System;

    public interface IGameModuleVisual<out TModuleSettings> : IGameModuleComponent<TModuleSettings>, IOuterUpdateableOperations, IDrawableComponentOperations, IDisposable 
    {
    }
}