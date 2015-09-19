namespace MetaMind.Engine.Gui.Control.Item.Layers
{
    using Views.Layers;

    public interface IViewItemLayerOperations : IViewUniversalLayerOperations 
    {
        T GetItemLayer<T>() where T : class, IViewItemLayer;
    }
}