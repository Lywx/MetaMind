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
    using Engine.Component.Interop.Event;
    using Engine.Console.Commands.Coreutils;
    using Engine.Scripting.FSharp;
    using Engine.Scripting.IronPython;
    using Engine.Session;
    using Guis.Screens;
    using Microsoft.Xna.Framework;
    using Sessions;
    using Game = Engine.Game;
    using ISessionData = Sessions.ISessionData;

    public class Unity : Game
    {
        public Unity(GameEngine engine)
            : base(engine)
        {
            this.Engine.Components.Add(new ResourceMonitor(engine));

            this.Interop.Save = new SaveManager(engine);

            // Save CPU and GPU resource
            this.Interop.Screen.Settings.IsAlwaysActive  = false;
            this.Interop.Screen.Settings.IsAlwaysVisible = false;
        }

        #region Components

        public static ISessionData SessionData { get; set; }

        public static ISession<SessionData> Session { get; set; }

        public static FsiSession FsiSession { get; set; }

        public static IpySession IpySession { get; set; }

        public static SpeechSynthesizer Speech { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            this.InitializeSessionData();
            this.InitializeScripting();
            this.InitializeSynthesizer();
            this.InitializeConsole();

            // Has to be last, adding screen involves loading all kind of things
            this.InitializeScreen();
        }

        private void InitializeConsole()
        {
            this.Interop.Console.AddCommand(new VerboseCommand(FsiSession));
        }

        private void InitializeSessionData()
        {
            // Load save to retrieve saved session
            this.Interop.Save.Load();

            // Update time related data before using Session.Cognition.Awake
            Session.Update();
        }

        private void InitializeSynthesizer()
        {
            // Start Speech Synthesizer 
            Speech = new SpeechSynthesizer
            {
                Volume = 100,
                Rate = 3
            };
        }

        private void InitializeScripting()
        {
            // Start F# session 
            FsiSession = new FsiSession();

            // Start IronPython session 
            IpySession = new IpySession();
        }

        private void InitializeScreen()
        {
            this.Interop.Screen.AddScreen(new BackgroundScreen());
            this.Interop.Event.QueueEvent(
                new Event((int)SessionEvent.GameStarted, null, 3, 1));

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

        protected override void LoadContent()
        {
            base.LoadContent();

            var asset = this.Interop.Asset;
            asset.LoadPackage("Unity.Persistent");
        }

        #region Update

        public override void Update(GameTime time)
        {
            Session.Update();

            this.UpdateScripting();
            base.Update(time);
        }

        private void UpdateScripting()
        {
            FsiSession.Update();
            IpySession.Update();
        }

        #endregion

        #region System

        public override void OnExiting()
        {
            this.Interop.Save.Save();

            this.Dispose();
        }

        #endregion System

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposeScripting();
                this.DisposeSynthesizer();
            }

            base.Dispose(disposing);
        }

        private void DisposeSynthesizer()
        {
            Speech?.Dispose();
        }

        private void DisposeScripting()
        {
            FsiSession?.Dispose();
            IpySession?.Dispose();
        }

        #endregion
    }
}