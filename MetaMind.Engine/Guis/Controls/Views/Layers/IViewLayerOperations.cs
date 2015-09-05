namespace MetaMind.Engine.Guis.Widgets.Views.Layers
{
    using Items;
    using Items.Layers;

    public interface IViewLayerOperations : IViewAndItemLayerOperations 
    {
        T ItemGetLayer<T>(IViewItem item) where T : class, IViewItemLayer;
    }
}