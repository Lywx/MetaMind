// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Perseverance.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance
{
    using System;

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
            GraphicsSettings.Fullscreen = fullscreen;

            if (fullscreen)
            {
                GameEngine.Window.IsBorderless = true;
            }

            // show mouse
            GameEngine.IsMouseVisible = true;

            // 60 FPS
            GameEngine.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / (double)60);
            GameEngine.IsFixedTimeStep   = true;

            // faster update loop but higher cpu usage
            //// GameEngine.IsFixedTimeStep = false;

            // center game window
            var screen = GraphicsSettings.Screen;
            GameEngine.Window.Position = new Point(
                screen.Bounds.X + (screen.Bounds.Width  - GraphicsSettings.Width)  / 2, 
                screen.Bounds.Y + (screen.Bounds.Height - GraphicsSettings.Height) / 2);

            // set width and height
            GameEngine.GraphicsManager.PreferredBackBufferWidth  = GraphicsSettings.Width;
            GameEngine.GraphicsManager.PreferredBackBufferHeight = GraphicsSettings.Height;
            GameEngine.GraphicsManager.ApplyChanges();

            // save
            // -----------------------------------------------------------------
            SaveManager = SaveManager.GetInstance(Game);
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

            GameEngine.ScreenManager.AddScreen(new BackgroundScreen());
            GameEngine.ScreenManager.AddScreen(new MotivationScreen());
        }

        #endregion Initialization

        #region System

        public override void OnExiting()
        {
            SaveManager.Save();
        }

        #endregion System
    }
}