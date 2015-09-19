namespace MetaMind.Engine.Screen
{
    public interface ILayerManager : ILayerManagerOperations
    {
        GameControllableEntityCollection<IGameLayer> Layers { get; }
    }
}