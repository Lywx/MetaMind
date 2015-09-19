namespace MetaMind.Engine.Gui.Control.Item.Layers
{
    public interface IViewItemLayer : IViewItemComponent
    {
        T Get<T>() where T : class, IViewItemLayer;
    }
}