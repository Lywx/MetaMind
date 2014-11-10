using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
            ItemErrorControl = new TaskItemErrorControl( item );
        }

        public TaskItemErrorControl ItemErrorControl { get; private set; }

        public override void UpdateInput( GameTime gameTime )
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) &&
                !Item.IsEnabled( ItemState.Item_Editing ) )
            {
                // normal status
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.TaskEditItem ) )
                {
                    View.Enable( ViewState.Item_Editting );
                    Item.Enable( ItemState.Item_Pending );
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.TaskDeleteItem ) )
                    DeleteIt();
                // in pending status
                if ( Item.IsEnabled( ItemState.Item_Pending ) )
                {
                    if ( InputSequenceManager.Keyboard.IsKeyTriggered( Keys.N ) )
                        ItemDataControl.EditString( "Name" );
                    if ( InputSequenceManager.Keyboard.IsKeyTriggered( Keys.D ) )
                        ItemDataControl.EditInt( "Done" );
                    if ( InputSequenceManager.Keyboard.IsKeyTriggered( Keys.E ) )
                        ItemDataControl.EditExperience( "Experience" );
                    if ( InputSequenceManager.Keyboard.IsKeyTriggered( Keys.L ) )
                        ItemDataControl.EditInt( "Load" );
                    if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Escape ) )
                    {
                        View.Disable( ViewState.Item_Editting );
                        Item.Disable( ItemState.Item_Pending );
                    }
                }
            }
        }

        private void DeleteIt()
        {
            // remove from gui
            View.Items.Remove( Item );
            // remove from data source
            View.Control.ItemFactory.RemoveData( Item );
        }

        #endregion Constructors
    }
}