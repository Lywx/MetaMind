namespace MetaMind.Engine.Guis.Widgets.Items
{
    public class ViewItemBasicFactory1D : IViewItemFactory
    {
        public dynamic CreateControl(IViewItem item)
        {
            return new ViewItemControl1D(item);
        }

        public dynamic CreateData(IViewItem item)
        {
            return new ViewItemData();
        }

        public IItemGraphics CreateGraphics(IViewItem item)
        {
            return new ViewItemBasicGraphics(item);
        }
    }
}