using MetaMind.Engine.Guis.Widgets.Items;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemBasicFactory2D : IViewItemFactory
    {
        public virtual dynamic CreateControl( IViewItem item )
        {
            return new ViewItemControl2D( item );
        }

        public virtual dynamic CreateData( IViewItem item )
        {
            return new ViewItemData();
        }

        public virtual IItemGraphics CreateGraphics( IViewItem item )
        {
            return new ViewItemBasicGraphics( item );
        }
    }
}