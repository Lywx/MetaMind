namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Graphics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public sealed class GameEngine : Microsoft.Xna.Framework.Game
    {
        #region Singleton

        private static GameEngine singleton;

        public static GameEngine Instance
        {
            get
            {
                return singleton ?? (singleton = new GameEngine());
            }
        }

        #endregion

        #region Components

        #region File

        public static ContentManager ContentManager { get; private set; }

        public static FolderManager FolderManager { get; private set; }

        #endregion

        #region Sound

        public static AudioManager AudioManager { get; private set; }

        #endregion

        #region Graphics

        public static FontManager FontManager { get; private set; }

        public static GraphicsManager GraphicsManager { get; private set; }

        public static GraphicsSettings GraphicsSettings { get; set; }

        public static MessageManager MessageManager { get; private set; }

        public static ScreenManager ScreenManager { get; private set; }

        #endregion

        #region Input

        public static InputEventManager InputEventManager { get; private set; }

        public static InputSequenceManager InputSequenceManager { get; private set; }

        #endregion

        #region Interop

        public static EventManager EventManager { get; private set; }

        public static ProcessManager ProcessManager { get; private set; }

        #endregion

        public static GameManager GameManager { get; private set; }

        #endregion Components

        #region Constructors

        private GameEngine()
        {
            // all necessary components during construction are loaded here.
            // other components are loaded in initialization.

            // graphics
            GraphicsSettings = GraphicsSettings.GetInstance();
            GraphicsManager  = GraphicsManager .GetInstance(this);

            // screen
            ScreenManager = ScreenManager.GetInstance(this, new ScreenSettings());

            // content
            this.Content.RootDirectory = "Content";
            ContentManager = this.Content;

            // game
            GameManager = GameManager.GetInstance(this);
        }

        #endregion Consructors

        #region Initializations

        protected override void Initialize()
        {
            // graphics
            GraphicsSettings.Initialize();
            GraphicsManager .Initialize();

            // audio
            AudioManager = AudioManager.GetInstance(this);

            // folder
            FolderManager = FolderManager.GetInstance();

            // process
            ProcessManager = ProcessManager.GetInstance(this);

            // event
            EventManager = EventManager.GetInstance(this);

            // input
            InputEventManager    = InputEventManager   .GetInstance(this);
            InputSequenceManager = InputSequenceManager.GetInstance();

            // font
            FontManager = FontManager.GetInstance();

            // message
            MessageManager = MessageManager.GetInstance();

            base.Initialize();
        }

        #endregion Initializations

        #region Load and Unload

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

        #endregion

        #region Update and Draw

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        #endregion Update and Draw

        #region System Events

        protected override void OnExiting(object sender, EventArgs args)
        {
            GameManager.OnExiting();
            base       .OnExiting(sender, args);
        }

        #endregion 
    }
}