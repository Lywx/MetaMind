using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    public class TaskItemControl : ViewItemControl2D
    {
        public ItemEntryFrame NameFrame       { get { return ( ( TaskItemFrameControl ) ItemFrameControl ).NameFrame; } }
        public ItemEntryFrame IdFrame         { get { return ( ( TaskItemFrameControl ) ItemFrameControl ).IdFrame; } }
        public ItemEntryFrame ExperienceFrame { get { return ( ( TaskItemFrameControl ) ItemFrameControl ).ExperienceFrame; } }
        public ItemEntryFrame ProgressFrame   { get { return ( ( TaskItemFrameControl ) ItemFrameControl ).ProgressFrame; } }

        #region Constructors

        public TaskItemControl( IViewItem item )
            : base( item )
        {
            ItemFrameControl = new TaskItemFrameControl( item );
        }

        #endregion Constructors
    }
}