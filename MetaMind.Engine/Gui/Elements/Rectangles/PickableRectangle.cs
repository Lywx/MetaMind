﻿namespace MetaMind.Engine.Gui.Elements.Rectangles
{
    using System;
    using System.Linq;
    using Engine.Components.Input;
    using Input;
    using Microsoft.Xna.Framework;
    using Service;

    public class PickableRectangle : RectangleElement, IPickableRectangle
    {
        #region Constructors and Finalizer

        public PickableRectangle(Rectangle bounds)
            : this()
        {
            this.Bounds = bounds;
        }

        public PickableRectangle()
        {
            this.Active = true;

            this.InitializeStates();
            this.RegisterHandlers();
        }

        ~PickableRectangle()
        {
            this.Dispose(true);
        }

        #endregion

        #region Events

        public event EventHandler<ElementEventArgs> MouseEnter = delegate { };

        public event EventHandler<ElementEventArgs> MouseLeave = delegate { };

        /// <summary>
        /// When mouse is pressed inside frame.
        /// </summary>
        public event EventHandler<ElementEventArgs> MousePress = delegate { };

        public event EventHandler<ElementEventArgs> MousePressLeft = delegate { };

        public event EventHandler<ElementEventArgs> MousePressRight = delegate { };

        /// <summary>
        /// When mouse is pressed outside frame.
        /// </summary>
        public event EventHandler<ElementEventArgs> MousePressOut = delegate { };

        public event EventHandler<ElementEventArgs> MousePressOutLeft = delegate { };

        public event EventHandler<ElementEventArgs> MousePressOutRight = delegate { };

        /// <summary>
        /// When mouse is released inside frame.
        /// </summary>
        public event EventHandler<ElementEventArgs> MouseUp = delegate { }; 

        public event EventHandler<ElementEventArgs> MouseUpLeft = delegate { }; 

        public event EventHandler<ElementEventArgs> MouseUpRight = delegate { };

        /// <summary>
        /// When mouse is released outside frame.
        /// </summary>
        public event EventHandler<ElementEventArgs> MouseUpOut = delegate { };

        public event EventHandler<ElementEventArgs> MouseUpOutLeft = delegate { };

        public event EventHandler<ElementEventArgs> MouseUpOutRight = delegate {};

        public event EventHandler<ElementEventArgs> MouseDoubleClick = delegate { };

        public event EventHandler<ElementEventArgs> MouseDoubleClickLeft = delegate { };

        public event EventHandler<ElementEventArgs> MouseDoubleClickRight = delegate { }; 

        #endregion

        #region Event Handlers

        private void EventFrameChanged(object sender, ElementEventArgs e)
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

                    this.DeferAction(this.OnMouseDoubleClickLeft);

                    return;
                }
                else if (this.IsRButton(e))
                {
                    this.mouse.LClear();
                    this.mouse.RDoubleClick();

                    this.DeferAction(this.OnMouseDoubleClickRight);

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
                    this.DeferAction(this.OnMousePressLeft);
                }

                if (!this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMousePressOutLeft);
                }

                return;
            }
            else if (this.IsRButton(e))
            {
                this.mouse.RPress();
                this.mouse.LClear();

                if (this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMousePressRight);
                }

                if (!this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMousePressOutRight);
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
                    this.DeferAction(this.OnMouseUpLeft);
                }
                else
                {
                    this.DeferAction(this.OnMouseUpOutLeft);
                }

                return;
            }
            else if (this.IsRButton(e))
            {
                this.mouse.RRelease();

                if (this.mouse.IsMouseOver)
                {
                    this.DeferAction(this.OnMouseUpRight);
                }
                else
                {
                    this.DeferAction(this.OnMouseUpOutRight);
                }

                return;
            }
        }

        #endregion

        #region Event Registration

        private void RegisterHandlers()
        {
            this.RegisterMouseInputHandlers();
            this.RegisterFrameChangeHandlers();
        }

        private void RegisterFrameChangeHandlers()
        {
            this.Move += this.EventFrameChanged;
            this.Resize += this.EventFrameChanged;
        }

        private void RegisterMouseInputHandlers()
        {
            this.Input.Event.MouseMove += this.EventMouseMove;
            this.Input.Event.MouseUp += this.EventMouseUp;
            this.Input.Event.MouseDown += this.EventMouseDown;
            this.Input.Event.MouseDoubleClick += this.EventMouseDoubleClick;
        }

        #endregion

        #region Event On Methods

        protected virtual void OnMouseDoubleClickLeft()
        {
            this.MouseDoubleClick?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Double_Click_Left));
            this.MouseDoubleClickLeft?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Double_Click_Left));
        }

        protected virtual void OnMouseDoubleClickRight()
        {
            this.MouseDoubleClick?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Double_Click_Right));
            this.MouseDoubleClickRight?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Double_Click_Right));
        }

        protected virtual  void OnMousePressOutLeft()
        {
            this.MousePressOut?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Press_Out_Left));
            this.MousePressOutLeft?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Press_Out_Left));
        }

        protected virtual void OnMousePressOutRight()
        {
            this.MousePressOut?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Pressed_Out_Right));
            this.MousePressOutRight?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Pressed_Out_Right));
        }

        protected virtual void OnMousePressLeft()
        {
            this.MousePress?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Press_Left));
            this.MousePressLeft?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Press_Left));
        }

        protected virtual void OnMousePressRight()
        {
            this.MousePress?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Press_Right));
            this.MousePressRight?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Press_Right));
        }

        protected virtual void OnMouseUpLeft()
        {
            this.MouseUp?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Up_Left));
            this.MouseUpLeft?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Up_Left));
        }

        protected virtual void OnMouseUpRight()
        {
            this.MouseUp?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Up_Right));
            this.MouseUpRight?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Up_Right));
        }

        private void OnMouseUpOutRight()
        {
            this.MouseUpOut?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Up_Out_Left));
            this.MouseUpOutRight?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Up_Out_Left));
        }

        private void OnMouseUpOutLeft()
        {
            this.MouseUpOut?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Up_Out_Right));
            this.MouseUpOutLeft?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Up_Out_Right));
        }

        protected virtual void OnMouseLeave()
        {
            this.MouseLeave?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Leave));
        }

        protected virtual void OnMouseEnter()
        {
            this.MouseEnter?.Invoke(this, new ElementEventArgs(ElementEvent.Mouse_Enter));
        }

        #endregion

        #region Element State Data

        private bool active;

        /// <summary>
        /// Gets or sets a value indicating whether this should receive or send any events.
        /// </summary>
        public sealed override bool Active
        {
            get { return this.active; }
            set
            {
                if (this.active != value)
                {
                    // This is used to deduce event performance overhead on 
                    // an individual frame.
                    if (value)
                    {
                        this.RegisterHandlers();
                    }
                    else
                    {
                        this.DisposeHandlers();
                    }
                }

                this.active = value;
            }
        }

        /// TODO(Minor): Enable read-only setter by implementing a permission array.
        /// TODO(Minor): Migrate some advanced states to fields and properties.
        /// <summary>
        /// Frame states as Func<bool> to replace messy things like active, 
        /// visible. In order to enable logic passing, I decided to make them 
        /// Func<bool>.
        /// </summary>
        private readonly Func<bool>[] frameStates = new Func<bool>[(int)ElementState.StateNum];

        public Func<bool> this[ElementState state]
        {
            get
            {
                return this.frameStates[(int)state];
            }

            protected set
            {
                this.frameStates[(int)state] = value;
            }
        }

        internal bool[] FrameStates => this.frameStates.Select(state => state()).ToArray();

        #endregion

        #region Element Geometry Data

        public override sealed Rectangle Bounds
        {
            get { return base.Bounds; }
            set { base.Bounds = value; }
        }

        #endregion

        #region Element State Initialization

        private void InitializeStates()
        {
            this.InitializeDefaultStates();
            this.InitializePickableStates();
        }

        private void InitializeDefaultStates()
        {
            for (var i = 0; i < (int)ElementState.StateNum; i++)
            {
                this.frameStates[i] = () => false;
            }
        }

        private void InitializePickableStates()
        {
            this[ElementState.Element_Is_Active] = () => this.Active;

            this[ElementState.Mouse_Is_Over] = () => this.mouse.IsMouseOver;

            this[ElementState.Mouse_Is_Left_Pressed]        = () => this.mouse.IsLButtonPressed && this.mouse.IsMouseOver;
            this[ElementState.Mouse_Is_Left_Pressed_Out]    = () => this.mouse.IsLButtonPressed && !this.mouse.IsMouseOver;
            this[ElementState.Mouse_Is_Left_Released]       = () => this.mouse.IsLButtonReleased;
            this[ElementState.Mouse_Is_Left_Double_Clicked] = () => this.mouse.IsLButtonDoubleClicked && this.mouse.IsMouseOver;

            this[ElementState.Mouse_Is_Right_Pressed]        = () => this.mouse.IsRButtonPressed && this.mouse.IsMouseOver;
            this[ElementState.Mouse_Is_Right_Pressed_Out]    = () => this.mouse.IsRButtonPressed && !this.mouse.IsMouseOver;
            this[ElementState.Mouse_Is_Right_Released]       = () => this.mouse.IsRButtonReleased;
            this[ElementState.Mouse_Is_Right_Double_Clicked] = () => this.mouse.IsRButtonDoubleClicked && this.mouse.IsMouseOver;
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
            return this.Active && this.Bounds.Contains(location);
        }

        protected bool IsRButton(MouseEventArgs e)
        {
            return e.Button == MouseButtons.Right;
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            if (!this.Enabled)
            {
                return;
            }

            this.ClearAction(time);
        }

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            if (!this.Enabled)
            {
                return;
            }

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

        private void DisposeHandlers()
        {
            this.DisposeMouseInputHandlers();
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