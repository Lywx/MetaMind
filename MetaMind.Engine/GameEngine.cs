﻿namespace MetaMind.Engine
{
    using System;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    using Microsoft.Xna.Framework;

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

        #region Audio

        public AudioManager Audio { get; private set; }

        #endregion

        #region File

        public FolderManager Folder { get; private set; }

        #endregion

        #region Graphics

        public FontManager FontManager { get; private set; }

        public FontDrawer FontDrawer { get; set; }

        public GraphicsManager GraphicsManager { get; private set; }

        public GraphicsSettings GraphicsSettings { get; set; }

        public MessageManager Message { get; private set; }

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

        #endregion Components

        #region Constructors

        private GameEngine()
        {
            // All necessary components during construction are loaded here.
            // Other components are loaded in initialization.

            // Graphics
            this.GraphicsSettings = GraphicsSettings.GetInstance(this);
            this.GraphicsManager  = GraphicsManager .GetInstance(this);

            // Screen
            this.Screen = ScreenManager.GetInstance(this, new ScreenSettings());

            // Content
            this.Content.RootDirectory = "Content";

            // Game
            this.Games = GameManager.GetInstance(this);
        }

        #endregion Consructors

        #region Initializations

        protected override void Initialize()
        {
            // Graphics
            this.GraphicsSettings.Initialize();
            this.GraphicsManager .Initialize();

            // Audio
            this.Audio = AudioManager.GetInstance(this);

            // Folder
            this.Folder = FolderManager.GetInstance();

            // Processes
            this.Process = ProcessManager.GetInstance(this);

            // Events
            this.Event = EventManager.GetInstance(this);

            // Input
            this.InputEvent    = InputEvent.GetInstance(this);
            this.InputState = InputState.GetInstance();

            // Font
            this.FontManager = FontManager.GetInstance(this);

            // Message
            this.Message = MessageManager.GetInstance(this, new MessageSettings());

            // Service
            GameEngineService.Provide(new Random((int)DateTime.Now.Ticks));

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
            this.InputState.UpdateInput(gameTime);
            this.InputEvent.UpdateInput(gameTime);
            this.Screen.UpdateInput(gameTime);
        }

        #endregion Update and Draw

        #region System Events

        protected override void OnExiting(object sender, EventArgs args)
        {
            this.Games.OnExiting();
            base      .OnExiting(sender, args);
        }

        #endregion 
    }
}