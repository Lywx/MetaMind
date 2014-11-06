using MetaMind.Engine.Guis.Widgets.ViewItems;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    public class FutureItemFactory : TaskItemFactory
    {
        public override dynamic CreateData( IViewItem item )
        {
            return Perseverance.Adventure.Tasklist.CreateFuture();
        }
    }
}