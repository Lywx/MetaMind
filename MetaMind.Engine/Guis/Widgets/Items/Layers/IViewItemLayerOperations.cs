namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    using Views.Layers;

    public interface IViewItemLayerOperations : IViewAndItemLayerOperations 
    {
        T ItemGetLayer<T>() where T : class, IViewItemLayer;
    }
}