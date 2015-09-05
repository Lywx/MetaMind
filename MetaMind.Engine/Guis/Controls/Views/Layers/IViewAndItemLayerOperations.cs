namespace MetaMind.Engine.Guis.Widgets.Views.Layers
{
    public interface IViewAndItemLayerOperations
    {
        T ViewGetLayer<T>() where T : class, IViewLayer;

        void SetupLayer();
    }
}