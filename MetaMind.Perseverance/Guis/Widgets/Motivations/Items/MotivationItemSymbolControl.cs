using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemSymbolControl : ViewItemComponent
    {
        public MotivationItemSymbolControl( IViewItem item )
            : base( item )
        {
        }

        public void UpdateStructure( GameTime gameTime )
        {
        }

        public void UpdateInput( GameTime gameTime )
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) &&
                !Item.IsEnabled( ItemState.Item_Editing ) )
            {
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Up ) )
                    BecomeWish();
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Down ) )
                    BecomeFear();
            }
        }

        public void BecomeWish()
        {
            ItemData.Property = "Wish";
        }

        public void BecomeFear()
        {
            ItemData.Property = "Fear";
        }
    }
}