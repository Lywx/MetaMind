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
        private MotivationItemSymbolFrameControl frame;

        public MotivationItemSymbolControl( IViewItem item )
            : base( item )
        {
            type  = MotivationType.Neutral;
            frame = new MotivationItemSymbolFrameControl( item );
        }

        public MotivationType Type
        {
            get { return type; }
        }

        public TimeSpan SelectedTime
        {
            get { return frame.SelectedTime; }
        }

        public void Update( GameTime gameTime )
        {
            UpdateInput( gameTime );
            UpdateStructure( gameTime );
        }

        private void UpdateStructure( GameTime gameTime )
        {
            frame.Update( gameTime );
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

        public IPickableFrame SymbolFrame
        {
            get { return frame.SymbolFrame; }
        }
    }
}