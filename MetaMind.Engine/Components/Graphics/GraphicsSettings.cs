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

    public class GraphicsSettings : IConfigurationFileLoader, IConfigurationParameter
    {
        public static GraphicsSettings GetInstance()
        {
            return new GraphicsSettings();
        }

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

        #region Constructors

        private GraphicsSettings()
        {
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
            GameEngine.Instance.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / (double)this.FPS);
            GameEngine.Instance.IsFixedTimeStep = true;
        }

        private void ApplyMouseChange()
        {
            GameEngine.Instance.IsMouseVisible = this.IsMouseVisible;
        }

        private void ApplyScreenChange()
        {
            // Resolution
            GameEngine.GraphicsManager.PreferredBackBufferWidth  = this.Width;
            GameEngine.GraphicsManager.PreferredBackBufferHeight = this.Height;
            GameEngine.GraphicsManager.ApplyChanges();

            // Border
            GameEngine.Instance.Window.IsBorderless = this.IsFullscreen;
        }

        #endregion Graphics Operations

        #region Initialization

        public void Initialize()
        {
            this.ConfigurationLoad();
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

        public void ConfigurationLoad()
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