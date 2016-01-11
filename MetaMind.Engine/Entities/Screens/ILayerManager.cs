namespace MetaMind.Engine.Entities.Screens
{
    using Bases;

    public interface ILayerManager : ILayerManagerOperations
    {
        MMEntityCollection<IMMLayer> Layers { get; }
    }
}