using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    public class TaskItemFactory : ViewItemBasicFactory2D, IViewItemFactory
    {
        public override dynamic CreateControl( IViewItem item )
        {
            return new TaskItemControl( item );
        }

        public override IViewItemData CreateData( IViewItem item )
        {
            return base.CreateData( item );
        }

        public override IItemGraphics CreateGraphics( IViewItem item )
        {
            return new TaskItemGraphics( item );
        }
    }
}