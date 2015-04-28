namespace MetaMind.Engine.Guis.Widgets.Items
{
    public abstract class ViewItemFactory2D : IViewItemFactory
    {
        public virtual dynamic CreateControl(IViewItem item)
        {
            return new ViewItemControl2D(item);
        }

        public abstract dynamic CreateData(IViewItem item);

        public virtual IItemVisualControl CreateGraphics(IViewItem item)
        {
            return new ViewItemVisualControl(item);
        }
    }
}