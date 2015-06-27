using System;

namespace MetaMind.Engine.Guis.Elements
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements.Inputs;

    using Microsoft.Xna.Framework;
    using Services;

    public class PickableFrame : FrameEntity, IPickableFrame
    {
        private readonly MouseAutomata mouse = new MouseAutomata();

        private Rectangle rectangle;

        public PickableFrame(Rectangle rectangle)
            : this()
        {
            this.Populate(rectangle);
        }

        public PickableFrame()
        {
            this.InputEvent.MouseMove        += this.EventMouseMove;
            this.InputEvent.MouseUp          += this.EventMouseUp;
            this.InputEvent.MouseDown        += this.EventMouseDown;
            this.InputEvent.MouseDoubleClick += this.EventMouseDoubleClick;

            this.FrameMoved                  += this.FrameFrameMoved;
            this.FrameSized                  += this.FrameFrameMoved;

            this.IsActive = true;

            this[FrameState.Mouse_Is_Over]                   = () => this.mouse.IsMouseOver;

            this[FrameState.Mouse_Is_Left_Pressed]           = () => this.mouse.IsLButtonPressed && this.mouse.IsMouseOver;
            this[FrameState.Mouse_Is_Left_Pressed_Outside]   = () => this.mouse.IsLButtonPressed && !this.mouse.IsMouseOver;
            this[FrameState.Mouse_Is_Left_Released]          = () => this.mouse.IsLButtonReleased;
            this[FrameState.Mouse_Is_Left_Double_Clicked]    = () => this.mouse.IsLButtonDoubleClicked && this.mouse.IsMouseOver;

            this[FrameState.Mouse_Is_Right_Pressed]          = () => this.mouse.IsRButtonPressed && this.mouse.IsMouseOver;
            this[FrameState.Mouse_Is_Right_Pressed_Outside]  = () => this.mouse.IsRButtonPressed && !this.mouse.IsMouseOver;
            this[FrameState.Mouse_Is_Right_Released]         = () => this.mouse.IsRButtonReleased;
            this[FrameState.Mouse_Is_Right_Double_Clicked]   = () => this.mouse.IsRButtonDoubleClicked && this.mouse.IsMouseOver;

            this[FrameState.Frame_Is_Active] = () => this.IsActive;
        }

        ~PickableFrame()
        {
            this.Dispose();
        }

        #region IDisposable

        public override void Dispose()
        {
            // Clean events
            this.MouseEnter               = null;
            this.MouseLeave               = null;
            this.MouseLeftPressed         = null;
            this.MouseLeftPressedOutside  = null;
            this.MouseLeftReleased        = null;
            this.MouseLeftDoubleClicked   = null;
            this.MouseRightPressed        = null;
            this.MouseRightPressedOutside = null;
            this.MouseRightReleased       = null;
            this.MouseRightDoubleClicked  = null;

            this.FrameMoved               = null;
            this.FrameSized               = null;

            // Clean handlers
            this.InputEvent.MouseMove        -= this.EventMouseMove;
            this.InputEvent.MouseDown        -= this.EventMouseDown;
            this.InputEvent.MouseUp          -= this.EventMouseUp;
            this.InputEvent.MouseDoubleClick -= this.EventMouseDoubleClick;

            base.Dispose();
        }

        #endregion IDisposable

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

        private void OnFrameMoved()
        {
            if (this.FrameMoved != null)
            {
                this.FrameMoved(this, new FrameEventArgs(FrameEventType.Frame_Moved));
            }
        }

        private void OnFrameSized()
        {
            if (this.FrameSized != null)
            {
                this.FrameSized(this, new FrameEventArgs(FrameEventType.Frame_Sized));
            }
        }

        private void OnMouseLeftDoubleClicked()
        {
            if (this.MouseLeftDoubleClicked != null)
            {
                this.MouseLeftDoubleClicked(this, new FrameEventArgs(FrameEventType.Mouse_Left_Double_Clicked));
            }
        }

        private void OnMouseLeftPressedOutside()
        {
            if (this.MouseLeftPressedOutside != null)
            {
                this.MouseLeftPressedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Left_Pressed_Outside));
            }
        }

        private void OnMouseLeftPressed()
        {
            if (this.MouseLeftPressed != null)
            {
                this.MouseLeftPressed(this, new FrameEventArgs(FrameEventType.Mouse_Left_Pressed));
            }
        }

        private void OnMouseLeftReleased()
        {
            if (this.MouseLeftReleased != null)
            {
                this.MouseLeftReleased(this, new FrameEventArgs(FrameEventType.Mouse_Left_Released));
            }
        }

        private void OnMouseLeave()
        {
            if (this.MouseLeave != null)
            {
                this.MouseLeave(this, new FrameEventArgs(FrameEventType.Mouse_Leave));
            }
        }

        private void OnMouseEnter()
        {
            if (this.MouseEnter != null)
            {
                this.MouseEnter(this, new FrameEventArgs(FrameEventType.Mouse_Enter));
            }
        }

        private void OnMouseRightDoubleClicked()
        {
            if (this.MouseRightDoubleClicked != null)
            {
                this.MouseRightDoubleClicked(this, new FrameEventArgs(FrameEventType.Mouse_Right_Double_Clicked));
            }
        }

        private void OnMouseRightPressed()
        {
            if (this.MouseRightPressed != null)
            {
                this.MouseRightPressed(this, new FrameEventArgs(FrameEventType.Mouse_Right_Pressed));
            }
        }

        private void OnMouseRightPressedOutside()
        {
            if (this.MouseRightPressedOutside != null)
            {
                this.MouseRightPressedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Right_Pressed_Outside));
            }
        }

        private void OnMouseRightReleased()
        {
            if (this.MouseRightReleased != null)
            {
                this.MouseRightReleased(this, new FrameEventArgs(FrameEventType.Mouse_Right_Released));
            }
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

        private void FrameFrameMoved(object sender, FrameEventArgs e)
        {
            var location = this.InputState.Mouse.CurrentState;
            this.EventMouseMove(null, new MouseEventArgs(MouseButton.None, 0, location.X, location.Y, 0));
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
            return e.Button == MouseButton.Left;
        }

        protected bool IsMouseOver(Point location)
        {
            return this.IsActive && this.rectangle.Contains(location);
        }

        protected bool IsRButton(MouseEventArgs e)
        {
            return e.Button == MouseButton.Right;
        }

        #endregion

        protected void Populate(Point center, Point size)
        {
            this.Populate(center.ToRectangleCenter(size));
        }

        protected void Populate(Rectangle rect)
        {
            this.Rectangle = rect;
        }

        public override void Update(GameTime time)
        {
            this.ClearAction(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.FlushAction(time);
        }
    }
}