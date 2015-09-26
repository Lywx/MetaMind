namespace MetaMind.Engine.Gui.Controls.Views.Layers
{
    using Item;
    using Item.Layers;

    public interface IViewLayerOperations : IViewUniversalLayerOperations 
    {
        T GetItemLayer<T>(IViewItem item) where T : class, IViewItemLayer;
    }
}