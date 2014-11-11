using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Modules;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemControl : ViewItemControl1D
    {
        public MotivationItemControl( IViewItem item )
            : base( item )
        {
            ItemFrameControl  = new MotivationItemFrameControl( item );
            
            ItemSymbolControl = new MotivationItemSymbolControl( item );
            ItemTaskControl   = new MotivationItemTaskControl( item );
        }

        public MotivationItemTaskControl ItemTaskControl { get; set; }

        public IPickableFrame SymbolFrame { get { return ItemFrameControl.SymbolFrame; } }
        public IModule        Tracer      { get { return ItemTaskControl.TaskTracer; } }

        private MotivationItemSymbolControl ItemSymbolControl { get; set; }

        public void DeleteIt()
        {
            // remove from gui
            View.Items.Remove( Item );
            // remove from data source
            View.Control.ItemFactory.RemoveData( Item );
        }

        public override void SelectIt()
        {
            base           .SelectIt();
            ItemTaskControl.SelectIt();
        }

        public override void UnselectIt()
        {
            if ( ItemTaskControl.UnselectIt() )
            {
                base.UnselectIt();
            }
        }

        public override void UpdateInput( GameTime gameTime )
        {
            // mouse
            //-----------------------------------------------------------------
            ItemFrameControl .UpdateInput( gameTime );

            // keyboard
            //-----------------------------------------------------------------
            if ( Item.IsEnabled( ItemState.Item_Selected ) &&
                !Item.IsEnabled( ItemState.Item_Editing ) )
            {
                // normal status
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.MotivationEditItem ) )
                {
                    View.Enable( ViewState.Item_Editting );
                    Item.Enable( ItemState.Item_Pending );
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.MotivationDeleteItem ) )
                    DeleteIt();
                // in pending status
                if ( Item.IsEnabled( ItemState.Item_Pending ) )
                {
                    if ( InputSequenceManager.Keyboard.IsKeyTriggered( Keys.N ) )
                        ItemDataControl.EditString( "Name" );
                    if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Escape ) )
                    {
                        View.Disable( ViewState.Item_Editting );
                        Item.Disable( ItemState.Item_Pending );
                    }
                }
            }
            ItemSymbolControl.UpdateInput( gameTime );
            ItemTaskControl  .UpdateInput( gameTime );
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            base             .UpdateStructure( gameTime );
            ItemSymbolControl.UpdateStructure( gameTime );
            ItemTaskControl  .UpdateStructure( gameTime );
        }
    }
}