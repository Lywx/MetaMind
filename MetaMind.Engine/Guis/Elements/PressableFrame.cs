namespace MetaMind.Engine.Guis.Elements
{
    using System;
    using System.Diagnostics;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Extensions;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// Only deals with low level clicked event.
    /// </summary>
    public class PressableFrame : FrameBase, IPressableFrame
    {
        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return this.rectangle; }
            set
            {
                var deltaLocation = this.rectangle.Location.DistanceFrom(value.Location);
                var rectanleMoved = deltaLocation.Length() > 0f;
                if (rectanleMoved && this.FrameMoved != null)
                {
                    this.FrameMoved(this, new FrameEventArgs(FrameEventType.Frame_Moved));
                }

                this.rectangle = value;
            }
        }

        #region Constructors

        public PressableFrame(Rectangle rectangle)
            : this()
        {
            this.Initialize(rectangle);
        }

        protected PressableFrame()
        {
            GameEngine.InputEventManager.MouseMove += this.DetectMouseOver;

            GameEngine.InputEventManager.MouseDown += this.DetectMouseLeftPressed;
            GameEngine.InputEventManager.MouseDown += this.DetectMouseRightPressed;

            GameEngine.InputEventManager.MouseUp += this.DetectMouseLeftRelease;
            GameEngine.InputEventManager.MouseUp += this.DetectMouseRightRelease;

            this.FrameMoved += this.DetectMouseOver;

            // dummy intialization
            this.Initialize(new Rectangle());
        }

        ~PressableFrame()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.MouseEnter               = null;
            this.MouseLeave               = null;
            this.MouseLeftPressed         = null;
            this.MouseLeftReleased        = null;
            this.MouseLeftDraggedOutside  = null;
            this.MouseRightPressed        = null;
            this.MouseRightReleased       = null;
            this.MouseRightDraggedOutside = null;

            this.FrameMoved               = null;

            GameEngine.InputEventManager.MouseMove -= this.DetectMouseOver;

            GameEngine.InputEventManager.MouseDown -= this.DetectMouseLeftPressed;
            GameEngine.InputEventManager.MouseDown -= this.DetectMouseRightPressed;

            GameEngine.InputEventManager.MouseUp   -= this.DetectMouseLeftRelease;
            GameEngine.InputEventManager.MouseUp   -= this.DetectMouseRightRelease;
        }

        protected void Initialize(Rectangle rectangle)
        {
            this.rectangle = rectangle;

            this.Enable(FrameState.Frame_Initialized);
            this.Enable(FrameState.Frame_Active);
        }

        protected void Initialize(Point center, Point size)
        {
            this.Initialize(center.PinRectangleCenter(size));
        }

        #endregion Constructors

        #region Public Properties

        public int Height
        {
            get { return this.Rectangle.Height; }
            set { this.Rectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y, this.Rectangle.Width, value); }
        }

        public int Width
        {
            get { return this.Rectangle.Width; }
            set { this.Rectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y, value, this.Rectangle.Height); }
        }

        public int X
        {
            get { return this.Rectangle.X; }
            set { this.Rectangle = new Rectangle(value, this.Rectangle.Y, this.Rectangle.Width, this.Rectangle.Height); }
        }

        public int Y
        {
            get { return this.Rectangle.Y; }
            set { this.Rectangle = new Rectangle(this.Rectangle.X, value, this.Rectangle.Width, this.Rectangle.Height); }
        }

        public Point Center
        {
            get { return this.Rectangle.Center; }
            set { this.Initialize(value, this.Size); }
        }

        public Point Size
        {
            get { return new Point(this.Rectangle.Width, this.Rectangle.Height); }
            set { this.Initialize(this.Center, value); }
        }

        public Point Location
        {
            get { return this.Rectangle.Location; }
            set { this.Initialize(new Rectangle(value.X, value.Y, this.Rectangle.Width, this.Rectangle.Height)); }
        }

        #endregion Public Properties

        #region Events

        public event EventHandler<FrameEventArgs> MouseEnter;

        public event EventHandler<FrameEventArgs> MouseLeave;

        public event EventHandler<FrameEventArgs> MouseLeftPressed;

        public event EventHandler<FrameEventArgs> MouseLeftReleased;

        public event EventHandler<FrameEventArgs> MouseLeftDraggedOutside;

        public event EventHandler<FrameEventArgs> MouseRightPressed;

        public event EventHandler<FrameEventArgs> MouseRightReleased;

        public event EventHandler<FrameEventArgs> MouseRightDraggedOutside;

        public event EventHandler<FrameEventArgs> FrameMoved;

        protected virtual void DetectMouseLeftPressed(object sender, MouseEventArgs e)
        {
            if (this.IsEnabled(FrameState.Mouse_Over) && this.MouseLeftPress(e))
            {
                this.Disable(FrameState.Mouse_Left_Released);
                this.Disable(FrameState.Mouse_Right_Pressed);
                this.Disable(FrameState.Mouse_Right_Released);

                this.Enable(FrameState.Mouse_Left_Pressed);
                if (this.MouseLeftPressed != null)
                {
                    this.MouseLeftPressed(this, new FrameEventArgs(FrameEventType.Mouse_Left_Pressed));
                }
            }
            else if (!this.IsEnabled(FrameState.Mouse_Over) && this.MouseLeftPress(e))
            {
                this.Disable(FrameState.Mouse_Left_Pressed);
            }
        }

        protected virtual void DetectMouseLeftRelease(object sender, MouseEventArgs e)
        {
            if (this.IsEnabled(FrameState.Mouse_Over) && this.IsEnabled(FrameState.Mouse_Left_Pressed))
            {
                this.Disable(FrameState.Mouse_Left_Pressed);

                this.Enable(FrameState.Mouse_Left_Released);
                if (this.MouseLeftReleased != null)
                {
                    this.MouseLeftReleased(this, new FrameEventArgs(FrameEventType.Mouse_Left_Released));
                }
            }
            else
            {
                this.Disable(FrameState.Mouse_Left_Released);
                this.Disable(FrameState.Mouse_Left_Dragged_Out);
            }
        }

        private void DetectMouseOver(object sender, MouseEventArgs e)
        {
            var previouslyInside = this.IsEnabled(FrameState.Mouse_Over);
            var currentlyInside = this.MouseInside(e.Location);

            this.UpdateStates(e.Location);

            if (!previouslyInside && currentlyInside && this.MouseEnter != null)
            {
                this.MouseEnter(this, new FrameEventArgs(FrameEventType.Mouse_Enter));
            }
            else if (previouslyInside && !currentlyInside && this.MouseLeave != null)
            {
                this.MouseLeave(this, new FrameEventArgs(FrameEventType.Mouse_Leave));
            }
        }

        private void DetectMouseOver(object sender, EventArgs e)
        {
            var mouse = GameEngine.InputSequenceManager.Mouse.CurrentState;
            this.DetectMouseOver(null, new MouseEventArgs(MouseButton.None, 0, mouse.X, mouse.Y, 0));
        }

        protected virtual void DetectMouseRightPressed(object sender, MouseEventArgs e)
        {
            if (this.IsEnabled(FrameState.Mouse_Over) && this.MouseRightPress(e))
            {
                this.Disable(FrameState.Mouse_Right_Released);
                this.Disable(FrameState.Mouse_Left_Pressed);
                this.Disable(FrameState.Mouse_Left_Released);

                this.Enable(FrameState.Mouse_Right_Pressed);
                if (this.MouseRightPressed != null)
                {
                    this.MouseRightPressed(this, new FrameEventArgs(FrameEventType.Mouse_Right_Pressed));
                }
            }
            else if (!this.IsEnabled(FrameState.Mouse_Over) && this.MouseRightPress(e))
            {
                this.Disable(FrameState.Mouse_Right_Pressed);
            }
        }

        protected virtual void DetectMouseRightRelease(object sender, MouseEventArgs e)
        {
            if (this.IsEnabled(FrameState.Mouse_Over) && this.IsEnabled(FrameState.Mouse_Right_Pressed))
            {
                this.Disable(FrameState.Mouse_Right_Pressed);

                this.Enable(FrameState.Mouse_Right_Released);
                if (this.MouseRightReleased != null)
                {
                    this.MouseRightReleased(this, new FrameEventArgs(FrameEventType.Mouse_Right_Released));
                }
            }
            else
            {
                this.Disable(FrameState.Mouse_Right_Released);
                this.Disable(FrameState.Mouse_Right_Dragged_Out);
            }
        }

        protected bool MouseInside(Point mouseLocation)
        {
            Debug.Assert(this.IsEnabled(FrameState.Frame_Initialized));
            Debug.Assert(this.rectangle != null);

            return this.IsEnabled(FrameState.Frame_Active) && this.rectangle.Contains(mouseLocation);
        }

        protected bool MouseLeftPress(MouseEventArgs e)
        {
            return e.Button == MouseButton.Left;
        }

        protected bool MouseRightPress(MouseEventArgs e)
        {
            return e.Button == MouseButton.Right;
        }

        #endregion Events

        #region Update

        public virtual void UpdateInput(GameTime gameTime)
        {
            var mouse         = GameEngine.InputSequenceManager.Mouse.CurrentState;
            var mouseLocation = new Point(mouse.X, mouse.Y);

            this.UpdateStates(mouseLocation);
        }

        protected virtual void UpdateStates(Point mouseLocation)
        {
            if (this.MouseInside(mouseLocation))
            {
                this.Enable(FrameState.Mouse_Over);
            }

            if (!this.MouseInside(mouseLocation) && this.IsEnabled(FrameState.Mouse_Left_Pressed))
            {
                // non-applicable for draggable frame
                if (!this.IsEnabled(FrameState.Frame_Dragging) && !this.IsEnabled(FrameState.Frame_Holding))
                {
                    this.Disable(FrameState.Mouse_Left_Pressed);
                    this.Enable(FrameState.Mouse_Left_Dragged_Out);
                    if (this.MouseLeftDraggedOutside != null)
                    {
                        this.MouseLeftDraggedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Left_Dragged_Out));
                    }
                }
            }

            if (!this.MouseInside(mouseLocation) && this.IsEnabled(FrameState.Mouse_Right_Pressed))
            {
                // non-applicable for draggable frame
                if (!this.IsEnabled(FrameState.Frame_Dragging) && !this.IsEnabled(FrameState.Frame_Holding))
                {
                    this.Disable(FrameState.Mouse_Right_Pressed);
                    this.Enable(FrameState.Mouse_Right_Dragged_Out);
                    if (this.MouseRightDraggedOutside != null)
                    {
                        this.MouseRightDraggedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Right_Dragged_Out));
                    }
                }
            }

            if (!this.MouseInside(mouseLocation) && !this.IsEnabled(FrameState.Mouse_Left_Pressed) && !this.IsEnabled(FrameState.Mouse_Right_Pressed))
            {
                this.Disable(FrameState.Mouse_Over);
                this.Disable(FrameState.Mouse_Left_Pressed);
                this.Disable(FrameState.Mouse_Left_Released);
                this.Disable(FrameState.Mouse_Right_Pressed);
                this.Disable(FrameState.Mouse_Right_Released);
            }
        }

        #endregion Update
    }
}