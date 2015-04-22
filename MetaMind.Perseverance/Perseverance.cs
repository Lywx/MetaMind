// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Perseverance.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    using Game = MetaMind.Engine.Game;
    using SaveManager = MetaMind.Perseverance.Components.SaveManager;

    public class Perseverance : Game
    {
        public Perseverance(GameEngine engine)
            : base(engine)
        {
            // Domain specific save manager
            this.SaveManager = new SaveManager(engine);

            // Save CPU and GPU resource
            this.Interop.Screen.Settings.IsAlwaysActive  = false;
            this.Interop.Screen.Settings.IsAlwaysVisible = false;
        }

        #region Components

        public static Session Session { get; set; }

        public SaveManager SaveManager { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            // Load save to retrieve saved session
            this.SaveManager.Load();

            // update time related data before using Session.Cognition.Awake
            Session.Update();

            this.Interop.Screen.AddScreen(new BackgroundScreen());
            this.Interop.Event .QueueEvent(new Event((int)SessionEventType.GameStarted, null, 3, 1));

            if (Session.Cognition.Awake)
            {
                this.Interop.Screen.AddScreen(new MotivationScreen());
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
            this.SaveManager.Save();
        }

        #endregion System
    }
}