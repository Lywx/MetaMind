﻿using System;

namespace MetaMind.Engine.Guis.Elements
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements.Inputs;

    using Microsoft.Xna.Framework;

    public class PickableFrame : FrameEntity, IPickableFrame
    {
        private readonly MouseAutomata mouse = new MouseAutomata();

        private Rectangle rectangle = new Rectangle();

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

            // Clean handlers
            this.InputEvent.MouseMove        -= this.EventMouseMove;
            this.InputEvent.MouseDown        -= this.EventMouseDown;
            this.InputEvent.MouseUp          -= this.EventMouseUp;
            this.InputEvent.MouseDoubleClick -= this.EventMouseDoubleClick;

            base.Dispose();
        }

        #endregion IDisposable

        public event EventHandler<FrameEventArgs> FrameMoved;

        public event EventHandler<FrameEventArgs> MouseEnter;

        public event EventHandler<FrameEventArgs> MouseLeave;

        public event EventHandler<FrameEventArgs> MouseLeftPressed;

        public event EventHandler<FrameEventArgs> MouseLeftPressedOutside;

        public event EventHandler<FrameEventArgs> MouseLeftDoubleClicked;

        public event EventHandler<FrameEventArgs> MouseLeftReleased;

        public event EventHandler<FrameEventArgs> MouseRightPressed;

        public event EventHandler<FrameEventArgs> MouseRightPressedOutside; 
        
        public event EventHandler<FrameEventArgs> MouseRightReleased;
        
        public event EventHandler<FrameEventArgs> MouseRightDoubleClicked;

        public Point Center
        {
            get { return this.Rectangle.Center; }
            set { this.Populate(value, this.Size); }
        }

        public int Height
        {
            get { return this.Rectangle.Height; }
            set { this.Rectangle = new Rectangle(this.Rectangle.X, this.Rectangle.Y, this.Rectangle.Width, value); }
        }

        public bool IsActive { get; set; }

        public Point Location
        {
            get { return this.Rectangle.Location; }
            set { this.Populate(new Rectangle(value.X, value.Y, this.Rectangle.Width, this.Rectangle.Height)); }
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
                var hasMoved = deltaLocation.Length() > 0f;
                if (hasMoved && this.FrameMoved != null)
                {
                    this.FrameMoved(this, new FrameEventArgs(FrameEventType.Frame_Moved));
                }

                this.rectangle = value;
            }
        }

        public Point Size
        {
            get { return new Point(this.Rectangle.Width, this.Rectangle.Height); }
            set { this.Populate(this.Center, value); }
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

        protected void Populate(Point center, Point size)
        {
            this.Populate(center.ToRectangleCenter(size));
        }

        protected void Populate(Rectangle rect)
        {
            this.rectangle = rect;
        }

        private void EventMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.mouse.IsMouseOver)
            {
                if (this.IsLButton(e))
                {
                    mouse.LDoubleClick();
                    mouse.RClear();

                    if (this.MouseLeftDoubleClicked != null)
                    {
                        this.MouseLeftDoubleClicked(this, new FrameEventArgs(FrameEventType.Mouse_Left_Double_Clicked));
                    }

                    return;
                }
                else if (this.IsRButton(e))
                {
                    mouse.LClear();
                    mouse.RDoubleClick();

                    if (this.MouseRightDoubleClicked != null)
                    {
                        this.MouseRightDoubleClicked(this, new FrameEventArgs(FrameEventType.Mouse_Right_Double_Clicked));
                    }

                    return;
                }
            }
        }

        private void EventMouseDown(object sender, MouseEventArgs e)
        {
            if (this.IsLButton(e))
            {
                mouse.LPress();
                mouse.RClear();

                if (this.mouse.IsMouseOver && this.MouseLeftPressed != null)
                {
                    this.MouseLeftPressed(this, new FrameEventArgs(FrameEventType.Mouse_Left_Pressed));
                }

                if (!this.mouse.IsMouseOver && this.MouseLeftPressedOutside != null)
                {
                    this.MouseLeftPressedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Left_Pressed_Outside));
                }

                return;
            }
            else if (this.IsRButton(e))
            {
                mouse.RPress();
                mouse.LClear();

                if (this.mouse.IsMouseOver && this.MouseRightPressed != null)
                {
                    this.MouseRightPressed(this, new FrameEventArgs(FrameEventType.Mouse_Right_Pressed));
                }

                if (!this.mouse.IsMouseOver && this.MouseRightPressedOutside != null)
                {
                    this.MouseRightPressedOutside(this, new FrameEventArgs(FrameEventType.Mouse_Right_Pressed_Outside));
                }

                return;
            }
        }

        private void EventMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouse.IsMouseOver && this.IsMouseOver(e.Location))
            {
                mouse.Enter();

                if (this.MouseEnter != null)
                {
                    this.MouseEnter(this, new FrameEventArgs(FrameEventType.Mouse_Enter));
                }

                return;
            }

            if (this.mouse.IsMouseOver && !this.IsMouseOver(e.Location))
            {
                mouse.Leave();

                if (this.MouseLeave != null)
                {
                    this.MouseLeave(this, new FrameEventArgs(FrameEventType.Mouse_Leave));
                }

                return;
            }
        }

        private void EventMouseUp(object sender, MouseEventArgs e)
        {
            if (this.IsLButton(e))
            {
                mouse.LRelease();

                if (this.MouseLeftReleased != null)
                {
                    this.MouseLeftReleased(this, new FrameEventArgs(FrameEventType.Mouse_Left_Released));
                }

                return;
            }
            else if (this.IsRButton(e))
            {
                mouse.RRelease();

                if (this.MouseRightReleased != null)
                {
                    this.MouseRightReleased(this, new FrameEventArgs(FrameEventType.Mouse_Right_Released));
                }

                return;
            }
        }

        private void FrameFrameMoved(object sender, FrameEventArgs e)
        {
            var location = this.InputState.Mouse.CurrentState;
            this.EventMouseMove(null, new MouseEventArgs(MouseButton.None, 0, location.X, location.Y, 0));
        }
    }
}