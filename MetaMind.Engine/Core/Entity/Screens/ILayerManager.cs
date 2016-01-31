namespace MetaMind.Engine.Core.Entity.Screens
{
    using Entity.Common;

    public interface ILayerManager : ILayerManagerOperations
    {
        MMEntityCollection<IMMLayer> Layers { get; }
    }
}