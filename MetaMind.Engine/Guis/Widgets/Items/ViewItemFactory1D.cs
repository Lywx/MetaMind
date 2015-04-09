namespace MetaMind.Engine.Guis.Widgets.Items
{
    public class ViewItemFactory1D : IViewItemFactory
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
            return new ViewItemGraphics(item);
        }
    }
}