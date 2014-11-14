namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Guis.Elements.Items;

    public interface IViewItemFactory
    {
        dynamic CreateControl(IViewItem item);

        dynamic CreateData(IViewItem item);

        IItemGraphics CreateGraphics(IViewItem item);
    }
}