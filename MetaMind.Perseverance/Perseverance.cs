// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Perseverance.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    using Game = MetaMind.Engine.Game;
    using SaveManager = MetaMind.Perseverance.Components.SaveManager;

    public class Perseverance : Game
    {
        public Perseverance(GameEngine gameEngine)
            : base(gameEngine)
        {
            // components
            // -----------------------------------------------------------------
            // domain specific save manager
            SaveManager = SaveManager.GetInstance(Game);

            // save resource mode
            GameEngine.ScreenManager.Settings.IsAlwaysActive = false;
            GameEngine.ScreenManager.Settings.IsAlwaysVisible = false;
        }

        #region Components

        public static Session Session { get; set; }

        private static SaveManager SaveManager { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            // load save to retrieve saved session
            SaveManager.Load();

            // update time related data before using Session.Cognition.Awake
            Session.Update();

            GameEngine.ScreenManager.AddScreen(new BackgroundScreen());
            GameEngine.EventManager .QueueEvent(new EventBase((int)SessionEventType.GameStarted, null, 3, 1));

            if (Session.Cognition.Awake)
            {
                GameEngine.ScreenManager.AddScreen(new MotivationScreen());
            }
            else
            {
                GameEngine.ScreenManager.AddScreen(new SummaryScreen());
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
            SaveManager.Save();
        }

        #endregion System
    }
}