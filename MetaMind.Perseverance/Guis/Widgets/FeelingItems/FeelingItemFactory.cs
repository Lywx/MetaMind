using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingItems
{
    public class FeelingItemFactory : IViewItemFactory
    {
        public dynamic CreateControl( IViewItem item )
        {
            return new FeelingItemControl( item );
        }

        public IViewItemData CreateData( IViewItem item )
        {
            return new FeelingItemData( item );
        }

        public IItemGraphics CreateGraphics( IViewItem item )
        {
            return new FeelingItemGraphics( item );
        }
    }
}