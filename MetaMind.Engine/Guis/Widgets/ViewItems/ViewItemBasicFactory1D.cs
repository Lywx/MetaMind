using MetaMind.Engine.Guis.Widgets.Items;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemBasicFactory1D : IViewItemFactory
    {
        public dynamic CreateControl(IViewItem item)
        {
            return new ViewItemControl1D(item);
        }

        public IViewItemData CreateData(IViewItem item)
        {
            return new ViewItemBasicData(item);
        }

        public IItemGraphics CreateGraphics(IViewItem item)
        {
            return new ViewItemBasicGraphics(item);
        }
    }
}