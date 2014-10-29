using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    public class TaskItemControl : ViewItemControl2D
    {
        public ItemEntryFrame NameFrame { get; private set; }

        #region Constructors

        public TaskItemControl( IViewItem item )
            : base( item )
        {

        }

        #endregion Constructors
    }
}