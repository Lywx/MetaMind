using MetaMind.Engine.Guis.Widgets.ViewItems;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    public class DirectionItemFactory : TaskItemFactory
    {
        public override dynamic CreateData( IViewItem item )
        {
            return Perseverance.Adventure.Tasklist.CreateDirection();
        }
    }
}