namespace MetaMind.Engine.Gui.Control.Views.Layers
{
    public interface IViewUniversalLayerOperations
    {
        void Initialize();

        T GetViewLayer<T>() where T : class, IViewLayer;
    }
}