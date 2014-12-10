namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IViewItemFactory
    {
        dynamic CreateControl(IViewItem item);

        dynamic CreateData(IViewItem item);

        IItemGraphics CreateGraphics(IViewItem item);
    }
}