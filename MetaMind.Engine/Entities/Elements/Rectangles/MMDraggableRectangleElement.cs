namespace MetaMind.Engine.Entities.Elements.Rectangles
{
    using System;
    using Microsoft.Xna.Framework;
    using Stateless;

    public class MMDraggableRectangleElement : MMPickableRectangleElement, IMMDraggableRectangleElement
    {
        private readonly int mouseHoldDistance = 6;

        private Point mousePressedPosition;

        /// <summary>
        /// Mouse position relative to rectangle
        /// </summary>
        private Point mouseRelativePosition;

        #region Constructors and Finalizer

        public MMDraggableRectangleElement()
        {
            this.Setup();
        }

        public MMDraggableRectangleElement(Rectangle bounds) 
            : base(bounds)
        {
            this.Setup();
        }

        ~MMDraggableRectangleElement()
        {
            this.Dispose(true);
        }

        #endregion

        #region Element States

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

        #region State Machine

        protected enum RectangleMachineState
        {
            Pressing,

            Holding,

            Dragging,

            Released,
        }

        protected enum RectangleMachineTrigger
        {
            Pressed,

            DraggedWithinRange,

            DraggedOutOfRange,

            Released,
        }

        protected StateMachine<RectangleMachineState, RectangleMachineTrigger> RectangleStateMachine { get; private set; }

        #endregion

        #region Events

        public event EventHandler<MMInputElementDebugEventArgs> MouseDrag;

        public event EventHandler<MMInputElementDebugEventArgs> MouseDrop;

        #endregion

        #region Event Handlers

        private void FrameMousePress(object sender, MMInputElementDebugEventArgs e)
        {
            var mousePosition = this.GlobalInput.State.Mouse.Position;

            // origin for deciding whether is dragging
            this.mousePressedPosition = new Point(mousePosition.X, mousePosition.Y);

            // save relative position of mouse compared to rectangle
            // mouse y-axis value is fixed at y-axis center of the rectangle
            this.mouseRelativePosition = new Point(mousePosition.X - this.Bounds.X, mousePosition.Y - this.Bounds.Y);

            this.RectangleStateMachine.Fire(RectangleMachineTrigger.Pressed);
        }

        private void FrameMouseUp(object sender, EventArgs e)
        {
            this.RectangleStateMachine.Fire(RectangleMachineTrigger.Released);
        }

        #endregion

        #region Event On

        private void OnMouseDropped()
        {
            this.MouseDrop?.Invoke(this, new MMInputElementDebugEventArgs(MMInputElementDebugEvent.Element_Drop));
        }

        private void OnMouseDragged()
        {
            this.MouseDrag?.Invoke(this, new MMInputElementDebugEventArgs(MMInputElementDebugEvent.Element_Drag));
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

        private void Setup()
        {
            this.InitializeStateMachine();
            this.InitializeStateDebug();
        }

        private void InitializeStateMachine()
        {
            this.RectangleStateMachine = new StateMachine<RectangleMachineState, RectangleMachineTrigger>(RectangleMachineState.Released);

            // Possible cross interference 
            this.RectangleStateMachine.Configure(RectangleMachineState.Released).PermitReentry(RectangleMachineTrigger.Released);
            this.RectangleStateMachine.Configure(RectangleMachineState.Released).Permit(RectangleMachineTrigger.Pressed, RectangleMachineState.Pressing);
            this.RectangleStateMachine.Configure(RectangleMachineState.Released).Ignore(RectangleMachineTrigger.DraggedWithinRange);
            this.RectangleStateMachine.Configure(RectangleMachineState.Released).Ignore(RectangleMachineTrigger.DraggedOutOfRange);

            // Possible cross interference
            this.RectangleStateMachine.Configure(RectangleMachineState.Pressing).PermitReentry(RectangleMachineTrigger.Pressed);
            this.RectangleStateMachine.Configure(RectangleMachineState.Pressing).Permit(RectangleMachineTrigger.Released, RectangleMachineState.Released);
            this.RectangleStateMachine.Configure(RectangleMachineState.Pressing).Permit(RectangleMachineTrigger.DraggedWithinRange, RectangleMachineState.Holding);
            this.RectangleStateMachine.Configure(RectangleMachineState.Pressing).Permit(RectangleMachineTrigger.DraggedOutOfRange, RectangleMachineState.Dragging);

            this.RectangleStateMachine.Configure(RectangleMachineState.Holding).PermitReentry(RectangleMachineTrigger.DraggedWithinRange);
            this.RectangleStateMachine.Configure(RectangleMachineState.Holding).Permit(RectangleMachineTrigger.DraggedOutOfRange, RectangleMachineState.Dragging);

            // Possible cross interference
            this.RectangleStateMachine.Configure(RectangleMachineState.Holding).Permit(RectangleMachineTrigger.Pressed, RectangleMachineState.Pressing);
            this.RectangleStateMachine.Configure(RectangleMachineState.Holding).Permit(RectangleMachineTrigger.Released, RectangleMachineState.Released);

            // Possible cross interference
            this.RectangleStateMachine.Configure(RectangleMachineState.Dragging).Permit(RectangleMachineTrigger.Pressed, RectangleMachineState.Released);
            this.RectangleStateMachine.Configure(RectangleMachineState.Dragging).Permit(RectangleMachineTrigger.Released, RectangleMachineState.Released);
            this.RectangleStateMachine.Configure(RectangleMachineState.Dragging).Ignore(RectangleMachineTrigger.DraggedOutOfRange);
            this.RectangleStateMachine.Configure(RectangleMachineState.Dragging).Ignore(RectangleMachineTrigger.DraggedWithinRange);

            this.RectangleStateMachine.Configure(RectangleMachineState.Dragging).OnEntry(() =>
                {
                    this.InputCacher.CacheInput(this.OnMouseDragged);
                });

            this.RectangleStateMachine.Configure(RectangleMachineState.Dragging).OnExit(() =>
                {
                    this.InputCacher.CacheInput(this.OnMouseDropped);
                });
        }

        private void InitializeStateDebug()
        {
            this[MMInputElementDebugState.Element_Is_Holding] = () => this.RectangleStateMachine.IsInState(RectangleMachineState.Holding);
            this[MMInputElementDebugState.Element_Is_Dragging] = () => this.RectangleStateMachine.IsInState(RectangleMachineState.Dragging);
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

            var mousePosition = this.GlobalInput.State.Mouse.Position;

            this.UpdateFramePosition(mousePosition);
            this.UpdateFrameStates(mousePosition);
        }

        private void UpdateFramePosition(Point mousePosition)
        {
            if (this.RectangleStateMachine.IsInState(RectangleMachineState.Dragging))
            {
                // Keep rectangle relative position to the mouse position from 
                // changing 
                this.Bounds = new Rectangle(
                    mousePosition.X - this.mouseRelativePosition.X,
                    mousePosition.Y - this.mouseRelativePosition.Y,
                    this.Bounds.Width,
                    this.Bounds.Height);
            }
        }

        private void UpdateFrameStates(Point mousePosition)
        {
            var isOutOfHoldLen =
                mousePosition.DistanceFrom(this.mousePressedPosition).
                     Length() > this.mouseHoldDistance;

            this.RectangleStateMachine.Fire(
                isOutOfHoldLen ? RectangleMachineTrigger.DraggedOutOfRange : RectangleMachineTrigger.DraggedWithinRange);
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