namespace MetaMind.Engine.Gui.Control
{
    public class ControlManager : GameControllableComponent
    {
        public ControlManager(GameEngine engine) : base(engine)
        {

        }
    }


//    public class ControlManager : GameControllableComponent
//    {
//        private struct ControlStates
//        {
//            public Control[] Buttons;

//            public int Click;

//            public Control Over;
//        }

//        internal readonly int _MenuDelay = 500;

//        internal readonly int _TooltipDelay = 500;

//        internal readonly int _DoubleClickTime = 500;

//        internal readonly int _TextureResizeIncrement = 32;

//        internal readonly RenderTargetUsage RenderTargetUsage = RenderTargetUsage.DiscardContents;

//        private bool deviceReset = false;

//        private bool renderTargetValid = false;

//        private int targetFrames = 60;

//        private long drawTime = 0;

//        private long updateTime = 0;

//        private bool inputEnabled = true;

//        public ControlCollection Controls { get; } = null;

//        private Control focusedControl = null;

//        #region Tooltip

//        /// <summary>
//        /// Enables or disables showing of tooltips globally.
//        /// </summary>
//        public bool TooltipsEnabled { get; } = true;

//        #endregion

//        private ControlStates states = new ControlStates();

//        private KeyboardLayout keyboardLayout = null;

//        private List<KeyboardLayout> keyboardLayouts = new List<KeyboardLayout>();

//        private bool autoUnfocus = true;

//        private bool autoCreateRenderTarget = true;

//        #region //// Properties ////////

//        /// <summary>
//        /// Gets or sets an application cursor.
//        /// </summary>
//        public Cursor Cursor { get; set; } = null;

//        /// <summary>
//        /// Should a software cursor be drawn? Very handy on a PC build.
//        /// </summary>
//        public bool ShowSoftwareCursor { get; set; } = false;

//        /// <summary>
//        /// Returns associated <see cref="GraphicsDevice"/>.
//        /// </summary>
//        public virtual new GraphicsDevice GraphicsDevice { get { return base.GraphicsDevice; } }

//        /// <summary>
//        /// Gets or sets the depth value used for rendering sprites.
//        /// </summary>
//        public virtual float GlobalDepth { get; set; } = 0.0f;

//        /// <summary>
//        /// Enables or disables input processing.                   
//        /// </summary>
//        public virtual bool InputEnabled { get { return inputEnabled; } set { inputEnabled = value; } }

//        /// <summary>
//        /// Gets or sets render target for drawing.                 
//        /// </summary>    
//        public RenderTarget2D RenderTarget { get; set; } = null;

//        /// <summary>
//        /// Gets or sets update interval for drawing, logic and input.                           
//        /// </summary>    
//        public virtual int TargetFrames { get { return targetFrames; } set { targetFrames = value; } }

//        /// <summary>
//        /// Gets or sets collection of active keyboard layouts.     
//        /// </summary>
//        public virtual List<KeyboardLayout> KeyboardLayouts
//        {
//            get { return keyboardLayouts; }
//            set { keyboardLayouts = value; }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating if Guide component can be used
//        /// </summary>
//        public bool UseGuide { get; set; } = false;

//        /// <summary>
//        /// Gets or sets a value indicating if a control should unfocus if you click outside on the screen.
//        /// </summary>
 
//        public virtual bool AutoUnfocus
//        {
//            get { return autoUnfocus; }
//            set { autoUnfocus = value; }
//        }

//        /// <summary>
//        /// Gets or sets a value indicating wheter Manager should create render target automatically.
//        /// </summary>    
//        public virtual bool AutoCreateRenderTarget
//        {
//            get { return autoCreateRenderTarget; }
//            set { autoCreateRenderTarget = value; }
//        }

//        /// <summary>
//        /// Gets width of the selected render target in pixels.
//        /// </summary>
//        public virtual int TargetWidth
//        {
//            get
//            {
//                if (this.RenderTarget != null)
//                {
//                    return this.RenderTarget.Width;
//                }
//                else return ScreenWidth;
//            }
//        }

//        /// <summary>
//        /// Gets height of the selected render target in pixels.
//        /// </summary>
//        public virtual int TargetHeight
//        {
//            get
//            {
//                if (this.RenderTarget != null)
//                {
//                    return this.RenderTarget.Height;
//                }
//                else return ScreenHeight;
//            }
//        }

//        /// <summary>
//        /// Gets current width of the screen in pixels.
//        /// </summary>
//        public virtual int ScreenWidth
//        {
//            get
//            {
//                if (GraphicsDevice != null)
//                {
//                    return GraphicsDevice.PresentationParameters.BackBufferWidth;
//                }
//                else return 0;
//            }
//        }

//        /// <summary>
//        /// Gets current height of the screen in pixels.
//        /// </summary>
//        public virtual int ScreenHeight
//        {
//            get
//            {
//                if (GraphicsDevice != null)
//                {
//                    return GraphicsDevice.PresentationParameters.BackBufferHeight;
//                }
//                else return 0;
//            }
//        }

//        /// <summary>
//        /// Returns currently focused control.
//        /// </summary>
//        public Control FocusedControl
//        {
//            get
//            {
//                return focusedControl;
//            }

//            internal set
//            {
//                if (value != null && value.Visible && value.Enabled)
//                {
//                    if (value.CanFocus)
//                    {
//                        if (focusedControl == null || (focusedControl != null && value.Root != focusedControl.Root) || !value.IsRoot)
//                        {
//                            if (focusedControl != null && focusedControl != value)
//                            {
//                                focusedControl.Focused = false;
//                            }

//                            focusedControl = value;
//                        }
//                    }
//                    else if (!value.CanFocus)
//                    {
//                        if (focusedControl != null && value.Root != focusedControl.Root)
//                        {
//                            if (focusedControl != value.Root)
//                            {
//                                focusedControl.Focused = false;
//                            }
//                            focusedControl = value.Root;
//                        }
//                        else if (focusedControl == null)
//                        {
//                            focusedControl = value.Root;
//                        }
//                    }

//                    this.BringToFront(value.Root);
//                }
//                else if (value == null)
//                {
//                    focusedControl = value;
//                }
//            }
//        }

//        internal virtual ControlCollection OrderList { get; } = null;

//        #endregion

//        #region Events

//        /// <summary>
//        /// Occurs when the GraphicsDevice settings are changed.
//        /// </summary>
//        public event EventHandler<PreparingDeviceSettingsEventArgs> DeviceSettingsChanged;

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// Initializes a new instance of the Manager class.
//        /// </summary>
//        /// <param name="engine">
//        /// The Game class.
//        /// </param>
//        /// <param name="graphics">
//        /// The GraphicsDeviceManager class provided by the Game class.
//        /// </param>
//        /// <param name="skin">
//        /// The name of the skin being loaded at the start.
//        /// </param>
//        public ControlManager(GameEngine engine, GraphicsDeviceManager graphics, string skin)
//            : base(engine)
//        {
//            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleUnhadledExceptions);

//            input = new InputSystem(this, new InputOffset(0, 0, 1f, 1f));

//            components = new List<Component>();
//            Controls = new ControlCollection();
//            this.OrderList = new ControlCollection();

//            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(PrepareGraphicsDevice);

//            skinName = skin;

//            states.Buttons = new Control[32];
//            states.Click = -1;
//            states.Over = null;

//            input.MouseDown += new MouseEventHandler(MouseDownProcess);
//            input.MouseUp += new MouseEventHandler(MouseUpProcess);
//            input.MousePress += new MouseEventHandler(MousePressProcess);
//            input.MouseMove += new MouseEventHandler(MouseMoveProcess);
//            input.MouseScroll += new MouseEventHandler(MouseScrollProcess);

//            input.GamePadDown += new GamePadEventHandler(GamePadDownProcess);
//            input.GamePadUp += new GamePadEventHandler(GamePadUpProcess);
//            input.GamePadPress += new GamePadEventHandler(GamePadPressProcess);

//            input.KeyDown += new KeyEventHandler(KeyDownProcess);
//            input.KeyUp += new KeyEventHandler(KeyUpProcess);
//            input.KeyPress += new KeyEventHandler(KeyPressProcess);

//            keyboardLayouts.Add(new KeyboardLayout());
//            keyboardLayouts.Add(new CzechKeyboardLayout());
//            keyboardLayouts.Add(new GermanKeyboardLayout());
//        }

//        #endregion

//        #region

//        #endregion

//        public void SetCursor(Cursor cursor)
//        {
//            this.Cursor = cursor;
//            if (this.Cursor.CursorTexture == null)
//            {
//                this.Cursor.CursorTexture = Texture2D.FromStream(GraphicsDevice, new FileStream(
//                    this.Cursor.cursorPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None));
//            }
//        }

//        private void InitializeControls()
//        {
//            // Initializing all controls created, even not visible or 
//            // not added to the manager or another parent.
//            foreach (Control c in Control.Stack)
//            {
//                c.Initialize();
//            }
//        }

//        private void SortLevel(ControlCollection cs)
//        {
//            if (cs != null)
//            {
//                foreach (Control c in cs)
//                {
//                    if (c.Visible)
//                    {
//                        OrderList.Add(c);
//                        SortLevel(c.Controls as ControlCollection);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Method used as an event handler for the GraphicsDeviceManager.PreparingDeviceSettings event.
//        /// </summary>
//        protected virtual void PrepareGraphicsDevice(object sender, PreparingDeviceSettingsEventArgs e)
//        {
//            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = this.RenderTargetUsage;
//            int w = e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth;
//            int h = e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight;

//            foreach (Control c in Controls)
//            {
//                SetMaxSize(c, w, h);
//            }

//            if (DeviceSettingsChanged != null) DeviceSettingsChanged.Invoke(new DeviceEventArgs(e));
//        }

//        private void SetMaxSize(Control c, int w, int h)
//        {
//            if (c.Width > w)
//            {
//                w -= (c.Skin != null) ? c.Skin.OriginMargins.Horizontal : 0;
//                c.Width = w;
//            }
//            if (c.Height > h)
//            {
//                h -= (c.Skin != null) ? c.Skin.OriginMargins.Vertical : 0;
//                c.Height = h;
//            }

//            foreach (Control cx in c.Controls)
//            {
//                SetMaxSize(cx, w, h);
//            }
//        }

//        /// <summary>
//        /// Initializes the controls manager.
//        /// </summary>    
//        public override void Initialize()
//        {
//            base.Initialize();

//            Game.Window.ClientSizeChanged += (object sender, EventArgs e) =>
//            {
//                InvalidateRenderTarget();
//            };

//            if (autoCreateRenderTarget)
//            {
//                if (this.RenderTarget != null)
//                {
//                    this.RenderTarget.Dispose();
//                }
//                this.RenderTarget = CreateRenderTarget();
//            }

//            GraphicsDevice.DeviceReset += new EventHandler<EventArgs>(GraphicsDevice_DeviceReset);

//            SetSkin(skinName);
//        }

//        private void InvalidateRenderTarget()
//        {
//            renderTargetValid = false;
//        }

//        public virtual RenderTarget2D CreateRenderTarget()
//        {
//            return CreateRenderTarget(ScreenWidth, ScreenHeight);
//        }

//        public virtual RenderTarget2D CreateRenderTarget(int width, int height)
//        {
//            Input.InputOffset = new InputOffset(0, 0, ScreenWidth / (float)width, ScreenHeight / (float)height);
//            return new RenderTarget2D(GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None, GraphicsDevice.PresentationParameters.MultiSampleCount, this.RenderTargetUsage);
//        }

//        /// <summary>
//        /// Sets the new skin.
//        /// </summary>
//        /// <param name="skin">
//        /// The skin being set.
//        /// </param>
//        public virtual void SetSkin(Skin skin)
//        {
//            Add(this.skin);
//            skinName = this.skin.Name;

//#if (!XBOX && !XBOX_FAKE)
//            if (this.skin.Cursors["Default"] != null)
//            {
//                SetCursor(this.skin.Cursors["Default"].Resource);
//            }
//#endif

//            InitSkins();
//            if (SkinChanged != null) SkinChanged.Invoke(new EventArgs());

//            this.InitializeControls();
//        }

//        /// <summary>
//        /// Brings the control to the front of the z-order.
//        /// </summary>
//        /// <param name="control">
//        /// The control being brought to the front.
//        /// </param>
//        public virtual void BringToFront(Control control)
//        {
//            if (control != null && !control.StayOnBack)
//            {
//                ControlCollection cs = (control.Parent == null) ? Controls as ControlCollection : control.Parent.Controls as ControlCollection;
//                if (cs.Contains(control))
//                {
//                    cs.Remove(control);
//                    if (!control.StayOnTop)
//                    {
//                        int pos = cs.Count;
//                        for (int i = cs.Count - 1; i >= 0; i--)
//                        {
//                            if (!cs[i].StayOnTop)
//                            {
//                                break;
//                            }
//                            pos = i;
//                        }
//                        cs.Insert(pos, control);
//                    }
//                    else
//                    {
//                        cs.Add(control);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Sends the control to the back of the z-order.
//        /// </summary>
//        /// <param name="control">
//        /// The control being sent back.
//        /// </param>
//        public virtual void SendToBack(Control control)
//        {
//            if (control != null && !control.StayOnTop)
//            {
//                ControlCollection cs = (control.Parent == null) ? Controls as ControlCollection : control.Parent.Controls as ControlCollection;
//                if (cs.Contains(control))
//                {
//                    cs.Remove(control);
//                    if (!control.StayOnBack)
//                    {
//                        int pos = 0;
//                        for (int i = 0; i < cs.Count; i++)
//                        {
//                            if (!cs[i].StayOnBack)
//                            {
//                                break;
//                            }
//                            pos = i;
//                        }
//                        cs.Insert(pos, control);
//                    }
//                    else
//                    {
//                        cs.Insert(0, control);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Called when the manager needs to be updated.
//        /// </summary>
//        /// <param name="gameTime">
//        /// Time elapsed since the last call to Update.
//        /// </param>
//        public override void Update(GameTime gameTime)
//        {
//            updateTime += gameTime.ElapsedGameTime.Ticks;
//            double ms = TimeSpan.FromTicks(updateTime).TotalMilliseconds;

//            if (targetFrames == 0 || ms == 0 || ms >= (1000f / targetFrames))
//            {
//                TimeSpan span = TimeSpan.FromTicks(updateTime);
//                gameTime = new GameTime(gameTime.TotalGameTime, span);
//                updateTime = 0;

//                if (inputEnabled)
//                {
//                    input.Update(gameTime);
//                }

//                if (components != null)
//                {
//                    foreach (Component c in components)
//                    {
//                        c.Update(gameTime);
//                    }
//                }

//                ControlCollection list = new ControlCollection(Controls);

//                if (list != null)
//                {
//                    foreach (Control c in list)
//                    {
//                        c.Update(gameTime);
//                    }
//                }

//                OrderList.Clear();
//                SortLevel(Controls);
//            }
//        }

//        /// <summary>
//        /// Adds a component or a control to the manager.
//        /// </summary>
//        /// <param name="component">
//        /// The component or control being added.
//        /// </param>
//        public virtual void Add(Component component)
//        {
//            if (component != null)
//            {
//                if (component is Control && !Controls.Contains(component as Control))
//                {
//                    Control c = (Control)component;

//                    if (c.Parent != null) c.Parent.Remove(c);

//                    Controls.Add(c);
//                    c.Manager = this;
//                    c.Parent = null;
//                    if (focusedControl == null) c.Focused = true;

//                    DeviceSettingsChanged += new DeviceEventHandler((component as Control).OnDeviceSettingsChanged);
//                    SkinChanging += new SkinEventHandler((component as Control).OnSkinChanging);
//                    SkinChanged += new SkinEventHandler((component as Control).OnSkinChanged);
//                }
//                else if (!(component is Control) && !components.Contains(component))
//                {
//                    components.Add(component);
//                    component.Manager = this;
//                }
//            }
//        }

//        /// <summary>
//        /// Removes a component or a control from the manager.
//        /// </summary>
//        /// <param name="component">
//        /// The component or control being removed.
//        /// </param>
//        public virtual void Remove(Component component)
//        {
//            if (component != null)
//            {
//                if (component is Control)
//                {
//                    Control c = component as Control;
//                    SkinChanging -= c.OnSkinChanging;
//                    SkinChanged -= c.OnSkinChanged;
//                    DeviceSettingsChanged -= c.OnDeviceSettingsChanged;

//                    if (c.Focused) c.Focused = false;
//                    Controls.Remove(c);
//                }
//                else
//                {
//                    components.Remove(component);
//                }
//            }
//        }

//        #region Draw

//        /// <summary>
//        /// Renders all controls added to the manager.
//        /// </summary>
//        /// <param name="gameTime">
//        /// Time passed since the last call to Draw.
//        /// </param>
//        public override void BeginDraw(GameTime gameTime)
//        {
//            if (!renderTargetValid && AutoCreateRenderTarget)
//            {
//                if (this.RenderTarget != null) RenderTarget.Dispose();
//                RenderTarget = CreateRenderTarget();
//                renderer = new Renderer(this);
//                renderTargetValid = true;
//            }

//            this.Draw(gameTime);
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            if (this.RenderTarget != null)
//            {
//                drawTime += gameTime.ElapsedGameTime.Ticks;
//                double ms = TimeSpan.FromTicks(drawTime).TotalMilliseconds;

//                //if (targetFrames == 0 || (ms == 0 || ms >= (1000f / targetFrames)))
//                //{
//                TimeSpan span = TimeSpan.FromTicks(drawTime);
//                gameTime = new GameTime(gameTime.TotalGameTime, span);
//                drawTime = 0;

//                if ((Controls != null))
//                {
//                    ControlCollection list = new ControlCollection();
//                    list.AddRange(Controls);

//                    foreach (Control c in list)
//                    {
//                        c.PrepareTexture(renderer, gameTime);
//                    }

//                    // Set the render target for all control in this.ControList 
//                    GraphicsDevice.SetRenderTarget(this.RenderTarget);
//                    GraphicsDevice.Clear(Color.Transparent);

//                    if (renderer != null)
//                    {
//                        foreach (Control c in list)
//                        {
//                            c.Render(renderer, gameTime);
//                        }
//                    }
//                }

//                if (this.ShowSoftwareCursor && Cursor != null)
//                {
//                    if (this.Cursor.CursorTexture == null)
//                    {
//                        this.Cursor.CursorTexture = Texture2D.FromStream(GraphicsDevice, new FileStream(
//                            this.Cursor.cursorPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None));
//                    }
//                    renderer.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
//                    MouseState mstate = Mouse.GetState();
//                    Rectangle rect = new Rectangle(mstate.X, mstate.Y, Cursor.Width, Cursor.Height);
//                    renderer.SpriteBatch.Draw(Cursor.CursorTexture, rect, null, Color.White, 0f, Cursor.HotSpot, SpriteEffects.None, 0f);
//                    renderer.SpriteBatch.End();
//                }

//                GraphicsDevice.SetRenderTarget(null);
//                //}
//            }
//            else
//            {
//                throw new Exception("Manager.RenderTarget has to be specified. Assign a render target or set Manager.AutoCreateRenderTarget property to true.");
//            }
//        }

//        /// <summary>
//        /// Draws texture resolved from RenderTarget used for rendering.
//        /// </summary>

//        public virtual void EndDraw()
//        {
//            this.EndDraw(new Rectangle(0, 0, ScreenWidth, ScreenHeight));
//        }

//        /// <summary>
//        /// Draws texture resolved from RenderTarget to specified rectangle.
//        /// </summary>

//        public virtual void EndDraw(Rectangle rect)
//        {
//            if (this.RenderTarget != null && !deviceReset)
//            {
//                renderer.Begin(BlendingMode.Default);
//                renderer.Draw(RenderTarget, rect, Color.White);
//                renderer.End();
//            }
//            else if (deviceReset)
//            {
//                deviceReset = false;
//            }
//        }

//        #endregion

//        public virtual Control GetControl(string name)
//        {
//            foreach (Control c in Controls)
//            {
//                if (string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase))
//                {
//                    return c;
//                }
//            }

//            return null;
//        }

//        private void HandleUnhadledExceptions(object sender, UnhandledExceptionEventArgs e)
//        {
//            if (LogUnhandledExceptions)
//            {
//                LogException(e.ExceptionObject as Exception);
//            }
//        }

//        private void GraphicsDevice_DeviceReset(object sender, EventArgs e)
//        {
//            deviceReset = true;
//            InvalidateRenderTarget();
//            /*if (AutoCreateRenderTarget) 
//            {
//              if (renderTarget != null) RenderTarget.Dispose();
//              RenderTarget = CreateRenderTarget();        
//            }
//            }*/
//        }

//        public virtual void LogException(Exception e)
//        {
//#if (!XBOX && !XBOX_FAKE)
//            string an = Assembly.GetEntryAssembly().Location;
//            Assembly asm = Assembly.GetAssembly(typeof(ControlManager));
//            string path = Path.GetDirectoryName(an);
//            string fn = path + "\\" + Path.GetFileNameWithoutExtension(asm.Location) + ".log";

//            File.AppendAllText(fn, "////////////////////////////////////////////////////////////////\n" +
//                                   "    Date: " + DateTime.Now.ToString() + "\n" +
//                                   "Assembly: " + Path.GetFileName(asm.Location) + "\n" +
//                                   " Version: " + asm.GetName().Version.ToString() + "\n" +
//                                   " Message: " + e.Message + "\n" +
//                                   "////////////////////////////////////////////////////////////////\n" +
//                                   e.StackTrace + "\n" +
//                                   "////////////////////////////////////////////////////////////////\n\n", Encoding.Default);
//#endif
//        }

//        #endregion

//        #region //// Input /////////////

//        private bool CheckParent(Control control, Point pos)
//        {
//            if (control.Parent != null && !CheckDetached(control))
//            {
//                Control parent = control.Parent;
//                Control root = control.Root;

//                Rectangle pr = new Rectangle(parent.AbsoluteLeft,
//                                             parent.AbsoluteTop,
//                                             parent.Width,
//                                             parent.Height);

//                Margins margins = root.Skin.ClientMargins;
//                Rectangle rr = new Rectangle(root.AbsoluteLeft + margins.Left,
//                                             root.AbsoluteTop + margins.Top,
//                                             root.OriginWidth - margins.Horizontal,
//                                             root.OriginHeight - margins.Vertical);

//                return (rr.Contains(pos) && pr.Contains(pos));
//            }

//            return true;
//        }

//        private bool CheckState(Control control)
//        {
//            bool modal = (ModalWindow == null) ? true : (ModalWindow == control.Root);

//            return (control != null && !control.Passive && control.Visible && control.Enabled && modal);
//        }

//        private bool CheckOrder(Control control, Point pos)
//        {
//            if (!CheckPosition(control, pos)) return false;

//            for (int i = OrderList.Count - 1; i > OrderList.IndexOf(control); i--)
//            {
//                Control c = OrderList[i];

//                if (!c.Passive && CheckPosition(c, pos) && CheckParent(c, pos))
//                {
//                    return false;
//                }
//            }

//            return true;
//        }

//        private bool CheckDetached(Control control)
//        {
//            bool ret = control.Detached;
//            if (control.Parent != null)
//            {
//                if (CheckDetached(control.Parent)) ret = true;
//            }
//            return ret;
//        }

//        private bool CheckPosition(Control control, Point pos)
//        {
//            return (control.AbsoluteLeft <= pos.X &&
//                    control.AbsoluteTop <= pos.Y &&
//                    control.AbsoluteLeft + control.Width >= pos.X &&
//                    control.AbsoluteTop + control.Height >= pos.Y &&
//                    CheckParent(control, pos));
//        }

//        private bool CheckButtons(int index)
//        {
//            for (int i = 0; i < states.Buttons.Length; i++)
//            {
//                if (i == index) continue;
//                if (states.Buttons[i] != null) return false;
//            }

//            return true;
//        }

//        private void TabNextControl(Control control)
//        {
//            int start = OrderList.IndexOf(control);
//            int i = start;

//            do
//            {
//                if (i < OrderList.Count - 1) i += 1;
//                else i = 0;
//            }
//            while ((OrderList[i].Root != control.Root || !OrderList[i].CanFocus || OrderList[i].IsRoot || !OrderList[i].Enabled) && i != start);

//            OrderList[i].Focused = true;
//        }

//        private void TabPrevControl(Control control)
//        {
//            int start = OrderList.IndexOf(control);
//            int i = start;

//            do
//            {
//                if (i > 0) i -= 1;
//                else i = OrderList.Count - 1;
//            }
//            while ((OrderList[i].Root != control.Root || !OrderList[i].CanFocus || OrderList[i].IsRoot || !OrderList[i].Enabled) && i != start);
//            OrderList[i].Focused = true;
//        }

//        private void ProcessArrows(Control control, KeyEventArgs kbe, GamePadEventArgs gpe)
//        {
//            Control c = control;
//            if (c.Parent != null && c.Parent.Controls != null)
//            {
//                int index = -1;

//                if ((kbe.Key == Keys.Left && !kbe.Handled) ||
//                    (gpe.Button == c.GamePadActions.Left && !gpe.Handled))
//                {
//                    int miny = int.MaxValue;
//                    int minx = int.MinValue;
//                    for (int i = 0; i < (c.Parent.Controls as ControlCollection).Count; i++)
//                    {
//                        Control cx = (c.Parent.Controls as ControlCollection)[i];
//                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

//                        int cay = (int)(c.Top + (c.Height / 2));
//                        int cby = (int)(cx.Top + (cx.Height / 2));

//                        if (Math.Abs(cay - cby) <= miny && (cx.Left + cx.Width) >= minx && (cx.Left + cx.Width) <= c.Left)
//                        {
//                            miny = Math.Abs(cay - cby);
//                            minx = cx.Left + cx.Width;
//                            index = i;
//                        }
//                    }
//                }
//                else if ((kbe.Key == Keys.Right && !kbe.Handled) ||
//                         (gpe.Button == c.GamePadActions.Right && !gpe.Handled))
//                {
//                    int miny = int.MaxValue;
//                    int minx = int.MaxValue;
//                    for (int i = 0; i < (c.Parent.Controls as ControlCollection).Count; i++)
//                    {
//                        Control cx = (c.Parent.Controls as ControlCollection)[i];
//                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

//                        int cay = (int)(c.Top + (c.Height / 2));
//                        int cby = (int)(cx.Top + (cx.Height / 2));

//                        if (Math.Abs(cay - cby) <= miny && cx.Left <= minx && cx.Left >= (c.Left + c.Width))
//                        {
//                            miny = Math.Abs(cay - cby);
//                            minx = cx.Left;
//                            index = i;
//                        }
//                    }
//                }
//                else if ((kbe.Key == Keys.Up && !kbe.Handled) ||
//                         (gpe.Button == c.GamePadActions.Up && !gpe.Handled))
//                {
//                    int miny = int.MinValue;
//                    int minx = int.MaxValue;
//                    for (int i = 0; i < (c.Parent.Controls as ControlCollection).Count; i++)
//                    {
//                        Control cx = (c.Parent.Controls as ControlCollection)[i];
//                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

//                        int cax = (int)(c.Left + (c.Width / 2));
//                        int cbx = (int)(cx.Left + (cx.Width / 2));

//                        if (Math.Abs(cax - cbx) <= minx && (cx.Top + cx.Height) >= miny && (cx.Top + cx.Height) <= c.Top)
//                        {
//                            minx = Math.Abs(cax - cbx);
//                            miny = cx.Top + cx.Height;
//                            index = i;
//                        }
//                    }
//                }
//                else if ((kbe.Key == Keys.Down && !kbe.Handled) ||
//                         (gpe.Button == c.GamePadActions.Down && !gpe.Handled))
//                {
//                    int miny = int.MaxValue;
//                    int minx = int.MaxValue;
//                    for (int i = 0; i < (c.Parent.Controls as ControlCollection).Count; i++)
//                    {
//                        Control cx = (c.Parent.Controls as ControlCollection)[i];
//                        if (cx == c || !cx.Visible || !cx.Enabled || cx.Passive || !cx.CanFocus) continue;

//                        int cax = (int)(c.Left + (c.Width / 2));
//                        int cbx = (int)(cx.Left + (cx.Width / 2));

//                        if (Math.Abs(cax - cbx) <= minx && cx.Top <= miny && cx.Top >= (c.Top + c.Height))
//                        {
//                            minx = Math.Abs(cax - cbx);
//                            miny = cx.Top;
//                            index = i;
//                        }
//                    }
//                }

//                if (index != -1)
//                {
//                    (c.Parent.Controls as ControlCollection)[index].Focused = true;
//                    kbe.Handled = true;
//                    gpe.Handled = true;
//                }
//            }
//        }

//        private void MouseDownProcess(object sender, MouseEventArgs e)
//        {
//            ControlCollection c = new ControlCollection();
//            c.AddRange(OrderList);

//            if (autoUnfocus && focusedControl != null && focusedControl.Root != modalWindow)
//            {
//                bool hit = false;

//                foreach (Control cx in Controls)
//                {
//                    if (cx.AbsoluteRect.Contains(e.Position))
//                    {
//                        hit = true;
//                        break;
//                    }
//                }
//                if (!hit)
//                {
//                    for (int i = 0; i < Control.Stack.Count; i++)
//                    {
//                        if (Control.Stack[i].Visible && Control.Stack[i].Detached && Control.Stack[i].AbsoluteRect.Contains(e.Position))
//                        {
//                            hit = true;
//                            break;
//                        }
//                    }
//                }
//                if (!hit) focusedControl.Focused = false;
//            }

//            for (int i = c.Count - 1; i >= 0; i--)
//            {
//                if (CheckState(c[i]) && CheckPosition(c[i], e.Position))
//                {
//                    states.Buttons[(int)e.Button] = c[i];
//                    c[i].SendMessage(Message.MouseDown, e);

//                    if (states.Click == -1)
//                    {
//                        states.Click = (int)e.Button;

//                        if (FocusedControl != null)
//                        {
//                            FocusedControl.Invalidate();
//                        }
//                        c[i].Focused = true;
//                    }
//                    return;
//                }
//            }

//            if (ModalWindow != null)
//            {
//#if (!XBOX && !XBOX_FAKE)
//                SystemSounds.Beep.Play();
//#endif
//            }
//        }

//        private void MouseUpProcess(object sender, MouseEventArgs e)
//        {
//            Control c = states.Buttons[(int)e.Button];
//            if (c != null)
//            {
//                if (CheckPosition(c, e.Position) && CheckOrder(c, e.Position) && states.Click == (int)e.Button && CheckButtons((int)e.Button))
//                {
//                    c.SendMessage(Message.Click, e);
//                }
//                states.Click = -1;
//                c.SendMessage(Message.MouseUp, e);
//                states.Buttons[(int)e.Button] = null;
//                MouseMoveProcess(sender, e);
//            }
//        }

//        private void MousePressProcess(object sender, MouseEventArgs e)
//        {
//            Control c = states.Buttons[(int)e.Button];
//            if (c != null)
//            {
//                if (CheckPosition(c, e.Position))
//                {
//                    c.SendMessage(Message.MousePress, e);
//                }
//            }
//        }

//        private void MouseMoveProcess(object sender, MouseEventArgs e)
//        {
//            ControlCollection c = new ControlCollection();
//            c.AddRange(OrderList);

//            for (int i = c.Count - 1; i >= 0; i--)
//            {
//                bool chpos = CheckPosition(c[i], e.Position);
//                bool chsta = CheckState(c[i]);

//                if (chsta && ((chpos && states.Over == c[i]) || (states.Buttons[(int)e.Button] == c[i])))
//                {
//                    c[i].SendMessage(Message.MouseMove, e);
//                    break;
//                }
//            }

//            for (int i = c.Count - 1; i >= 0; i--)
//            {
//                bool chpos = CheckPosition(c[i], e.Position);
//                bool chsta = CheckState(c[i]) || (c[i].Tooltip.Text != "" && c[i].Tooltip.Text != null && c[i].Visible);

//                if (chsta && !chpos && states.Over == c[i] && states.Buttons[(int)e.Button] == null)
//                {
//                    states.Over = null;
//                    c[i].SendMessage(Message.MouseOut, e);
//                    break;
//                }
//            }

//            for (int i = c.Count - 1; i >= 0; i--)
//            {
//                bool chpos = CheckPosition(c[i], e.Position);
//                bool chsta = CheckState(c[i]) || (c[i].Tooltip.Text != "" && c[i].Tooltip.Text != null && c[i].Visible);

//                if (chsta && chpos && states.Over != c[i] && states.Buttons[(int)e.Button] == null)
//                {
//                    if (states.Over != null)
//                    {
//                        states.Over.SendMessage(Message.MouseOut, e);
//                    }
//                    states.Over = c[i];
//                    c[i].SendMessage(Message.MouseOver, e);
//                    break;
//                }
//                else if (states.Over == c[i]) break;
//            }
//        }

//        /// <summary>
//        /// Processes mouse scroll events for the manager.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void MouseScrollProcess(object sender, MouseEventArgs e)
//        {
//            ControlCollection c = new ControlCollection();
//            c.AddRange(OrderList);

//            for (int i = c.Count - 1; i >= 0; i--)
//            {
//                bool chpos = CheckPosition(c[i], e.Position);
//                bool chsta = CheckState(c[i]);

//                if (chsta && chpos && states.Over == c[i])
//                {
//                    c[i].SendMessage(Message.MouseScroll, e);
//                    break;
//                }
//            }
//        }

//        void GamePadDownProcess(object sender, GamePadEventArgs e)
//        {
//            Control c = FocusedControl;

//            if (c != null && CheckState(c))
//            {
//                if (states.Click == -1)
//                {
//                    states.Click = (int)e.Button;
//                }
//                states.Buttons[(int)e.Button] = c;
//                c.SendMessage(Message.GamePadDown, e);

//                if (e.Button == c.GamePadActions.Click)
//                {
//                    c.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
//                }
//            }
//        }

//        void GamePadUpProcess(object sender, GamePadEventArgs e)
//        {
//            Control c = states.Buttons[(int)e.Button];

//            if (c != null)
//            {
//                if (e.Button == c.GamePadActions.Press)
//                {
//                    c.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
//                }
//                states.Click = -1;
//                states.Buttons[(int)e.Button] = null;
//                c.SendMessage(Message.GamePadUp, e);
//            }
//        }

//        void GamePadPressProcess(object sender, GamePadEventArgs e)
//        {
//            Control c = states.Buttons[(int)e.Button];
//            if (c != null)
//            {
//                c.SendMessage(Message.GamePadPress, e);

//                if ((e.Button == c.GamePadActions.Right ||
//                     e.Button == c.GamePadActions.Left ||
//                     e.Button == c.GamePadActions.Up ||
//                     e.Button == c.GamePadActions.Down) && !e.Handled && CheckButtons((int)e.Button))
//                {
//                    ProcessArrows(c, new KeyEventArgs(), e);
//                    GamePadDownProcess(sender, e);
//                }
//                else if (e.Button == c.GamePadActions.NextControl && !e.Handled && CheckButtons((int)e.Button))
//                {
//                    TabNextControl(c);
//                    GamePadDownProcess(sender, e);
//                }
//                else if (e.Button == c.GamePadActions.PrevControl && !e.Handled && CheckButtons((int)e.Button))
//                {
//                    TabPrevControl(c);
//                    GamePadDownProcess(sender, e);
//                }
//            }
//        }

//        void KeyDownProcess(object sender, KeyEventArgs e)
//        {
//            Control c = FocusedControl;

//            if (c != null && CheckState(c))
//            {
//                if (states.Click == -1)
//                {
//                    states.Click = (int)MouseButton.None;
//                }
//                states.Buttons[(int)MouseButton.None] = c;
//                c.SendMessage(Message.KeyDown, e);

//                if (e.Key == Keys.Enter)
//                {
//                    c.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
//                }
//            }
//        }

//        void KeyUpProcess(object sender, KeyEventArgs e)
//        {
//            Control c = states.Buttons[(int)MouseButton.None];

//            if (c != null)
//            {
//                if (e.Key == Keys.Space)
//                {
//                    c.SendMessage(Message.Click, new MouseEventArgs(new MouseState(), MouseButton.None, Point.Zero));
//                }
//                states.Click = -1;
//                states.Buttons[(int)MouseButton.None] = null;
//                c.SendMessage(Message.KeyUp, e);
//            }
//        }

//        void KeyPressProcess(object sender, KeyEventArgs e)
//        {
//            Control c = states.Buttons[(int)MouseButton.None];
//            if (c != null)
//            {
//                c.SendMessage(Message.KeyPress, e);

//                if ((e.Key == Keys.Right ||
//                     e.Key == Keys.Left ||
//                     e.Key == Keys.Up ||
//                     e.Key == Keys.Down) && !e.Handled && CheckButtons((int)MouseButton.None))
//                {
//                    ProcessArrows(c, e, new GamePadEventArgs(PlayerIndex.One));
//                    KeyDownProcess(sender, e);
//                }
//                else if (e.Key == Keys.Tab && !e.Shift && !e.Handled && CheckButtons((int)MouseButton.None))
//                {
//                    TabNextControl(c);
//                    KeyDownProcess(sender, e);
//                }
//                else if (e.Key == Keys.Tab && e.Shift && !e.Handled && CheckButtons((int)MouseButton.None))
//                {
//                    TabPrevControl(c);
//                    KeyDownProcess(sender, e);
//                }
//            }
//        }

//        #endregion

//        #region IDisposable

//        /// <summary>
//        /// Gets a value indicating whether Manager is in the process of disposing.
//        /// </summary>
//        public bool Disposing { get; private set; } = false;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                this.Disposing = true;

//                // Recursively disposing all controls added to the manager and its child controls.
//                if (this.Controls != null)
//                {
//                    var c = this.Controls.Count;

//                    for (var i = 0; i < c; i++)
//                    {
//                        if (this.Controls.Count > 0) this.Controls[0].Dispose();
//                    }
//                }

//                // Disposing all components added to manager.
//                if (this.components != null)
//                {
//                    int c = this.components.Count;
//                    for (int i = 0; i < c; i++)
//                    {
//                        if (this.components.Count > 0) this.components[0].Dispose();
//                    }
//                }
//            }

//            if (this.GraphicsDevice != null)
//            {
//                this.GraphicsDevice.DeviceReset -= this.GraphicsDevice_DeviceReset;
//            }

//            base.Dispose(disposing);
//        }

//        #endregion
//    }
}
