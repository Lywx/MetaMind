using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemControl2D : ViewItemComponent
    {
        //---------------------------------------------------------------------

        protected dynamic               ItemFrameControl { get; set; }
        protected ViewItemViewControl2D ItemViewControl  { get; set; }
        protected ViewItemDataControl   ItemDataControl  { get; set; }
        
        //---------------------------------------------------------------------

        #region Constructors

        public ViewItemControl2D( IViewItem item )
            : base( item )
        {
            ItemFrameControl = new ViewItemFrameControl( item );
            ItemViewControl  = new ViewItemViewControl2D( item );
            ItemDataControl  = new ViewItemDataControl( item );
        }


        #endregion Constructors

        #region Public Properties

        public int           Column    { get; set; }
        public int           Id        { get; set; }
        public int           Row       { get; set; }
        public ItemRootFrame RootFrame { get { return ItemFrameControl.RootFrame; } }

        #endregion Public Properties

        #region Operations

        public void SelectIt()
        {
            ItemViewControl.SelectIt();
        }

        public void SwapIt( IViewItem draggingItem )
        {
            ItemViewControl.SwapIt( draggingItem );
        }

        public void UnselectIt()
        {
            ItemViewControl.UnselectIt();
        }

        #endregion Operations

        #region Update

        public void Update( GameTime gameTime )
        {
            UpdateInput( gameTime );
            UpdateStructure( gameTime );
        }

        protected virtual void UpdateInput( GameTime gameTime )
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) &&
                !Item.IsEnabled( ItemState.Item_Editing ) )
            {
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.EditItem ) )
                    ItemDataControl.EditString( "Name" );
            }
        }

        protected virtual void UpdateStructure( GameTime gameTime )
        {
            ItemViewControl .Update( gameTime );
            ItemFrameControl.Update( gameTime );
            ItemDataControl .Update( gameTime );
        }

        #endregion Update
    }
}