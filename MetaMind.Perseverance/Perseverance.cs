// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Perseverance.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance
{
    using System.Runtime.InteropServices.ComTypes;

    using MetaMind.Engine;
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Components;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    using Microsoft.Xna.Framework;

    public class Perseverance : EngineRunner
    {
        public Perseverance(GameEngine engine, bool fullscreen)
            : base(engine)
        {
            GameEngine.Fps = 40;

            GraphicsSettings.Fullscreen = fullscreen;

            if (fullscreen)
            {
                GameEngine.Window.IsBorderless = true;
            }

            GameEngine.IsMouseVisible = true;

            GameEngine.TriggerCenter();

            // components
            // -----------------------------------------------------------------
            // domain specific save manager
            SaveManager = SaveManager.GetInstance(Game);

            // save resource mode
            GameEngine.ScreenManager.Settings.AlwaysUpdate = false;
            GameEngine.ScreenManager.Settings.AlwaysDraw = false;
        }

        #region Components

        public static Adventure Adventure { get; private set; }

        private static SaveManager SaveManager { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            Adventure = Adventure.LoadSave();

            // update adventure time related data
            Adventure.Update();

            GameEngine.ScreenManager.AddScreen(new BackgroundScreen());

            if (Adventure.Cognition.Awake)
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
            Adventure.Update();

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