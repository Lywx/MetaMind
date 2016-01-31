namespace MetaMind.Engine.Core.Entity.Control.Item.Layers
{
    using Views.Layers;

    public interface IMMViewItemLayerOperations : IMMViewUniversalLayerOperations 
    {
        T GetItemLayer<T>() where T : class, IMMViewItemLayer;
    }
}