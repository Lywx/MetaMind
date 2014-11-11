using System;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    public class MotivationItemFrameControl : ViewItemFrameControl
    {
        private TimeSpan selectedTime;

        public MotivationItemFrameControl( IViewItem item )
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
                    ( int ) ( RootFrame.Width  * ( 1 + ItemSettings.SymbolFrameIncrementFactor * Math.Abs( Math.Atan( selectedTime.TotalSeconds ) ) ) ),
                    ( int ) ( RootFrame.Height * ( 1 + ItemSettings.SymbolFrameIncrementFactor * Math.Abs( Math.Atan( selectedTime.TotalSeconds ) ) ) ) );
            }
        }


        public override void UpdateStructure(GameTime gameTime)
        {
            base.UpdateStructure( gameTime );
            
            UpdateFrameSelection( gameTime );
        }

        protected override void UpdateFrameGeometry()
        {
            base.UpdateFrameGeometry();

            SymbolFrame.Center = RootFrame.Center;
            SymbolFrame.Size   = SymbolFrameSize;
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base       .UpdateInput( gameTime );
            SymbolFrame.UpdateInput( gameTime );
        }

        private void UpdateFrameSelection( GameTime gameTime )
        {
            if ( Item.IsEnabled( ItemState.Item_Selected ) &&
                !Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                selectedTime = selectedTime + gameTime.DeltaTimeSpan( 2 );
            }
            else
            {
                if ( selectedTime.Ticks > 0 )
                {
                    selectedTime = selectedTime - gameTime.DeltaTimeSpan( 5 );
                }
                else
                {
                    selectedTime = TimeSpan.Zero;
                }
            }
        }
    }
}