// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DraggableFrame.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements.Frames
{
    using System;

    using MetaMind.Engine.Extensions;

    using Microsoft.Xna.Framework;

    public class DraggableFrame : PickableFrame, IDraggableFrame
    {
        #region Control Data

        private const int holdLen = 6;

        private Point PressedPosition { get; set; }

        private Point RelativePosition { get; set; }

        #endregion Control Data

        #region Constructors

        public DraggableFrame(Rectangle rectangle)
            : this()
        {
            this.Initialize(rectangle);
        }

        public DraggableFrame()
        {
            this.MouseLeftPressed += this.RecordPressPosition;
            this.MouseLeftReleased += this.ResetRecordPosition;

            this.MouseRightPressed += this.RecordPressPosition;
            this.MouseRightReleased += this.ResetRecordPosition;
        }

        #endregion Constructors

        #region Events

        public event EventHandler<FrameEventArgs> MouseDragged;

        public event EventHandler<FrameEventArgs> MouseDropped;

        private void RecordPressPosition(object sender, EventArgs e)
        {
            var mouse = InputSequenceManager.Mouse.CurrentState;

            // origin for deciding whether is dragging
            this.PressedPosition = new Point(mouse.X, mouse.Y);

            // save relative position of mouse compared to rectangle
            // mouse y-axis value is fixed at y-axis center of the rectangle
            this.RelativePosition = new Point(mouse.X - this.Rectangle.X, mouse.Y - this.Rectangle.Y);

            // ready to decide
            this.Enable(FrameState.Frame_Holding);

            // cancel with another button
            if (this.IsEnabled(FrameState.Frame_Dragging))
            {
                this.Disable(FrameState.Frame_Holding);
                this.Disable(FrameState.Frame_Dragging);
            }
        }

        private void ResetRecordPosition(object sender, EventArgs e)
        {
            if (this.IsEnabled(FrameState.Frame_Dragging) && this.MouseDropped != null)
            {
                this.MouseDropped(this, new FrameEventArgs(FrameEventType.Frame_Dropped));
            }

            // stop deciding
            this.Disable(FrameState.Frame_Holding);
            this.Disable(FrameState.Frame_Dragging);
        }

        #endregion Events

        #region Update

        public override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

            var mouse = InputSequenceManager.Mouse.CurrentState;
            var mouseLocation = new Point(mouse.X, mouse.Y);

            if (this.IsEnabled(FrameState.Frame_Dragging))
            {
                // keep up rectangle position with the mouse position
                this.Rectangle = new Rectangle(
                    mouseLocation.X - this.RelativePosition.X, 
                    mouseLocation.Y - this.RelativePosition.Y, 
                    this.Rectangle.Width, 
                    this.Rectangle.Height);
            }
        }

        protected override void UpdateStates(Point mouseLocation)
        {
            base.UpdateStates(mouseLocation);

            var isWithinHoldLen = mouseLocation.DistanceFrom(this.PressedPosition).Length() > holdLen;

            // decide whether is dragging
            if (this.IsEnabled(FrameState.Frame_Holding) && isWithinHoldLen)
            {
                // stop deciding
                this.Disable(FrameState.Frame_Holding);

                // successfully drag
                this.Enable(FrameState.Frame_Dragging);

                if (this.MouseDragged != null)
                {
                    this.MouseDragged(this, new FrameEventArgs(FrameEventType.Frame_Dragged));
                }
            }
        }

        #endregion Update
    }
}