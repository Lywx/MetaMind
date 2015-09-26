namespace MetaMind.Engine
{
    using System;

    public interface IGameComponentVisual<out TModuleSettings> : IGameComponentModuleComponent<TModuleSettings>, IOuterUpdateableOperations, IDrawableComponentOperations, IDisposable 
    {
    }
}