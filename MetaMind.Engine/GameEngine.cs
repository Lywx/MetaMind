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

        public static GameEngine GetInstance()
        {
            return Singleton ?? (Singleton = new GameEngine());
        }

        private static GameEngine Singleton { get; set; }

        #endregion

        #region Components

        #region File

        public ContentManager ContentManager { get; private set; }

        public FolderManager FolderManager { get; private set; }

        #endregion

        #region Sound

        public AudioManager AudioManager { get; private set; }

        #endregion

        #region Graphics

        public FontManager FontManager { get; private set; }

        public GraphicsManager GraphicsManager { get; private set; }

        public GraphicsSettings GraphicsSettings { get; set; }

        public MessageManager MessageManager { get; private set; }

        public ScreenManager ScreenManager { get; private set; }

        #endregion

        #region Input

        public InputEventManager InputEventManager { get; private set; }

        public InputSequenceManager InputSequenceManager { get; private set; }

        #endregion

        #region Interop

        public EventManager EventManager { get; private set; }

        public ProcessManager ProcessManager { get; private set; }

        #endregion

        public GameManager GameManager { get; private set; }

        #endregion Components

        #region Constructors

        private GameEngine()
        {
            // All necessary components during construction are loaded here.
            // Other components are loaded in initialization.

            // Graphics
            GraphicsSettings = GraphicsSettings.GetInstance(this);
            GraphicsManager  = GraphicsManager .GetInstance(this);

            // Screen
            ScreenManager = ScreenManager.GetInstance(this, new ScreenSettings());

            // Content
            this.Content.RootDirectory = "Content";
            ContentManager = this.Content;

            // Game
            GameManager = GameManager.GetInstance(this);
        }

        #endregion Consructors

        #region Initializations

        protected override void Initialize()
        {
            // Graphics
            this.GraphicsSettings.Initialize();
            this.GraphicsManager .Initialize();

            // Audio
            this.AudioManager = AudioManager.GetInstance(this);

            // Folder
            this.FolderManager = FolderManager.GetInstance();

            // Process
            this.ProcessManager = ProcessManager.GetInstance(this);

            // Event
            this.EventManager = EventManager.GetInstance(this);

            // Input
            this.InputEventManager    = InputEventManager.GetInstance(this);
            this.InputSequenceManager = InputSequenceManager.GetInstance();

            // Font
            this.FontManager = FontManager.GetInstance(this);

            // Message
            this.MessageManager = MessageManager.GetInstance(this);

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