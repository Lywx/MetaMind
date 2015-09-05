namespace MetaMind.Engine.Guis.Controls.Items.Layers
{
    using Views.Layers;

    public interface IViewItemLayerOperations : IViewAndItemLayerOperations 
    {
        T ItemGetLayer<T>() where T : class, IViewItemLayer;
    }
}