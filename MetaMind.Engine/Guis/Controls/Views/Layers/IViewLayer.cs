namespace MetaMind.Engine.Guis.Controls.Views.Layers
{
    public interface IViewLayer : IViewComponent
    {
        T Get<T>() where T : class, IViewLayer;
    }
}