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
            return Perseverance.Adventure.Motivationlist.Create( item.ViewSettings.Space );
        }

        public IItemGraphics CreateGraphics( IViewItem item )
        {
            return new MotivationItemGraphics( item );
        }
        public void RemoveData( IViewItem item )
        {
            Perseverance.Adventure.Motivationlist.Remove( item.ItemData, item.ViewSettings.Space );
        }
    }
}