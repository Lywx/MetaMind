using System;
using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingWidgets
{
    public class FeelingItemSymbolControl : ViewItemComponent
    {
        private FeelingType type;
        private FeelingItemSymbolFrameControl frame;

        public FeelingItemSymbolControl( IViewItem item )
            : base( item )
        {
            type  = FeelingType.Neutral;
            frame = new FeelingItemSymbolFrameControl( item );
        }

        public FeelingType Type
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
            type = FeelingType.Wish;
        }

        public void BecomeFear()
        {
            type = FeelingType.Fear;
        }

        public IPickableFrame SymbolFrame
        {
            get { return frame.SymbolFrame; }
        }
    }
}