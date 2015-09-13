namespace MetaMind.Engine.Screens
{
    public interface ILayerManager : ILayerManagerOperations
    {
        GameControllableEntityCollection<IGameLayer> Layers { get; }
    }
}