namespace MetaMind.Engine.Gui.Controls.Item.Layers
{
    public interface IViewItemLayer : IViewItemComponent
    {
        T Get<T>() where T : class, IViewItemLayer;
    }
}