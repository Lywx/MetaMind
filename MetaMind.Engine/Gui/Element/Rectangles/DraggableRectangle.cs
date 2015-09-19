// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DraggableFrame.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Element.Rectangles
{
    using System;
    using Microsoft.Xna.Framework;
    using Stateless;

    public class DraggableRectangle : PickableRectangle, IDraggableRectangle
    {
        private readonly int mouseHoldDistance = 6;

        private Point mouseLocation;

        private Point mousePressedPosition;

        /// <summary>
        /// Mouse position relative to rectangle
        /// </summary>
        private Point mouseRelativePosition;

        #region Properties

        private bool movable;

        public bool Movable
        {
            get { return this.movable; }
            set
            {
                var changed = value != this.movable;
                if (changed)
                {
                    if (value)
                    {
                        this.RegisterHandlers();
                    }
                    else
                    {
                        this.DisposeHandlers();
                    }
                }

                this.movable = value;
            }
        }

        #endregion

        #region Constructors and Finalizer

        public DraggableRectangle()
        {
            this.Constructor();
        }

        public DraggableRectangle(Rectangle bounds) 
            : base(bounds)
        {
            this.Constructor();
        }

        ~DraggableRectangle()
        {
            this.Dispose(true);
        }

        #endregion

        #region State Machine

        protected enum FrameMachineState
        {
            Pressing,

            Holding,

            Dragging,

            Released,
        }

        protected enum FrameMachineTrigger
        {
            Pressed,

            DraggedWithinRange,

            DraggedOutOfRange,

            Released,
        }

        protected StateMachine<FrameMachineState, FrameMachineTrigger> FrameMachine { get; private set; }

        #endregion

        #region Events

        public event EventHandler<ElementEventArgs> MouseDrag;

        public event EventHandler<ElementEventArgs> MouseDrop;

        #endregion

        #region Event Handlers

        private void FrameMousePress(object sender, ElementEventArgs e)
        {
            var mouse = this.Input.State.Mouse.CurrentState;

            // origin for deciding whether is dragging
            this.mousePressedPosition = new Point(mouse.X, mouse.Y);

            // save relative position of mouse compared to rectangle
            // mouse y-axis value is fixed at y-axis center of the rectangle
            this.mouseRelativePosition = new Point(mouse.X - this.Bounds.X, mouse.Y - this.Bounds.Y);

            this.FrameMachine.Fire(FrameMachineTrigger.Pressed);
        }

        private void FrameMouseUp(object sender, EventArgs e)
        {
            this.FrameMachine.Fire(FrameMachineTrigger.Released);
        }

        #endregion

        #region Event On

        private void OnMouseDropped()
        {
            this.MouseDrop?.Invoke(this, new ElementEventArgs(ElementEvent.Element_Drop));
        }

        private void OnMouseDragged()
        {
            this.MouseDrag?.Invoke(this, new ElementEventArgs(ElementEvent.Element_Drag));
        }

        #endregion

        #region Event Registration

        private void RegisterHandlers()
        {
            this.MousePress += this.FrameMousePress;

            // Perform the same for inside or outside frame mouse up.
            this.MouseUp    += this.FrameMouseUp;
            this.MouseUpOut += this.FrameMouseUp;
        }

        #endregion

        #region Initialization

        private void Constructor()
        {
            this.InitializeMachine();
            this.InitializeStates();
        }

        private void InitializeMachine()
        {
            this.FrameMachine = new StateMachine<FrameMachineState, FrameMachineTrigger>(FrameMachineState.Released);

            // Possible cross interference 
            this.FrameMachine.Configure(FrameMachineState.Released).PermitReentry(FrameMachineTrigger.Released);
            this.FrameMachine.Configure(FrameMachineState.Released).Permit(FrameMachineTrigger.Pressed, FrameMachineState.Pressing);
            this.FrameMachine.Configure(FrameMachineState.Released).Ignore(FrameMachineTrigger.DraggedWithinRange);
            this.FrameMachine.Configure(FrameMachineState.Released).Ignore(FrameMachineTrigger.DraggedOutOfRange);

            // Possible cross interference
            this.FrameMachine.Configure(FrameMachineState.Pressing).PermitReentry(FrameMachineTrigger.Pressed);
            this.FrameMachine.Configure(FrameMachineState.Pressing).Permit(FrameMachineTrigger.Released, FrameMachineState.Released);
            this.FrameMachine.Configure(FrameMachineState.Pressing).Permit(FrameMachineTrigger.DraggedWithinRange, FrameMachineState.Holding);
            this.FrameMachine.Configure(FrameMachineState.Pressing).Permit(FrameMachineTrigger.DraggedOutOfRange, FrameMachineState.Dragging);

            this.FrameMachine.Configure(FrameMachineState.Holding).PermitReentry(FrameMachineTrigger.DraggedWithinRange);
            this.FrameMachine.Configure(FrameMachineState.Holding).Permit(FrameMachineTrigger.DraggedOutOfRange, FrameMachineState.Dragging);

            // Possible cross interference
            this.FrameMachine.Configure(FrameMachineState.Holding).Permit(FrameMachineTrigger.Pressed, FrameMachineState.Pressing);
            this.FrameMachine.Configure(FrameMachineState.Holding).Permit(FrameMachineTrigger.Released, FrameMachineState.Released);

            // Possible cross interference
            this.FrameMachine.Configure(FrameMachineState.Dragging).Permit(FrameMachineTrigger.Pressed, FrameMachineState.Released);
            this.FrameMachine.Configure(FrameMachineState.Dragging).Permit(FrameMachineTrigger.Released, FrameMachineState.Released);
            this.FrameMachine.Configure(FrameMachineState.Dragging).Ignore(FrameMachineTrigger.DraggedOutOfRange);
            this.FrameMachine.Configure(FrameMachineState.Dragging).Ignore(FrameMachineTrigger.DraggedWithinRange);

            this.FrameMachine.Configure(FrameMachineState.Dragging).OnEntry(() =>
                {
                    this.DeferAction(this.OnMouseDragged);
                });

            this.FrameMachine.Configure(FrameMachineState.Dragging).OnExit(() =>
                {
                    this.DeferAction(this.OnMouseDropped);
                });
        }

        private void InitializeStates()
        {
            this[ElementState.Element_Is_Holding] = () => this.FrameMachine.IsInState(FrameMachineState.Holding);
            this[ElementState.Element_Is_Dragging] = () => this.FrameMachine.IsInState(FrameMachineState.Dragging);
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (!this.Movable)
            {
                return;
            }

            this.UpdateFramePosition();
            this.UpdateFrameStates();
        }

        private void UpdateFramePosition()
        {
            var mouse = this.Input.State.Mouse.CurrentState;
            this.mouseLocation = new Point(mouse.X, mouse.Y);

            if (this.FrameMachine.IsInState(FrameMachineState.Dragging))
            {
                // Keep rectangle relative position to the mouse position from 
                // changing 
                this.Bounds = new Rectangle(
                    this.mouseLocation.X - this.mouseRelativePosition.X,
                    this.mouseLocation.Y - this.mouseRelativePosition.Y,
                    this.Bounds.Width,
                    this.Bounds.Height);
            }
        }

        private void UpdateFrameStates()
        {
            var isOutOfHoldLen =
                this.mouseLocation.DistanceFrom(this.mousePressedPosition).
                     Length() > this.mouseHoldDistance;

            this.FrameMachine.Fire(
                isOutOfHoldLen ? FrameMachineTrigger.DraggedOutOfRange : FrameMachineTrigger.DraggedWithinRange);
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
            this.MouseDrag = null;
            this.MouseDrop = null;
        }

        private void DisposeHandlers()
        {
            this.MousePress -= this.FrameMousePress;

            this.MouseUp    -= this.FrameMouseUp;
            this.MouseUpOut -= this.FrameMouseUp;
        }

        #endregion
    }
}