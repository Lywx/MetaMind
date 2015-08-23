// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Testimony.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity
{
    using System.Speech.Synthesis;
    using Components;
    using Engine;
    using Engine.Components.Events;
    using Engine.Sessions;
    using Guis.Screens;
    using Microsoft.Xna.Framework;
    using Scripting;
    using Sessions;
    using Game = Engine.Game;
    using ISessionData = Sessions.ISessionData;

    public class Unity : Game
    {
        public Unity(GameEngine engine)
            : base(engine)
        {
            this.Interop.Engine.Interop.Save = new SaveManager(engine);

            // Save CPU and GPU resource
            this.Interop.Screen.Settings.IsAlwaysActive  = false;
            this.Interop.Screen.Settings.IsAlwaysVisible = false;
        }

        #region Components

        public static ISessionData SessionData { get; set; }

        public static ISession<SessionData> Session { get; set; }

        public static FsiSession FsiSession { get; set; }

        public static SpeechSynthesizer Speech { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            // Load save to retrieve saved session
            this.Interop.Save.Load();

            // Update time related data before using Session.Cognition.Awake
            Session.Update();

            // Start F# session 
            FsiSession = new FsiSession();

            // Start Speech Synthesizer 
            Speech = new SpeechSynthesizer
            {
                Volume = 100,
                Rate   = 3
            };

            this.InitializeScreen();
        }

        private void InitializeScreen()
        {
            this.Interop.Screen.AddScreen(new BackgroundScreen());
            this.Interop.Event.QueueEvent(
                new Event((int)SessionEventType.GameStarted, null, 3, 1));

            var consciousness = Session.Data.Cognition.Consciousness;
            if (consciousness.IsAwake)
            {
                this.Interop.Screen.AddScreen(new MainScreen());
            }
            else
            {
                this.Interop.Screen.AddScreen(new SummaryScreen());
            }
        }

        #endregion Initialization

        #region Update

        public override void Update(GameTime gameTime)
        {
            Session   .Update();
            FsiSession.Update();

            base.Update(gameTime);
        }

        #endregion

        #region System

        public override void OnExiting()
        {
            this.Interop.Save.Save();

            FsiSession.Dispose();
            Speech    .Dispose();
        }

        #endregion System
    }
}