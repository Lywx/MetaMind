using System;
using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemSymbolControl : ViewItemComponent
    {
        private MotivationType type;

        public MotivationItemSymbolControl( IViewItem item )
            : base( item )
        {
            type  = MotivationType.Neutral;
        }

        public MotivationType Type
        {
            get { return type; }
        }

        public void Update( GameTime gameTime )
        {
            UpdateInput( gameTime );
            UpdateStructure( gameTime );
        }

        private void UpdateStructure( GameTime gameTime )
        {
            
        }

        private void UpdateInput( GameTime gameTime )
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) )
            {
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Up ) )
                    BecomeWish();
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Down ) )
                    BecomeFear();
            }
        }

        public void BecomeWish()
        {
            type = MotivationType.Wish;
        }

        public void BecomeFear()
        {
            type = MotivationType.Fear;
        }
    }
}