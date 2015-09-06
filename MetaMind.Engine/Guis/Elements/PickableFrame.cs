using System;

namespace MetaMind.Engine.Guis.Elements
{
    using Components.Inputs;
    using Inputs;

    using Microsoft.Xna.Framework;
    using Services;

    public class PickableFrame : FrameEntity, IPickableFrame
    {
        #region Constructors and Finalizer

        public PickableFrame(Rectangle rectangle)
            : this()
        {
            this.Rectangle = rectangle;
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

        public event EventHandler<FrameEventArgs> Move = delegate
        {
        };

        public event EventHandler<FrameEventArgs> Resize = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MouseEnter = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MouseLeave = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MousePress = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MousePressLeft = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MousePressRight = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MousePressOut = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MousePressOutLeft =
            delegate
            {
            };

        public event EventHandler<FrameEventArgs> MousePressOutRight =
            delegate
            {
            };

        public event EventHandler<FrameEventArgs> MouseUp = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MouseUpLeft = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MouseUpRight = delegate
        {
        };

        public event EventHandler<FrameEventArgs> MouseDoubleClick;

        public event EventHandler<FrameEventArgs> MouseDoubleClickLeft =
            delegate
            {
            };

        public event EventHandler<FrameEventArgs> MouseDoubleClickRight =
            delegate
            {
            };

        #endregion

        #region Event Handlers

        private void EventFrameChanged(object sender, FrameEventArgs e)
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

        #endregion

        #region Event Registration

        private void RegisterFrameChangeHandlers()
        {
            this.Move += this.EventFrameChanged;
            this.Resize += this.EventFrameChanged;
        }

        private void RegisterMouseInputHandlers()
        {
            this.Input.Event.MouseMove        += this.EventMouseMove;
            this.Input.Event.MouseUp          += this.EventMouseUp;
            this.Input.Event.MouseDown        += this.EventMouseDown;
            this.Input.Event.MouseDoubleClick += this.EventMouseDoubleClick;
        }

        #endregion

        #region Event On Methods

        private void OnFrameMove()
        {
            this.Move?.Invoke(this, new FrameEventArgs(FrameEventType.Frame_Move));
        }

        private void OnFrameResize()
        {
            this.Resize?.Invoke(this, new FrameEventArgs(FrameEventType.Frame_Size));
        }

        private void OnMouseLeftDoubleClicked()
        {
            this.MouseDoubleClick?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Double_Click_Left));
            this.MouseDoubleClickLeft?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Double_Click_Left));
        }

        private void OnMouseLeftPressedOutside()
        {
            this.MousePressOut?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Press_Out_Left));
            this.MousePressOutLeft?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Press_Out_Left));
        }

        private void OnMouseLeftPressed()
        {
            this.MousePress?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Press_Left));
            this.MousePressLeft?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Press_Left));
        }

        private void OnMouseLeftReleased()
        {
            this.MouseUp?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Up_Left));
            this.MouseUpLeft?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Up_Left));
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
            this.MouseDoubleClick?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Double_Click_Right));
            this.MouseDoubleClickRight?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Double_Click_Right));
        }

        private void OnMouseRightPressed()
        {
            this.MousePress?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Press_Right));
            this.MousePressRight?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Press_Right));
        }

        private void OnMouseRightPressedOutside()
        {
            this.MousePressOut?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Pressed_Out_Right));
            this.MousePressOutRight?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Pressed_Out_Right));
        }

        private void OnMouseRightReleased()
        {
            this.MouseUp?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Up_Right));
            this.MouseUpRight?.Invoke(this, new FrameEventArgs(FrameEventType.Mouse_Up_Right));
        }

        #endregion


        #region Frame State

        public bool IsActive { get; set; }

        #endregion

        #region Frame Geometry

        private Rectangle rectangle;

        public Point Center
        {
            get
            {
                return this.Rectangle.Center;
            }
            set
            {
                this.Rectangle = new Rectangle(value, this.Size);
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

        public Point Location
        {
            get
            {
                return this.Rectangle.Location;
            }
            set
            {
                this.Rectangle = new Rectangle(value.X, value.Y, this.Rectangle.Width, this.Rectangle.Height);
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
                var hasResized = deltaSize.Length() > 0;

                if (hasMoved)
                {
                    this.DeferAction(this.OnFrameMove);
                }

                if (hasResized)
                {
                    this.DeferAction(this.OnFrameResize);
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
                this.Rectangle = new Rectangle(this.Center, value);
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

        #region Mouse State Detection

        private readonly MouseAutomata mouse = new MouseAutomata();

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
            this.Move = null;
            this.Resize = null;
        }

        private void DisposeMouseInputEvents()
        {
            this.MouseEnter = null;
            this.MouseLeave = null;
            this.MousePressLeft = null;
            this.MousePressOutLeft = null;
            this.MouseUpLeft = null;
            this.MouseDoubleClickLeft = null;
            this.MousePressRight = null;
            this.MousePressOutRight = null;
            this.MouseUpRight = null;
            this.MouseDoubleClickRight = null;
        }

        #endregion
    }
}