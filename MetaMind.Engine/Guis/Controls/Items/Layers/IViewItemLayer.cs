namespace MetaMind.Engine.Guis.Controls.Items.Layers
{
    public interface IViewItemLayer : IViewItemComponent
    {
        T Get<T>() where T : class, IViewItemLayer;
    }
}