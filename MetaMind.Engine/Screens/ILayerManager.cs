namespace MetaMind.Engine.Screens
{
    using Entities;

    public interface ILayerManager : ILayerManagerOperations
    {
        MMEntityCollection<IMMLayer> Layers { get; }
    }
}