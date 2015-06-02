namespace MetaMind.Engine.Guis.Widgets.Views.Layers
{
    public interface IViewLayer : IViewComponent
    {
        T Get<T>() where T : class, IViewLayer;
    }
}