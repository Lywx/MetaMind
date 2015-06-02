namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    public interface IViewItemLayer : IViewItemComponent
    {
        T Get<T>() where T : class, IViewItemLayer;
    }
}