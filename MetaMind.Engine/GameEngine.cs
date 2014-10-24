using System;
using System.Diagnostics;
using MetaMind.Engine.Components;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Engine
{
    public class GameEngine : Game
    {
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

        //---------------------------------------------------------------------

        #region Consructors

        public GameEngine()
        {
            //-----------------------------------------------------------------
            IsMouseVisible = true;
            IsFixedTimeStep = true;

            //-----------------------------------------------------------------
            // graphics
            GraphicsManager = new GraphicsDeviceManager( this )
            {
                PreferredBackBufferWidth = GraphicsSettings.Width,
                PreferredBackBufferHeight = GraphicsSettings.Height
            };
            GraphicsManager.ApplyChanges();
            //-----------------------------------------------------------------
            // content
            Content.RootDirectory = "Content";
            ContentManager = Content;
            //------------------------------------------------------------------
            // audio
            Debug.Assert( AudioManager == null );
            AudioManager = AudioManager.GetInstance( this, @"Content\Audio\Audio.xgs", @"Content\Audio\Wave Bank.xwb", @"Content\Audio\Sound Bank.xsb" );
            //-----------------------------------------------------------------
            // folder
            Debug.Assert( FolderManager == null );
            FolderManager = FolderManager.GetInstance();
            //------------------------------------------------------------------
            // screen
            Debug.Assert( ScreenManager == null );
            ScreenManager = ScreenManager.GetInstance( this );
            //------------------------------------------------------------------
            // process
            Debug.Assert( ProcessManager == null );
            ProcessManager = ProcessManager.GetInstance( this );
            //------------------------------------------------------------------
            // event
            Debug.Assert( EventManager == null );
            EventManager = EventManager.GetInstance( this );
            //------------------------------------------------------------------
            // input
            Debug.Assert( InputEventManager == null );
            InputEventManager = InputEventManager.GetInstance( this );
            Debug.Assert( InputSequenceManager == null );
            InputSequenceManager = InputSequenceManager.GetInstance();
            //------------------------------------------------------------------
            // font
            Debug.Assert( FontManager == null );
            FontManager = FontManager.GetInstance();
            //------------------------------------------------------------------
            // message
            Debug.Assert( MessageManager == null );
            MessageManager = MessageManager.GetInstance( MessageSettings.Default );
            //---------------------------------------------------------------------
            Debug.Assert( RunnerManager == null );
            RunnerManager = RunnerManager.GetInstance();
        }

        #endregion Consructors

        #region Initializations

        protected override void Initialize()
        {
            // fixed drawing order in 3d graphics
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            FontManager.LoadContent();
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            FontManager.UnloadContent();
            base.UnloadContent();
        }

        #endregion Initializations

        #region Update and Draw

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );
            base.Draw( gameTime );
        }

        #endregion Update and Draw

        #region System

        protected override void OnExiting( Object sender, EventArgs args )
        {
            RunnerManager.Exit();
            base.OnExiting( sender, args );
        }

        #endregion System
    }
}