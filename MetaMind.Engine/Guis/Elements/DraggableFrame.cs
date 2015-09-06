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

        #region Constructors and Finalizer

        public DraggableFrame(Rectangle rectangle)
            : this()
        {
            this.Populate(rectangle);
        }

        protected DraggableFrame()
        {
            // State machine
            this.Machine = new StateMachine<State, Trigger>(State.Released);

            // Possible cross interference 
            this.Machine.Configure(State.Released).PermitReentry(Trigger.Released);
            this.Machine.Configure(State.Released).Permit(Trigger.Pressed, State.Pressing);
            this.Machine.Configure(State.Released).Ignore(Trigger.DraggedWithinRange);
            this.Machine.Configure(State.Released).Ignore(Trigger.DraggedOutOfRange);

            // Possible cross interference
            this.Machine.Configure(State.Pressing).PermitReentry(Trigger.Pressed);
            this.Machine.Configure(State.Pressing).Permit(Trigger.Released, State.Released);
            this.Machine.Configure(State.Pressing).Permit(Trigger.DraggedWithinRange, State.Holding);
            this.Machine.Configure(State.Pressing).Permit(Trigger.DraggedOutOfRange, State.Dragging);

            this.Machine.Configure(State.Holding).PermitReentry(Trigger.DraggedWithinRange);
            this.Machine.Configure(State.Holding).Permit(Trigger.DraggedOutOfRange, State.Dragging);

            // Possible cross interference
            this.Machine.Configure(State.Holding).Permit(Trigger.Pressed, State.Pressing);
            this.Machine.Configure(State.Holding).Permit(Trigger.Released, State.Released);

            // Possible cross interference
            this.Machine.Configure(State.Dragging).Permit(Trigger.Pressed, State.Released);
            this.Machine.Configure(State.Dragging).Permit(Trigger.Released, State.Released);
            this.Machine.Configure(State.Dragging).Ignore(Trigger.DraggedOutOfRange);
            this.Machine.Configure(State.Dragging).Ignore(Trigger.DraggedWithinRange);

            this.Machine.Configure(State.Dragging).OnEntry(() =>
                {
                    this.DeferAction(this.OnMouseDragged);
                });

            this.Machine.Configure(State.Dragging).OnExit(() =>
                {
                    this.DeferAction(this.OnMouseDropped);
                });

            this.RegisterHandlers();

            // States
            this[FrameState.Frame_Is_Holding] = () => this.Machine.IsInState(State.Holding);
            this[FrameState.Frame_Is_Dragging] = () => this.Machine.IsInState(State.Dragging);
        }

        ~DraggableFrame()
        {
            this.Dispose(true);
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

        protected StateMachine<State, Trigger> Machine { get; private set; }

        #endregion

        #region Events

        public event EventHandler<FrameEventArgs> MouseDragged;

        public event EventHandler<FrameEventArgs> MouseDropped;

        private void FrameMousePressed(object sender, FrameEventArgs e)
        {
            var mouse = this.Input.State.Mouse.CurrentState;

            // origin for deciding whether is dragging
            this.mousePressedPosition = new Point(mouse.X, mouse.Y);

            // save relative position of mouse compared to rectangle
            // mouse y-axis value is fixed at y-axis center of the rectangle
            this.mouseRelativePosition = new Point(mouse.X - this.Rectangle.X, mouse.Y - this.Rectangle.Y);

            this.Machine.Fire(Trigger.Pressed);
        }

        private void FrameMouseReleased(object sender, EventArgs e)
        {
            this.Machine.Fire(Trigger.Released);
        }

        private void OnMouseDropped()
        {
            this.MouseDropped?.Invoke(this, new FrameEventArgs(FrameEventType.Frame_Dropped));
        }

        private void OnMouseDragged()
        {
            this.MouseDragged?.Invoke(this, new FrameEventArgs(FrameEventType.Frame_Dragged));
        }

        private void RegisterHandlers()
        {
            this.MousePressed += this.FrameMousePressed;
            this.MouseReleased += this.FrameMouseReleased;
        }

        #endregion Events

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            var mouse = this.Input.State.Mouse.CurrentState;
            this.mouseLocation = new Point(mouse.X, mouse.Y);

            if (this.Machine.IsInState(State.Dragging))
            {
                // Keep rectangle relative position to the mouse position from changing 
                this.Rectangle = new Rectangle(
                    this.mouseLocation.X - this.mouseRelativePosition.X, 
                    this.mouseLocation.Y - this.mouseRelativePosition.Y, 
                    this.Rectangle.Width, 
                    this.Rectangle.Height);
            }

            var isOutOfHoldLen = this.mouseLocation.DistanceFrom(this.mousePressedPosition).Length() > this.mouseHoldLen;

            this.Machine.Fire(isOutOfHoldLen ? Trigger.DraggedOutOfRange : Trigger.DraggedWithinRange);
        }

        #endregion Update

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.DisposeEvents();
                        this.DisposeHandlers();
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void DisposeEvents()
        {
            this.MouseDragged = null;
            this.MouseDropped = null;
        }

        private void DisposeHandlers()
        {
            this.MouseLeftPressed -= this.FrameMousePressed;
            this.MouseRightPressed -= this.FrameMousePressed;

            this.MouseLeftReleased -= this.FrameMouseReleased;
            this.MouseRightReleased -= this.FrameMouseReleased;
        }

        #endregion
    }
}