using System;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingWidgets
{
    public class FeelingItemSymbolFrameControl : ViewItemComponent
    {
        private const float IncrementFactor = 0.2f;
        
        private MimicFrame symbolFrame;
        private TimeSpan   selectedTime;

        private Point SymbolFrameSize
        {
            get
            {
                return new Point(
                    ( int ) ( ItemControl.RootFrame.Rectangle.Width * ( 1 + IncrementFactor * Math.Abs( Math.Atan( SelectedTime.TotalSeconds ) ) ) ),
                    ( int ) ( ItemControl.RootFrame.Rectangle.Height * ( 1 + IncrementFactor * Math.Abs( Math.Atan( SelectedTime.TotalSeconds ) ) ) ) );
            }
        }

        public FeelingItemSymbolFrameControl( IViewItem item )
            : base( item )
        {
            symbolFrame = new MimicFrame();
            selectedTime = TimeSpan.Zero;
        }

        public MimicFrame SymbolFrame
        {
            get { return symbolFrame; }
        }

        public TimeSpan SelectedTime
        {
            get { return selectedTime; }
        }

        public void Update( GameTime gameTime )
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) &&
                !Item.IsEnabled( ItemState.Item_Dragging ))
            {
                selectedTime = SelectedTime + gameTime.DeltaTimeSpan( 2 );
            }
            else
            {
                if ( SelectedTime.Ticks > 0 )
                    selectedTime = SelectedTime - gameTime.DeltaTimeSpan( 5 );
                else
                    selectedTime = TimeSpan.Zero;
            }
            symbolFrame.Mimic( ItemControl.RootFrame, SymbolFrameSize );
        }
    }
}