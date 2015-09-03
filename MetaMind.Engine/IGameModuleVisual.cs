namespace MetaMind.Engine
{
    public interface IGameModuleVisual<out TModuleSettings> : IGameModuleComponent<TModuleSettings>, IOuterUpdateableOperations, IDrawableOperations
    {
    }
}