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

        protected StateMachine<RectangleMachineState, RectangleMachineTrigger> RectangleMachine { get; private set; }

        #endregion

        #region Events

        public event EventHandler<MMElementEventArgs> MouseDrag;

        public event EventHandler<MMElementEventArgs> MouseDrop;

        #endregion

        #region Event Handlers

        private void FrameMousePress(object sender, MMElementEventArgs e)
        {
            var mousePosition = this.Input.State.Mouse.Position;

            // origin for deciding whether is dragging
            this.mousePressedPosition = new Point(mousePosition.X, mousePosition.Y);

            // save relative position of mouse compared to rectangle
            // mouse y-axis value is fixed at y-axis center of the rectangle
            this.mouseRelativePosition = new Point(mousePosition.X - this.Bounds.X, mousePosition.Y - this.Bounds.Y);

            this.RectangleMachine.Fire(RectangleMachineTrigger.Pressed);
        }

        private void FrameMouseUp(object sender, EventArgs e)
        {
            this.RectangleMachine.Fire(RectangleMachineTrigger.Released);
        }

        #endregion

        #region Event On

        private void OnMouseDropped()
        {
            this.MouseDrop?.Invoke(this, new MMElementEventArgs(MMElementEvent.Element_Drop));
        }

        private void OnMouseDragged()
        {
            this.MouseDrag?.Invoke(this, new MMElementEventArgs(MMElementEvent.Element_Drag));
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
            this.InitializeMachine();
            this.InitializeStates();
        }

        private void InitializeMachine()
        {
            this.RectangleMachine = new StateMachine<RectangleMachineState, RectangleMachineTrigger>(RectangleMachineState.Released);

            // Possible cross interference 
            this.RectangleMachine.Configure(RectangleMachineState.Released).PermitReentry(RectangleMachineTrigger.Released);
            this.RectangleMachine.Configure(RectangleMachineState.Released).Permit(RectangleMachineTrigger.Pressed, RectangleMachineState.Pressing);
            this.RectangleMachine.Configure(RectangleMachineState.Released).Ignore(RectangleMachineTrigger.DraggedWithinRange);
            this.RectangleMachine.Configure(RectangleMachineState.Released).Ignore(RectangleMachineTrigger.DraggedOutOfRange);

            // Possible cross interference
            this.RectangleMachine.Configure(RectangleMachineState.Pressing).PermitReentry(RectangleMachineTrigger.Pressed);
            this.RectangleMachine.Configure(RectangleMachineState.Pressing).Permit(RectangleMachineTrigger.Released, RectangleMachineState.Released);
            this.RectangleMachine.Configure(RectangleMachineState.Pressing).Permit(RectangleMachineTrigger.DraggedWithinRange, RectangleMachineState.Holding);
            this.RectangleMachine.Configure(RectangleMachineState.Pressing).Permit(RectangleMachineTrigger.DraggedOutOfRange, RectangleMachineState.Dragging);

            this.RectangleMachine.Configure(RectangleMachineState.Holding).PermitReentry(RectangleMachineTrigger.DraggedWithinRange);
            this.RectangleMachine.Configure(RectangleMachineState.Holding).Permit(RectangleMachineTrigger.DraggedOutOfRange, RectangleMachineState.Dragging);

            // Possible cross interference
            this.RectangleMachine.Configure(RectangleMachineState.Holding).Permit(RectangleMachineTrigger.Pressed, RectangleMachineState.Pressing);
            this.RectangleMachine.Configure(RectangleMachineState.Holding).Permit(RectangleMachineTrigger.Released, RectangleMachineState.Released);

            // Possible cross interference
            this.RectangleMachine.Configure(RectangleMachineState.Dragging).Permit(RectangleMachineTrigger.Pressed, RectangleMachineState.Released);
            this.RectangleMachine.Configure(RectangleMachineState.Dragging).Permit(RectangleMachineTrigger.Released, RectangleMachineState.Released);
            this.RectangleMachine.Configure(RectangleMachineState.Dragging).Ignore(RectangleMachineTrigger.DraggedOutOfRange);
            this.RectangleMachine.Configure(RectangleMachineState.Dragging).Ignore(RectangleMachineTrigger.DraggedWithinRange);

            this.RectangleMachine.Configure(RectangleMachineState.Dragging).OnEntry(() =>
                {
                    this.CacheAction(this.OnMouseDragged);
                });

            this.RectangleMachine.Configure(RectangleMachineState.Dragging).OnExit(() =>
                {
                    this.CacheAction(this.OnMouseDropped);
                });
        }

        private void InitializeStates()
        {
            this[MMElementState.Element_Is_Holding] = () => this.RectangleMachine.IsInState(RectangleMachineState.Holding);
            this[MMElementState.Element_Is_Dragging] = () => this.RectangleMachine.IsInState(RectangleMachineState.Dragging);
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

            var mousePosition = this.Input.State.Mouse.Position;

            this.UpdateFramePosition(mousePosition);
            this.UpdateFrameStates(mousePosition);
        }

        private void UpdateFramePosition(Point mousePosition)
        {
            if (this.RectangleMachine.IsInState(RectangleMachineState.Dragging))
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

            this.RectangleMachine.Fire(
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