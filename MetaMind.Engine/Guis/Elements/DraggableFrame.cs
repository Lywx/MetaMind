// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DraggableFrame.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
    using System;

    using Microsoft.Xna.Framework;

    using Stateless;

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
            // State machine
            this.StateMachine = new StateMachine<State, Trigger>(State.Released);

            // Possible cross interference 
            this.StateMachine.Configure(State.Released).PermitReentry(Trigger.Released);
            this.StateMachine.Configure(State.Released).Permit(Trigger.Pressed, State.Pressing);
            this.StateMachine.Configure(State.Released).Ignore(Trigger.DraggedWithinRange);
            this.StateMachine.Configure(State.Released).Ignore(Trigger.DraggedOutOfRange);

            // Possible cross interference
            this.StateMachine.Configure(State.Pressing).PermitReentry(Trigger.Pressed);
            this.StateMachine.Configure(State.Pressing).Permit(Trigger.Released, State.Released);
            this.StateMachine.Configure(State.Pressing).Permit(Trigger.DraggedWithinRange, State.Holding);
            this.StateMachine.Configure(State.Pressing).Permit(Trigger.DraggedOutOfRange, State.Dragging);

            this.StateMachine.Configure(State.Holding).PermitReentry(Trigger.DraggedWithinRange);
            this.StateMachine.Configure(State.Holding).Permit(Trigger.DraggedOutOfRange, State.Dragging);

            // Possible cross interference
            this.StateMachine.Configure(State.Holding).Permit(Trigger.Pressed, State.Pressing);
            this.StateMachine.Configure(State.Holding).Permit(Trigger.Released, State.Released);

            // Possible cross interference
            this.StateMachine.Configure(State.Dragging).Permit(Trigger.Pressed, State.Released);
            this.StateMachine.Configure(State.Dragging).Permit(Trigger.Released, State.Released);
            this.StateMachine.Configure(State.Dragging).Ignore(Trigger.DraggedOutOfRange);
            this.StateMachine.Configure(State.Dragging).Ignore(Trigger.DraggedWithinRange);

            this.StateMachine.Configure(State.Dragging).OnEntry(() =>
                {
                    this.DeferAction(this.OnMouseDragged);
                });

            this.StateMachine.Configure(State.Dragging).OnExit(() =>
                {
                    this.DeferAction(this.OnMouseDropped);
                });

            // Events
            this.MouseLeftPressed  += this.FrameMousePressed;
            this.MouseLeftReleased += this.FrameMouseReleased;

            this.MouseRightPressed  += this.FrameMousePressed;
            this.MouseRightReleased += this.FrameMouseReleased;

            // States
            this[FrameState.Frame_Is_Holding] = () => this.StateMachine.IsInState(State.Holding);
            this[FrameState.Frame_Is_Dragging] = () => this.StateMachine.IsInState(State.Dragging);
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
            this.MouseLeftPressed   -= this.FrameMousePressed;
            this.MouseRightPressed  -= this.FrameMousePressed;
            
            this.MouseLeftReleased  -= this.FrameMouseReleased;
            this.MouseRightReleased -= this.FrameMouseReleased;

            base.Dispose();
        }

        #endregion

        #region State Machine

        protected enum State
        {
            Pressing,

            Holding,

            Dragging,

            Released,
        }

        protected enum Trigger
        {
            Pressed,

            DraggedWithinRange,

            DraggedOutOfRange,

            Released,
        }

        protected StateMachine<State, Trigger> StateMachine { get; set; }

        #endregion

        #region Events

        public event EventHandler<FrameEventArgs> MouseDragged;

        public event EventHandler<FrameEventArgs> MouseDropped;

        private void OnMouseDropped()
        {
            if (this.MouseDropped != null)
            {
                this.MouseDropped(this, new FrameEventArgs(FrameEventType.Frame_Dropped));
            }
        }

        private void OnMouseDragged()
        {
            if (this.MouseDragged != null)
            {
                this.MouseDragged(this, new FrameEventArgs(FrameEventType.Frame_Dragged));
            }
        }

        private void FrameMousePressed(object sender, FrameEventArgs e)
        {
            var mouse = InputState.Mouse.CurrentState;

            // origin for deciding whether is dragging
            this.mousePressedPosition = new Point(mouse.X, mouse.Y);

            // save relative position of mouse compared to rectangle
            // mouse y-axis value is fixed at y-axis center of the rectangle
            this.mouseRelativePosition = new Point(mouse.X - this.Rectangle.X, mouse.Y - this.Rectangle.Y);

            this.StateMachine.Fire(Trigger.Pressed);
        }

        private void FrameMouseReleased(object sender, EventArgs e)
        {
            this.StateMachine.Fire(Trigger.Released);
        }

        #endregion Events

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            var mouse = this.GameInput.State.Mouse.CurrentState;
            this.mouseLocation = new Point(mouse.X, mouse.Y);

            if (this.StateMachine.IsInState(State.Dragging))
            {
                // Keep rectangle relative position to the mouse position from changing 
                this.Rectangle = new Rectangle(
                    this.mouseLocation.X - this.mouseRelativePosition.X, 
                    this.mouseLocation.Y - this.mouseRelativePosition.Y, 
                    this.Rectangle.Width, 
                    this.Rectangle.Height);
            }

            var isOutOfHoldLen = this.mouseLocation.DistanceFrom(this.mousePressedPosition).Length() > this.mouseHoldLen;

            this.StateMachine.Fire(isOutOfHoldLen ? Trigger.DraggedOutOfRange : Trigger.DraggedWithinRange);
        }

        #endregion Update
    }
}