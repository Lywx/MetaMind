namespace MetaMind.Engine
{
    using System;

    public interface IGameMvcComponentVisual<out TMvcSettings> : IGameComponentModuleComponent<TMvcSettings>, IOuterUpdateableOperations, IDrawableComponentOperations, IDisposable 
    {
    }
}