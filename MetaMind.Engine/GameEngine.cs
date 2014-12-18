namespace MetaMind.Engine
{
    using System;
    using System.Diagnostics;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class GameEngine : Game
    {
        private int fps;

        #region Singleton

        private static GameEngine singleton;

        public static GameEngine GetInstance()
        {
            return singleton ?? (singleton = new GameEngine());
        }

        #endregion

        #region Consructors

        private GameEngine()
        {
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;

            // graphics
            //-----------------------------------------------------------------
            GraphicsManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GraphicsSettings.Width,
                PreferredBackBufferHeight = GraphicsSettings.Height
            };
            GraphicsManager.ApplyChanges();

            // content
            //-----------------------------------------------------------------
            this.Content.RootDirectory = "Content";
            ContentManager = this.Content;

            // audio
            //------------------------------------------------------------------
            Debug.Assert(AudioManager == null);
            AudioManager = AudioManager.GetInstance(
                this,
                @"Content\Audio\Audio.xgs",
                @"Content\Audio\Wave Bank.xwb",
                @"Content\Audio\Sound Bank.xsb");

            // folder
            //-----------------------------------------------------------------
            Debug.Assert(FolderManager == null);
            FolderManager = FolderManager.GetInstance();

            // screen
            //------------------------------------------------------------------
            Debug.Assert(ScreenManager == null);
            ScreenManager = ScreenManager.GetInstance(this, new ScreenManagerSettings());

            // process
            //------------------------------------------------------------------
            Debug.Assert(ProcessManager == null);
            ProcessManager = ProcessManager.GetInstance(this);

            // event
            //------------------------------------------------------------------
            Debug.Assert(EventManager == null);
            EventManager = EventManager.GetInstance(this);

            // input
            //------------------------------------------------------------------
            Debug.Assert(InputEventManager == null);
            InputEventManager = InputEventManager.GetInstance(this);
            Debug.Assert(InputSequenceManager == null);
            InputSequenceManager = InputSequenceManager.GetInstance();

            // font
            //------------------------------------------------------------------
            Debug.Assert(FontManager == null);
            FontManager = FontManager.GetInstance();

            // message
            //------------------------------------------------------------------
            Debug.Assert(MessageManager == null);
            MessageManager = MessageManager.GetInstance();

            // runner
            //---------------------------------------------------------------------
            Debug.Assert(RunnerManager == null);
            RunnerManager = RunnerManager.GetInstance(this);
        }

        #endregion Consructors

        #region Public Properties

        public static int Fps
        {
            get { return singleton.fps; }
            set
            {
                singleton.fps = value;

                singleton.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / (double)singleton.fps);
                singleton.IsFixedTimeStep = true;
            }
        }

        #endregion


        #region Components

        public static AudioManager AudioManager { get; private set; }

        public static ContentManager ContentManager { get; private set; }

        public static EventManager EventManager { get; private set; }

        public static FontManager FontManager { get; private set; }

        public static GraphicsDeviceManager GraphicsManager { get; private set; }

        public static InputEventManager InputEventManager { get; private set; }

        public static InputSequenceManager InputSequenceManager { get; private set; }

        public static MessageManager MessageManager { get; private set; }

        public static ProcessManager ProcessManager { get; private set; }

        public static RunnerManager RunnerManager { get; private set; }

        public static ScreenManager ScreenManager { get; private set; }

        private static FolderManager FolderManager { get; set; }

        #endregion Components

        #region Initializations

        public void TriggerCenter()
        {
            // center game window
            var screen = GraphicsSettings.Screen;
            singleton.Window.Position = new Point(
                screen.Bounds.X + (screen.Bounds.Width - GraphicsSettings.Width) / 2,
                screen.Bounds.Y + (screen.Bounds.Height - GraphicsSettings.Height) / 2);

            // set width and height
            GraphicsManager.PreferredBackBufferWidth = GraphicsSettings.Width;
            GraphicsManager.PreferredBackBufferHeight = GraphicsSettings.Height;
            GraphicsManager.ApplyChanges();
        }

        protected override void Initialize()
        {
            // fixed drawing order in 3d graphics
            this.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            FontManager.LoadContent();
            base       .LoadContent();
        }

        protected override void UnloadContent()
        {
            FontManager.UnloadContent();
            base       .UnloadContent();
        }
        #endregion Initializations

        #region Update and Draw

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        #endregion Update and Draw

        #region System

        protected override void OnExiting(object sender, EventArgs args)
        {
            RunnerManager.OnExiting();
            base         .OnExiting(sender, args);
        }

        #endregion System
    }
}