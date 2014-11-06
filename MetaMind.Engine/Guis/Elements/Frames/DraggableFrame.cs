using System;
using MetaMind.Engine.Extensions;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Frames
{
    public class DraggableFrame : PickableFrame, IDraggableFrame
    {
        #region Control Data

        private const int holdLen = 6;

        private Point PressedPosition { get; set; }
        private Point RelativePosition { get; set; }

        #endregion Control Data

        #region Constructors

        public DraggableFrame( Rectangle rectangle )
            : this()
        {
            Initialize( rectangle );
        }

        public DraggableFrame()
        {
            MouseLeftPressed += RecordPressPosition;
            MouseLeftReleased += ResetRecordPosition;

            MouseRightPressed += RecordPressPosition;
            MouseRightReleased += ResetRecordPosition;
        }

        #endregion Constructors

        #region Events

        public event EventHandler<FrameEventArgs> MouseDragged;
        public event EventHandler<FrameEventArgs> MouseDropped;

        private void RecordPressPosition( object sender, EventArgs e )
        {
            var mouse = InputSequenceManager.Mouse.CurrentState;

            // origin for deciding whether is dragging
            PressedPosition = new Point( mouse.X, mouse.Y );

            // save relative position of mouse compared to rectangle
            // mouse y-axis value is fixed at y-axis center of the rectangle
            RelativePosition = new Point( mouse.X - Rectangle.X, mouse.Y - Rectangle.Y );

            // ready to decide
            Enable( FrameState.Frame_Holding );

            // cancel with another button
            if ( IsEnabled( FrameState.Frame_Dragging ) )
            {
                Disable( FrameState.Frame_Holding );
                Disable( FrameState.Frame_Dragging );
            }
        }

        private void ResetRecordPosition( object sender, EventArgs e )
        {
            if ( IsEnabled( FrameState.Frame_Dragging ) && MouseDropped != null )
                MouseDropped( this, new FrameEventArgs( FrameEventType.Frame_Dropped ) );
            // stop deciding
            Disable( FrameState.Frame_Holding );
            Disable( FrameState.Frame_Dragging );
        }

        #endregion Events

        #region Update

        public override void Update( GameTime gameTime )
        {
            base.Update( gameTime );

            var mouse = InputSequenceManager.Mouse.CurrentState;
            var mouseLocation = new Point( mouse.X, mouse.Y );

            UpdateFrameDraggingRectangle( mouseLocation );
        }

        protected override void UpdateStates( Point mouseLocation )
        {
            base.UpdateStates( mouseLocation );

            UpdateFrameDraggingState( mouseLocation );
        }

        private void UpdateFrameDraggingRectangle( Point mouseLocation )
        {
            if ( IsEnabled( FrameState.Frame_Dragging ) )
            {
                // keep up rectangle position with the mouse position
                Rectangle = new Rectangle(
                    mouseLocation.X - RelativePosition.X,
                    mouseLocation.Y - RelativePosition.Y,
                    Rectangle.Width,
                    Rectangle.Height
                    );
            }
        }

        private void UpdateFrameDraggingState( Point mouseLocation )
        {
            var isWithinHoldLen = ( mouseLocation.DistanceFrom( PressedPosition ) ).Length() > holdLen;

            // decide whether is dragging
            if ( IsEnabled( FrameState.Frame_Holding ) && isWithinHoldLen )
            {
                // stop deciding
                Disable( FrameState.Frame_Holding );
                // successfully drag
                Enable( FrameState.Frame_Dragging );

                if ( MouseDragged != null )
                    MouseDragged( this, new FrameEventArgs( FrameEventType.Frame_Dragged ) );
            }
        }

        #endregion Update
    }
}