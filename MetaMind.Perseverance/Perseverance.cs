// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Perseverance.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance
{
    using MetaMind.Engine;
    using MetaMind.Perseverance.Components;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    using Game = MetaMind.Engine.Game;

    public class Perseverance : Game
    {
        public Perseverance(GameEngine engine)
            : base(engine)
        {
            // components
            // -----------------------------------------------------------------
            // domain specific save manager
            SaveManager = SaveManager.GetInstance(Game);

            // save resource mode
            GameEngine.ScreenManager.Settings.AlwaysUpdate = false;
            GameEngine.ScreenManager.Settings.AlwaysDraw = false;
        }

        #region Components

        public static Session Session { get; set; }

        private static SaveManager SaveManager { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            SaveManager.Load();

            GameEngine.ScreenManager.AddScreen(new BackgroundScreen());

            // update adventure time related data
            Session.Update();

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