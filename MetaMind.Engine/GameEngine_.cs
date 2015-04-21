namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public sealed class GameEngine_ : Microsoft.Xna.Framework.Game, IGameEngine
    {
        #region Components

        #region Audio

        public AudioManager AudioManager { get; private set; }

        #endregion

        #region File

        public FolderManager Folder { get; private set; }

        #endregion

        #region Graphics

        public FontManager FontManager { get; private set; }

        public StringDrawer StringDrawer { get; set; }

        public GraphicsManager GraphicsManager { get; private set; }

        public GraphicsSettings GraphicsSettings { get; set; }

        public ScreenManager Screen { get; private set; }

        #endregion

        #region Input

        public InputEvent InputEvent { get; private set; }

        public InputState InputState { get; private set; }

        #endregion

        #region Interop

        public EventManager Event { get; private set; }

        public ProcessManager Process { get; private set; }

        #endregion

        public GameManager Games { get; private set; }

        public static IGameService Service { get; private set; }

        #endregion Components

        #region Constructors

        public GameEngine_()
        {
            // All necessary components during construction are loaded here.
            // Other components are loaded in initialization.

            // Graphics
            this.GraphicsSettings = new GraphicsSettings();
            this.GraphicsManager  = new GraphicsManager(this, this.GraphicsSettings);

            // Screen
            this.Screen = ScreenManager.GetComponent(this, new ScreenSettings(), 3);

            // Content
            this.Content.RootDirectory = "Content";

            // Game
            this.Games = GameManager.GetComponent(this);
        }

        #endregion Consructors

        #region Initializations

        protected override void Initialize()
        {
            // Graphics
            this.GraphicsManager .Initialize();
            
            // Folder
            this.Folder = FolderManager.GetComponent();

            // Process
            this.Process = ProcessManager.GetComponent(this, 5);

            // Event
            this.Event = EventManager.GetComponent(this, 4);

            // Input
            this.InputEvent = InputEvent.GetComponent(this, 1);
            this.InputState = InputState.GetComponent(this, 2);

            // Font
            this.FontManager = FontManager.GetComponent(this);

            // Service
            Service = new GameEngineService(this.);

            // Extra components as GameEntity
            this.StringDrawer = new StringDrawer();

            base.Initialize();
        }

        #endregion Initializations

        #region Load and Unload

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        #endregion

        #region Update and Draw

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            this.UpdateInput(gameTime);
            base.Update(gameTime);
        }

        private void UpdateInput(GameTime gameTime)
        {
            // TODO: Playtest

            this.Input.UpdateInput(gameTime);

            // TODO: More
            this.Screen    .UpdateInput(gameTime);
        }

        #endregion Update and Draw

        #region System Events

        #endregion 
    }
}