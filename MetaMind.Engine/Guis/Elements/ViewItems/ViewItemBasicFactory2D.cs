namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Guis.Elements.Items;

    public class ViewItemBasicFactory2D : IViewItemFactory
    {
        public virtual dynamic CreateControl(IViewItem item)
        {
            return new ViewItemControl2D(item);
        }

        public virtual dynamic CreateData(IViewItem item)
        {
            return new ViewItemData();
        }

        public virtual IItemGraphics CreateGraphics(IViewItem item)
        {
            return new ViewItemBasicGraphics(item);
        }
    }
}