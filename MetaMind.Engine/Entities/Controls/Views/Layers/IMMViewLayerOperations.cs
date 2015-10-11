namespace MetaMind.Engine.Entities.Controls.Views.Layers
{
    using Item;
    using Item.Layers;

    public interface IMMViewLayerOperations : IMMViewUniversalLayerOperations 
    {
        T GetItemLayer<T>(IMMViewItem item) where T : class, IMMViewItemLayer;
    }
}