using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    public class TaskItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl( IViewItem item )
        {
            return new TaskItemControl( item );
        }

        public override dynamic CreateData( IViewItem item )
        {
            return Perseverance.Adventure.Tasklist.Create();
        }

        public override IItemGraphics CreateGraphics( IViewItem item )
        {
            return new TaskItemGraphics( item );
        }

        public void RemoveData( IViewItem item )
        {
            Perseverance.Adventure.Motivationlist.Remove( item.ItemData );
        }
    }
}