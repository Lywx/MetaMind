namespace MetaMind.Engine.Gui.Controls.Views.Layers
{
    public interface IViewUniversalLayerOperations 
    {
        T GetViewLayer<T>() where T : class, IViewLayer;
    }
}