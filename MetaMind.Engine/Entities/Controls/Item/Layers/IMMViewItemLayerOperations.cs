namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    using Views.Layers;

    public interface IMMViewItemLayerOperations : IMMViewUniversalLayerOperations 
    {
        T GetItemLayer<T>() where T : class, IMMViewItemLayer;
    }
}