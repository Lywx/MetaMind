using System;

namespace MetaMind.Engine.Guis.Elements
{
    using Components.Inputs;
    using Inputs;

    using Microsoft.Xna.Framework;
    using Services;

    public class PickableFrame : FrameEntity, IPickableFrame
    {
        private readonly MouseAutomata mouse = new MouseAutomata();

        private Rectangle rectangle;

        #region Constructors and Finalizer

        public PickableFrame(Rectangle rectangle)
            : this()
        {
            this.Populate(rectangle);
        }

        public PickableFrame()
        {
            this.RegisterMouseInputHandlers();
            this.RegisterFrameChangeHandlers();

            this.IsActive = true;

            this[FrameState.Mouse_Is_Over] = () => this.mouse.IsMouseOver;

            this[FrameState.Mouse_Is_Left_Pressed]          = () => this.mouse.IsLButtonPressed && this.mouse.IsMouseOver;
            this[FrameState.Mouse_Is_Left_Pressed_Outside]  = () => this.mouse.IsLButtonPressed && !this.mouse.IsMouseOver;
            this[FrameState.Mouse_Is_Left_Released]         = () => this.mouse.IsLButtonReleased;
            this[FrameState.Mouse_Is_Left_Double_Clicked]   = () => this.mouse.IsLButtonDoubleClicked && this.mouse.IsMouseOver;

            this[FrameState.Mouse_Is_Right_Pressed]         = () => this.mouse.IsRButtonPressed && this.mouse.IsMouseOver;
            this[FrameState.Mouse_Is_Right_Pressed_Outside] = () => this.mouse.IsRButtonPressed && !this.mouse.IsMouseOver;
            this[FrameState.Mouse_Is_Right_Released]        = () => this.mouse.IsRButtonReleased;
            this[FrameState.Mouse_Is_Right_Double_Clicked]  = () => this.mouse.IsRButtonDoubleClicked && this.mouse.IsMouseOver;

            this[FrameState.Frame_Is_Active] = () => this.IsActive;
        }

        ~PickableFrame()
        {
            this.Dispose(true);
        }

        #endregion

        #region Events

        public event EventHandler<FrameEventArgs> FrameMoved = delegate {};

        public event EventHandler<FrameEventArgs> FrameSized = delegate {};

        public event EventHandler<FrameEventArgs> MouseEnter = delegate {};

        public event EventHandler<FrameEventArgs> MouseLeave = delegate {};

        public event EventHandler<FrameEventArgs> MouseLeftPressed = delegate {};

        public event EventHandler<FrameEventArgs> MouseLeftPressedOutside = delegate {};

        public event EventHandler<FrameEventArgs> MouseLeftDoubleClicked = delegate {};

        public event EventHandler<FrameEventArgs> MouseLeftReleased = delegate {};

        public event EventHandler<FrameEventArgs> MouseRightPressed = delegate {};

        public event EventHandler<FrameEventArgs> MouseRightPressedOutside = delegate {};

        public event EventHandler<FrameEventArgs> MouseRightReleased = delegate {};

        public event EventHandler<FrameEventArgs> MouseRightDoubleClicked = delegate {};

        private void FrameFrameMoved(object sender, FrameEventArgs e)
        {
            var mosue = this.Input.State.Mouse.CurrentState;
            this.EventMouseMove(null, new MouseEventArgs(MouseButtons.None, 0, mosue.X, mosue.Y, 0));
        }

        private void EventMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.mouse.IsMouseOver)
            {
                if (this.IsLButton(e))
                {
                    this.mouse.LDoubleClick();
                    this.mouse.RClear();

                    this.DeferAction(this.OnMouseLeftDoubleClicked);

                    return;
                }
                else if (this.IsRButton(e))
                {
                    this.mouse.LClear();
                    this.mouse.RDoubleClick();

                    this.DeferAction(this.OnMouseRightDoubleClicked);

                    return;
                }
            }
        }

        private void EventMouseDown(object sender, MouseEventArgs e)
        {
            if (this.IsLButton(e))
            {
                this.mouse.LPress();
                this.mouse.RClear();

                if (this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMouseLeftPressed);
                }

                if (!this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMouseLeftPressedOutside);
                }

                return;
            }
            else if (this.IsRButton(e))
            {
                this.mouse.RPress();
                this.mouse.LClear();

                if (this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMouseRightPressed);
                }

                if (!this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMouseRightPressedOutside);
                }

                return;
            }
        }

        private void EventMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouse.IsMouseOver && this.IsMouseOver(e.Location))
            {
                this.mouse.Enter();

                this.DeferAction(this.OnMouseEnter);

                return;
            }

            if (this.mouse.IsMouseOver && !this.IsMouseOver(e.Location))
            {
                this.mouse.Leave();

                this.DeferAction(this.OnMouseLeave);

                return;
            }
        }

        private void EventMouseUp(object sender, MouseEventArgs e)
        {
            if (this.IsLButton(e))
            {
                this.mouse.LRelease();

                if (this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMouseLeftReleased);
                }

                return;
            }
            else if (this.IsRButton(e))
            {
                this.mouse.RRelease();

                if (this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMouseRightReleased);
                }

                return;
            }
        }

        private void OnFrameMoved()
        {
            this.FrameMoved?.Invoke(this, new FrameEventArgs(FrameEventType.Frame_Moved));
        }

        private void OnFrameSized()
        {
            this.FrameSized?.Invoke(this, new FrameEventArgs(FrameEventType.Frame_Sized));
        }

        private void OnMouseLeftDoubleClicked()
        {
            this.MouseLeftDoubleClicked?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Left_Double_Clicked));
        }

        private void OnMouseLeftPressedOutside()
        {
            this.MouseLeftPressedOutside?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Left_Pressed_Outside));
        }

        private void OnMouseLeftPressed()
        {
            this.MouseLeftPressed?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Left_Pressed));
        }

        private void OnMouseLeftReleased()
        {
            this.MouseLeftReleased?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Left_Released));
        }

        private void OnMouseLeave()
        {
            this.MouseLeave?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Leave));
        }

        private void OnMouseEnter()
        {
            this.MouseEnter?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Enter));
        }

        private void OnMouseRightDoubleClicked()
        {
            this.MouseRightDoubleClicked?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Right_Double_Clicked));
        }

        private void OnMouseRightPressed()
        {
            this.MouseRightPressed?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Right_Pressed));
        }

        private void OnMouseRightPressedOutside()
        {
            this.MouseRightPressedOutside?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Right_Pressed_Outside));
        }

        private void OnMouseRightReleased()
        {
            this.MouseRightReleased?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Right_Released));
        }

        private void RegisterFrameChangeHandlers()
        {
            this.FrameMoved += this.FrameFrameMoved;
            this.FrameSized += this.FrameFrameMoved;
        }

        private void RegisterMouseInputHandlers()
        {
            this.Input.Event.MouseMove        += this.EventMouseMove;
            this.Input.Event.MouseUp          += this.EventMouseUp;
            this.Input.Event.MouseDown        += this.EventMouseDown;
            this.Input.Event.MouseDoubleClick += this.EventMouseDoubleClick;
        }

        #endregion

        #region Frame Data

        public Point Center
        {
            get
            {
                return this.Rectangle.Center;
            }
            set
            {
                this.Populate(value, this.Size);
            }
        }

        public int Height
        {
            get
            {
                return this.Rectangle.Height;
            }
            set
            {
                this.Rectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y, this.Rectangle.Width, value);
            }
        }

        public bool IsActive { get; set; }

        public Point Location
        {
            get
            {
                return this.Rectangle.Location;
            }
            set
            {
                this.Populate(new Rectangle(value.X, value.Y, this.Rectangle.Width, this.Rectangle.Height));
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }

            set
            {
                var deltaLocation = this.rectangle.Location.DistanceFrom(value.Location);
                var deltaSize = this.rectangle.Size.DistanceFrom(value.Size);

                this.rectangle = value;

                var hasMoved = deltaLocation.Length() > 0f;
                var hasSized = deltaSize.Length() > 0;
                if (hasMoved)
                {
                    this.DeferAction(this.OnFrameMoved);
                }

                if (hasSized)
                {
                    this.DeferAction(this.OnFrameSized);
                }
            }
        }

        public Point Size
        {
            get
            {
                return new Point(this.Rectangle.Width, this.Rectangle.Height);
            }
            set
            {
                this.Populate(this.Center, value);
            }
        }

        public int Width
        {
            get
            {
                return this.Rectangle.Width;
            }
            set
            {
                this.Rectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y, value, this.Rectangle.Height);
            }
        }

        public int X
        {
            get
            {
                return this.Rectangle.X;
            }
            set
            {
                this.Rectangle = new Rectangle(value, this.Rectangle.Y, this.Rectangle.Width, this.Rectangle.Height);
            }
        }

        public int Y
        {
            get
            {
                return this.Rectangle.Y;
            }
            set
            {
                this.Rectangle = new Rectangle(this.Rectangle.X, value, this.Rectangle.Width, this.Rectangle.Height);
            }
        }

        #endregion

        #region State

        protected bool IsLButton(MouseEventArgs e)
        {
            return e.Button == MouseButtons.Left;
        }

        protected bool IsMouseOver(Point location)
        {
            return this.IsActive && this.rectangle.Contains(location);
        }

        protected bool IsRButton(MouseEventArgs e)
        {
            return e.Button == MouseButtons.Right;
        }

        #endregion

        #region Initialization

        protected void Populate(Point center, Point size)
        {
            this.Populate(center.ToRectangleCenter(size));
        }

        protected void Populate(Rectangle rect)
        {
            this.Rectangle = rect;
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.ClearAction(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.FlushAction(time);
        }

        #endregion

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

                        // No need to dispose frame change handlers because the 
                        // events are disposed in this.DisposeEvents
                        this.DisposeMouseInputHandlers();
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

        private void DisposeMouseInputHandlers()
        {
            this.Input.Event.MouseMove -= this.EventMouseMove;
            this.Input.Event.MouseUp -= this.EventMouseUp;
            this.Input.Event.MouseDown -= this.EventMouseDown;
            this.Input.Event.MouseDoubleClick -= this.EventMouseDoubleClick;
        }

        private void DisposeEvents()
        {
            this.DisposeMouseInputEvents();
            this.DisposeFrameChangeEvents();
        }

        private void DisposeFrameChangeEvents()
        {
            this.FrameMoved = null;
            this.FrameSized = null;
        }

        private void DisposeMouseInputEvents()
        {
            this.MouseEnter = null;
            this.MouseLeave = null;
            this.MouseLeftPressed = null;
            this.MouseLeftPressedOutside = null;
            this.MouseLeftReleased = null;
            this.MouseLeftDoubleClicked = null;
            this.MouseRightPressed = null;
            this.MouseRightPressedOutside = null;
            this.MouseRightReleased = null;
            this.MouseRightDoubleClicked = null;
        }

        #endregion
    }
}