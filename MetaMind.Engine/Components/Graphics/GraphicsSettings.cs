// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphicsSettings.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Graphics
{
    using System;
    using System.Windows.Forms;

    using MetaMind.Engine.Settings.Loaders;
    using MetaMind.Engine.Settings.Systems;

    using Microsoft.Xna.Framework;

    public class GraphicsSettings : GameComponent, IConfigurationFileLoader, IConfigurationParameter
    {
        #region Builder

        public static GraphicsSettings GetInstance(GameEngine gameEngine)
        {
            return new GraphicsSettings(gameEngine);
        }


        #endregion

        #region Graphics Data

        private int height;

        private int width;

        public int FPS { get; set; }

        public int Height
        {
            get
            {
                // allow taskbar to show
                return this.IsFullscreen ? Screen.Bounds.Height - 1 : this.height;
            }

            set { this.height = value; }
        }

        public bool IsFullscreen { get; set; }

        public bool IsMouseVisible { get; set; }

        public Screen Screen
        {
            get
            {
                return Monitor.Screen;
            }
        }

        public int Width
        {
            get
            {
                // allow taskbar to show
                return this.IsFullscreen ? this.Screen.Bounds.Width : this.width;
            }

            set { this.width = value; }
        }

        #endregion Graphics Data

        #region Engine Data

        private IGameGraphics GameGraphics { get; set; }

        #endregion

        #region Constructors

        private GraphicsSettings(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.GameGraphics = new GameEngineGraphics(gameEngine);
        }

        #endregion

        #region Graphics Operations

        public void ApplyChanges()
        {
            this.ApplyFrameChange();
            this.ApplyScreenChange();
            this.ApplyMouseChange();
        }

        private void ApplyFrameChange()
        {
            this.Game.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / (double)this.FPS);
            this.Game.IsFixedTimeStep = true;
        }

        private void ApplyMouseChange()
        {
            this.Game.IsMouseVisible = this.IsMouseVisible;
        }

        private void ApplyScreenChange()
        {
            // Resolution
            this.GameGraphics.Graphics.PreferredBackBufferWidth  = this.Width;
            this.GameGraphics.Graphics.PreferredBackBufferHeight = this.Height;
            this.GameGraphics.Graphics.ApplyChanges();

            // Border
            this.Game.Window.IsBorderless = this.IsFullscreen;
        }

        #endregion Graphics Operations

        #region Initialization

        public void Initialize()
        {
            this.LoadConfiguration();
        }

        #endregion

        #region Configuration

        public string ConfigurationFile
        {
            get
            {
                return "Graphics.txt";
            }
        }

        public void LoadConfiguration()
        {
            var configuration = ConfigurationFileLoader.LoadUniquePairs(this);

            this.FPS    = ConfigurationFileLoader.ExtractMultipleInt(configuration, "FPS", 0, 30);
            this.Width  = ConfigurationFileLoader.ExtractMultipleInt(configuration, "Resolution", 0, 800);
            this.Height = ConfigurationFileLoader.ExtractMultipleInt(configuration, "Resolution", 1, 600);

            this.IsMouseVisible = ConfigurationFileLoader.ExtractBool(configuration, "IsMouseVisible", true);
            this.IsFullscreen   = ConfigurationFileLoader.ExtractBool(configuration, "IsFullscreen", false);

            this.ApplyChanges();
        }

        #endregion
    }
}