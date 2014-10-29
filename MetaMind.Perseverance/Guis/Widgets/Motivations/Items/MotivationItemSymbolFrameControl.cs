using System;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemSymbolFrameControl : ViewItemFrameControl
    {
        private TimeSpan selectedTime;

        public MotivationItemSymbolFrameControl( IViewItem item )
            : base( item )
        {
            SymbolFrame = new PickableFrame();
        }

        public PickableFrame SymbolFrame { get; private set; }

        private Point SymbolFrameSize
        {
            get
            {
                return new Point(
                    ( int ) ( RootFrame.Rectangle.Width * ( 1 + ItemSettings.SymbolFrameIncrementFactor * Math.Abs( Math.Atan( selectedTime.TotalSeconds ) ) ) ),
                    ( int ) ( RootFrame.Rectangle.Height * ( 1 + ItemSettings.SymbolFrameIncrementFactor * Math.Abs( Math.Atan( selectedTime.TotalSeconds ) ) ) ) );
            }
        }

        public override void Update( GameTime gameTime )
        {
            base.Update( gameTime );

            UpdateSelection( gameTime );
        }

        protected override void UpdateFrames( GameTime gameTime )
        {
            base.UpdateFrames( gameTime );

            SymbolFrame.Mimic( RootFrame, SymbolFrameSize );
        }

        private void UpdateSelection( GameTime gameTime )
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) &&
                !Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                selectedTime = selectedTime + gameTime.DeltaTimeSpan( 2 );
            }
            else
            {
                if ( selectedTime.Ticks > 0 )
                    selectedTime = selectedTime - gameTime.DeltaTimeSpan( 5 );
                else
                    selectedTime = TimeSpan.Zero;
            }
        }
    }
}