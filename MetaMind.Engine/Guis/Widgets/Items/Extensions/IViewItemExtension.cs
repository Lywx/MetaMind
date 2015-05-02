namespace MetaMind.Engine.Guis.Widgets.Items.Extensions
{
    public interface IViewItemExtension : IViewItemComponent
    {
        T Get<T>() where T : class;
    }
}