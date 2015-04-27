namespace MetaMind.Engine.Guis.Widgets.Items
{
    public abstract class ViewItemFactory1D : IViewItemFactory
    {
        public dynamic CreateControl(IViewItem item)
        {
            return new ViewItemControl1D(item);
        }

        public abstract dynamic CreateData(IViewItem item);

        public IItemGraphics CreateGraphics(IViewItem item)
        {
            return new ViewItemGraphics(item);
        }
    }
}