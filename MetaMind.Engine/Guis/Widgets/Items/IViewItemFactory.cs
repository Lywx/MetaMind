namespace MetaMind.Engine.Guis.Widgets.Items
{
    public interface IViewItemFactory
    {
        dynamic CreateControl(IViewItem item);

        dynamic CreateData(IViewItem item);

        IItemVisualControl CreateGraphics(IViewItem item);
    }

    // TODO: Replace Factory CreateData
    public interface IViewItemDataBinding
    {
        dynamic CreateData(IViewItem item);
    }
}