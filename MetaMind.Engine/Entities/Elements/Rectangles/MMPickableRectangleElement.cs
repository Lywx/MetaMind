namespace MetaMind.Engine.Entities.Elements.Rectangles
{
    using System;
    using System.Linq;
    using Components.Input;
    using Entities;
    using Input;
    using Microsoft.Xna.Framework;
    using Shapes;

    public class MMPickableRectangleElement : MMRectangle, IMMPickableRectangleElement
    {
        #region Constructors and Finalizer

        public MMPickableRectangleElement(Rectangle bounds)
            : this()
        {
            this.Bounds = bounds;
        }

        public MMPickableRectangleElement()
        {
            this.InitializeStates();
            this.RegisterHandlers();
        }

        ~MMPickableRectangleElement()
        {
            this.Dispose(true);
        }

        #endregion

        #region Events

        public event EventHandler<MMElementEventArgs> MouseEnter = delegate { };

        public event EventHandler<MMElementEventArgs> MouseLeave = delegate { };

        /// <summary>
        /// When mouse is pressed inside frame.
        /// </summary>
        public event EventHandler<MMElementEventArgs> MousePress = delegate { };

        public event EventHandler<MMElementEventArgs> MousePressLeft = delegate { };

        public event EventHandler<MMElementEventArgs> MousePressRight = delegate { };

        /// <summary>
        /// When mouse is pressed outside frame.
        /// </summary>
        public event EventHandler<MMElementEventArgs> MousePressOut = delegate { };

        public event EventHandler<MMElementEventArgs> MousePressOutLeft = delegate { };

        public event EventHandler<MMElementEventArgs> MousePressOutRight = delegate { };

        /// <summary>
        /// When mouse is released inside frame.
        /// </summary>
        public event EventHandler<MMElementEventArgs> MouseUp = delegate { }; 

        public event EventHandler<MMElementEventArgs> MouseUpLeft = delegate { }; 

        public event EventHandler<MMElementEventArgs> MouseUpRight = delegate { };

        /// <summary>
        /// When mouse is released outside frame.
        /// </summary>
        public event EventHandler<MMElementEventArgs> MouseUpOut = delegate { };

        public event EventHandler<MMElementEventArgs> MouseUpOutLeft = delegate { };

        public event EventHandler<MMElementEventArgs> MouseUpOutRight = delegate {};

        public event EventHandler<MMElementEventArgs> MouseDoubleClick = delegate { };

        public event EventHandler<MMElementEventArgs> MouseDoubleClickLeft = delegate { };

        public event EventHandler<MMElementEventArgs> MouseDoubleClickRight = delegate { }; 

        #endregion

        #region Event Handlers

        private void EventFrameChanged(object sender, MMElementEventArgs e)
        {
            var mousePosition = this.Input.State.Mouse.Position;
            this.EventMouseMove(
                null,
                new MMMouseEventArgs(
                    MMMouseButtons.None,
                    0,
                    mousePosition.X,
                    mousePosition.Y,
                    0));
        }

        private void EventMouseDoubleClick(object sender, MMMouseEventArgs e)
        {
            if (this.mouseState.IsMouseOver)
            {
                if (this.IsLButton(e))
                {
                    this.mouseState.LDoubleClick();
                    this.mouseState.RClear();

                    this.CacheAction(this.OnMouseDoubleClickLeft);

                    return;
                }
                else if (this.IsRButton(e))
                {
                    this.mouseState.LClear();
                    this.mouseState.RDoubleClick();

                    this.CacheAction(this.OnMouseDoubleClickRight);

                    return;
                }
            }
        }

        private void EventMouseDown(object sender, MMMouseEventArgs e)
        {
            if (this.IsLButton(e))
            {
                this.mouseState.LPress();
                this.mouseState.RClear();

                if (this.mouseState.IsMouseOver)
                {
                    this.CacheAction(this.OnMousePressLeft);
                }

                if (!this.mouseState.IsMouseOver)
                {
                    this.CacheAction(this.OnMousePressOutLeft);
                }

                return;
            }
            else if (this.IsRButton(e))
            {
                this.mouseState.RPress();
                this.mouseState.LClear();

                if (this.mouseState.IsMouseOver)
                {
                    this.CacheAction(this.OnMousePressRight);
                }

                if (!this.mouseState.IsMouseOver)
                {
                    this.CacheAction(this.OnMousePressOutRight);
                }

                return;
            }
        }

        private void EventMouseMove(object sender, MMMouseEventArgs e)
        {
            if (!this.mouseState.IsMouseOver && this.IsMouseOver(e.Location))
            {
                this.mouseState.Enter();

                this.CacheAction(this.OnMouseEnter);

                return;
            }

            if (this.mouseState.IsMouseOver && !this.IsMouseOver(e.Location))
            {
                this.mouseState.Leave();

                this.CacheAction(this.OnMouseLeave);

                return;
            }
        }

        private void EventMouseUp(object sender, MMMouseEventArgs e)
        {
            if (this.IsLButton(e))
            {
                this.mouseState.LRelease();

                if (this.mouseState.IsMouseOver)
                {
                    this.CacheAction(this.OnMouseUpLeft);
                }
                else
                {
                    this.CacheAction(this.OnMouseUpOutLeft);
                }

                return;
            }
            else if (this.IsRButton(e))
            {
                this.mouseState.RRelease();

                if (this.mouseState.IsMouseOver)
                {
                    this.CacheAction(this.OnMouseUpRight);
                }
                else
                {
                    this.CacheAction(this.OnMouseUpOutRight);
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
            this.MouseDoubleClick?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Double_Click_Left));
            this.MouseDoubleClickLeft?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Double_Click_Left));
        }

        protected virtual void OnMouseDoubleClickRight()
        {
            this.MouseDoubleClick?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Double_Click_Right));
            this.MouseDoubleClickRight?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Double_Click_Right));
        }

        protected virtual  void OnMousePressOutLeft()
        {
            this.MousePressOut?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Press_Out_Left));
            this.MousePressOutLeft?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Press_Out_Left));
        }

        protected virtual void OnMousePressOutRight()
        {
            this.MousePressOut?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Pressed_Out_Right));
            this.MousePressOutRight?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Pressed_Out_Right));
        }

        protected virtual void OnMousePressLeft()
        {
            this.MousePress?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Press_Left));
            this.MousePressLeft?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Press_Left));
        }

        protected virtual void OnMousePressRight()
        {
            this.MousePress?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Press_Right));
            this.MousePressRight?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Press_Right));
        }

        protected virtual void OnMouseUpLeft()
        {
            this.MouseUp?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Up_Left));
            this.MouseUpLeft?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Up_Left));
        }

        protected virtual void OnMouseUpRight()
        {
            this.MouseUp?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Up_Right));
            this.MouseUpRight?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Up_Right));
        }

        private void OnMouseUpOutRight()
        {
            this.MouseUpOut?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Up_Out_Left));
            this.MouseUpOutRight?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Up_Out_Left));
        }

        private void OnMouseUpOutLeft()
        {
            this.MouseUpOut?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Up_Out_Right));
            this.MouseUpOutLeft?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Up_Out_Right));
        }

        protected virtual void OnMouseLeave()
        {
            this.MouseLeave?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Leave));
        }

        protected virtual void OnMouseEnter()
        {
            this.MouseEnter?.Invoke(this, new MMElementEventArgs(MMElementEvent.Mouse_Enter));
        }

        #endregion

        #region Element State Data

        /// <summary>
        /// Gets or sets a value indicating whether this should receive or send any events.
        /// </summary>
        public override bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                var changed = this.Enabled != value;
                if (changed)
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

                base.Enabled = value;
            }
        }

        /// TODO(Minor): Enable read-only setter by implementing a permission array.
        /// TODO(Minor): Migrate some advanced states to fields and properties.
        /// <summary>
        /// Frame states as Func<bool> to replace messy things like active, 
        /// visible. In order to enable logic passing, I decided to make them 
        /// Func<bool>.
        /// </summary>
        private readonly Func<bool>[] frameStates = new Func<bool>[(int)MMElementState.StateNum];

        public Func<bool> this[MMElementState state]
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
            for (var i = 0; i < (int)MMElementState.StateNum; i++)
            {
                this.frameStates[i] = () => false;
            }
        }

        private void InitializePickableStates()
        {
            this[MMElementState.Element_Is_Active] = () => this.Enabled;

            this[MMElementState.Mouse_Is_Over] = () => this.mouseState.IsMouseOver;

            this[MMElementState.Mouse_Is_Left_Pressed]        = () => this.mouseState.IsLButtonPressed && this.mouseState.IsMouseOver;
            this[MMElementState.Mouse_Is_Left_Pressed_Out]    = () => this.mouseState.IsLButtonPressed && !this.mouseState.IsMouseOver;
            this[MMElementState.Mouse_Is_Left_Released]       = () => this.mouseState.IsLButtonReleased;
            this[MMElementState.Mouse_Is_Left_Double_Clicked] = () => this.mouseState.IsLButtonDoubleClicked && this.mouseState.IsMouseOver;

            this[MMElementState.Mouse_Is_Right_Pressed]        = () => this.mouseState.IsRButtonPressed && this.mouseState.IsMouseOver;
            this[MMElementState.Mouse_Is_Right_Pressed_Out]    = () => this.mouseState.IsRButtonPressed && !this.mouseState.IsMouseOver;
            this[MMElementState.Mouse_Is_Right_Released]       = () => this.mouseState.IsRButtonReleased;
            this[MMElementState.Mouse_Is_Right_Double_Clicked] = () => this.mouseState.IsRButtonDoubleClicked && this.mouseState.IsMouseOver;
        }

        #endregion

        #region Mouse State Detection

        private readonly MouseStateAutomata mouseState = new MouseStateAutomata();

        protected bool IsLButton(MMMouseEventArgs e)
        {
            return e.Button == MMMouseButtons.Left;
        }

        protected bool IsMouseOver(Point location)
        {
            return this.Enabled && this.Bounds.Contains(location);
        }

        protected bool IsRButton(MMMouseEventArgs e)
        {
            return e.Button == MMMouseButtons.Right;
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            if (!((MMEntity)this).Enabled)
            {
                return;
            }

            this.ClearAction();
        }

        public override void UpdateInput(GameTime time)
        {
            if (!((MMEntity)this).Enabled)
            {
                return;
            }

            this.FlushAction();
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