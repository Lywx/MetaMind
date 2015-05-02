namespace MetaMind.Engine.Guis.Widgets.Views.Extensions
{
    public interface IViewExtension : IViewComponent
    {
        T Get<T>();
    }
}