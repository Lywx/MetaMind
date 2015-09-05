namespace MetaMind.Engine.Guis.Controls.Views.Layers
{
    public interface IViewAndItemLayerOperations
    {
        T ViewGetLayer<T>() where T : class, IViewLayer;

        void SetupLayer();
    }
}