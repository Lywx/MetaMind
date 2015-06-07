// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Testimony.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Testimony
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Sessions;
    using MetaMind.Testimony.Screens;
    using MetaMind.Testimony.Sessions;

    using Microsoft.Xna.Framework;

    public class Testimony : Engine.Game
    {
        public Testimony(GameEngine engine)
            : base(engine)
        {
            this.Interop.Engine.Interop.Save = new Components.SaveManager(engine);

            // Save CPU and GPU resource
            this.Interop.Screen.Settings.IsAlwaysActive  = false;
            this.Interop.Screen.Settings.IsAlwaysVisible = false;
        }

        #region Components

        internal static Sessions.ISessionData SessionData { get; set; }

        internal static ISession<SessionData> Session { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            // Load save to retrieve saved session
            this.Interop.Save.Load();

            // Update time related data before using Session.Cognition.Awake
            Session.Update();

            this.Interop.Screen.AddScreen(new BackgroundScreen());
            this.Interop.Event .QueueEvent(new Event((int)SessionEventType.GameStarted, null, 3, 1));

            var consciousness = Session.Data.Cognition.Consciousness;
            if (consciousness.IsAwake)
            {
                this.Interop.Screen.AddScreen(new TestimonyScreen());
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
            Session.Update();

            base.Update(gameTime);
        }

        #endregion

        #region System

        public override void OnExiting()
        {
            this.Interop.Save.Save();
        }

        #endregion System
    }
}