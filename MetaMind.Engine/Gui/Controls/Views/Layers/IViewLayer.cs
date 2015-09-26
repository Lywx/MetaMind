namespace MetaMind.Engine.Gui.Controls.Views.Layers
{
    public interface IViewLayer : IViewComponent
    {
        T Get<T>() where T : class, IViewLayer;
    }
}