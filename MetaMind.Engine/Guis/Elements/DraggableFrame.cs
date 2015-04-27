// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DraggableFrame.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
    using System;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class DraggableFrame : PickableFrame, IDraggableFrame
    {
        private readonly int mouseHoldLen = 6;

        private Point mouseLocation;

        private Point mousePressedPosition;

        /// <summary>
        /// Mouse position relative to rectangle
        /// </summary>
        private Point mouseRelativePosition;

        public DraggableFrame(Rectangle rectangle)
            : this()
        {
            this.Populate(rectangle);
        }

        protected DraggableFrame()
        {
            this.MouseLeftPressed   += this.RecordPressPosition;
            this.MouseLeftReleased  += this.ResetRecordPosition;

            this.MouseRightPressed  += this.RecordPressPosition;
            this.MouseRightReleased += this.ResetRecordPosition;
        }

        ~DraggableFrame()
        {
            this.Dispose();
        }

        #region IDiposable

        public override void Dispose()
        {
            // Clean events
            this.MouseDragged = null;
            this.MouseDropped = null;

            // Clean handlers
            this.MouseLeftPressed   -= this.RecordPressPosition;
            this.MouseLeftReleased  -= this.ResetRecordPosition;
            this.MouseRightPressed  -= this.RecordPressPosition;
            this.MouseRightReleased -= this.ResetRecordPosition;

            base.Dispose();
        }

        #endregion 

        #region Events

        public event EventHandler<FrameEventArgs> MouseDragged;

        public event EventHandler<FrameEventArgs> MouseDropped;

        private void RecordPressPosition(object sender, FrameEventArgs e)
        {
            var mouse = InputState.Mouse.CurrentState;

            // origin for deciding whether is dragging
            this.mousePressedPosition = new Point(mouse.X, mouse.Y);

            // save relative position of mouse compared to rectangle
            // mouse y-axis value is fixed at y-axis center of the rectangle
            this.mouseRelativePosition = new Point(mouse.X - this.Rectangle.X, mouse.Y - this.Rectangle.Y);

            // ready to decide
            this[FrameState.Frame_Is_Holding] = () => true;

            // cancel with another button
            if (this[FrameState.Frame_Is_Dragging]())
            {
                this[FrameState.Frame_Is_Holding] = () => false;
                this[FrameState.Frame_Is_Dragging] = () => false;
            }
        }

        private void ResetRecordPosition(object sender, EventArgs e)
        {
            if (this[FrameState.Frame_Is_Dragging]() && this.MouseDropped != null)
            {
                this.MouseDropped(this, new FrameEventArgs(FrameEventType.Frame_Dropped));
            }

            // Stop deciding
            this[FrameState.Frame_Is_Holding] = () => false;
            this[FrameState.Frame_Is_Dragging] = () => false;
        }

        #endregion Events

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            var mouse = input.State.Mouse.CurrentState;
            this.mouseLocation = new Point(mouse.X, mouse.Y);

            if (this[FrameState.Frame_Is_Dragging]())
            {
                // keep up rectangle position with the mouse position
                this.Rectangle = new Rectangle(
                    this.mouseLocation.X - this.mouseRelativePosition.X, 
                    this.mouseLocation.Y - this.mouseRelativePosition.Y, 
                    this.Rectangle.Width, 
                    this.Rectangle.Height);
            }
        }

        public override void Update(GameTime time)
        {
            var isWithinHoldLen = mouseLocation.DistanceFrom(this.mousePressedPosition).Length() > mouseHoldLen;

            // decide whether is dragging
            if (this[FrameState.Frame_Is_Holding]() && isWithinHoldLen)
            {
                // stop deciding
                this[FrameState.Frame_Is_Holding] = () => false;

                // successfully drag
                this[FrameState.Frame_Is_Dragging] = () => true;

                if (this.MouseDragged != null)
                {
                    this.MouseDragged(this, new FrameEventArgs(FrameEventType.Frame_Dragged));
                }
            }
        }

        #endregion Update
    }
}