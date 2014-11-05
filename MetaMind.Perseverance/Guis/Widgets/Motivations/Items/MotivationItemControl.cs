using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemControl : ViewItemControl1D
    {
        private MotivationItemSymbolControl ItemSymbolControl { get; set; }

        public MotivationItemControl( IViewItem item )
            : base( item )
        {
            ItemFrameControl  = new MotivationItemSymbolFrameControl( item );
            ItemSymbolControl = new MotivationItemSymbolControl( item );
            ItemDataControl   = new ViewItemDataControl( item );
        }

        public MotivationType Type
        {
            get { return ItemSymbolControl.Type; }
        }

        public IPickableFrame SymbolFrame
        {
            get { return ItemFrameControl.SymbolFrame; }
        }

        public override void Update( GameTime gameTime )
        {
            UpdateInput( gameTime );
            UpdateStructure( gameTime );
        }

        private void UpdateStructure( GameTime gameTime )
        {
            base.Update( gameTime );
        }

        public void UpdateInput( GameTime gameTime )
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) )
            {
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.EditItem ) )
                    ItemDataControl.EditString( "Name" );
            }
        }
    }
}