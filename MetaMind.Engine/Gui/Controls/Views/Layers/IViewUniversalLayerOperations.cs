namespace MetaMind.Engine.Gui.Controls.Views.Layers
{
    public interface IViewUniversalLayerOperations
    {
        void Initialize();

        T GetViewLayer<T>() where T : class, IViewLayer;
    }
}