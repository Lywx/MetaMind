namespace MetaMind.Engine.Gui.Control.Views.Layers
{
    public interface IViewLayer : IViewComponent
    {
        T Get<T>() where T : class, IViewLayer;
    }
}