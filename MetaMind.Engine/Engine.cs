using System;
using System.Collections.Generic;
using System.Diagnostics;
using MetaMind.Engine.Components;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Engine
{
    public class Engine : Game
    {
        #region Components

        public static AudioManager          AudioManager { get; private set; }
        public static ContentManager        ContentManager { get; private set; }
        public static EventManager          EventManager { get; private set; }
        public static List<EngineElement>   Elements { get; set; }
        public static FolderManager         FolderManager { get; private set; }
        public static FontManager           FontManager { get; private set; }
        public static GraphicsDeviceManager GraphicsManager { get; private set; }
        public static InputEventManager     InputEventManager { get; private set; }
        public static InputSequenceManager  InputSequenceManager { get; private set; }
        public static MessageManager        MessageManager { get; private set; }
        public static ProcessManager        ProcessManager { get; private set; }
        public static ScreenManager         ScreenManager { get; private set; }
        
        #endregion Components

        #region Consructors

        public Engine()
        {
            ConfigureEngine();

            SetupGraphics();
            SetupContent();
            SetupComponents();
        }

        private void ConfigureEngine()
        {
            IsMouseVisible = true;
            IsFixedTimeStep = true;
        }

        #endregion Consructors

        #region Initializations

        private void SetupComponents()
        {
            //------------------------------------------------------------------
            // audio
            Debug.Assert(AudioManager == null);
            AudioManager = AudioManager.GetInstance( this,
                @"Content\Audio\Audio.xgs",
                @"Content\Audio\Wave Bank.xwb",
                @"Content\Audio\Sound Bank.xsb"
                );
            //------------------------------------------------------------------
            // folder
            Debug.Assert(FolderManager == null);
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
            Debug.Assert( InputSequenceManager == null );
            InputEventManager = InputEventManager.GetInstance( this );
            InputSequenceManager = InputSequenceManager.GetInstance( this );
            //------------------------------------------------------------------
            // font
            Debug.Assert( FontManager == null );
            FontManager = FontManager.GetInstance();
            //------------------------------------------------------------------
            // message
            Debug.Assert( MessageManager == null );
            MessageManager = MessageManager.GetInstance( MessageSettings.Default );
            //------------------------------------------------------------------
            // engine components
            Debug.Assert( Elements == null );
            Elements = new List<EngineElement>();
        }

        private void SetupContent()
        {
            Content.RootDirectory = "Content";
            ContentManager = Content;
        }

        private void SetupGraphics()
        {
            GraphicsManager = new GraphicsDeviceManager( this )
            {
                PreferredBackBufferWidth = GraphicsSettings.Width,
                PreferredBackBufferHeight = GraphicsSettings.Height
            };
            GraphicsManager.ApplyChanges();

            // fixed drawing order in 3d graphics
            var graphicsDevice = new GraphicsDevice( GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters() )
            {
                DepthStencilState = DepthStencilState.Default
            };
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
            Elements.ForEach( component => component.OnExiting( sender, args ) );

            base.OnExiting( sender, args );
        }

        #endregion System
    }
}