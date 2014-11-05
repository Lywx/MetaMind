using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemFactory : IViewItemFactory
    {
        public dynamic CreateControl( IViewItem item )
        {
            return new MotivationItemControl( item );
        }

        public dynamic CreateData( IViewItem item )
        {
            return new MotivationItemData( item );
        }

        public IItemGraphics CreateGraphics( IViewItem item )
        {
            return new MotivationItemGraphics( item );
        }
    }
}