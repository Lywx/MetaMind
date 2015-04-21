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

    public class GraphicsSettings : IConfigurationLoader, IParameter
    {
        #region Graphics Data

        private int height;
        private int width;

        public int Height
        {
            get
            {
                // allow taskbar to show
                return this.IsFullscreen ? this.Screen.Bounds.Height - 1 : this.height;
            }

            set { this.height = value; }
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

        public int FPS { get; set; }

        public Screen Screen
        {
            get
            {
                return Monitor.Screen;
            }
        }

        public bool IsFullscreen { get; set; }

        public bool IsMouseVisible { get; set; }

        #endregion Graphics Data

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
        }

        #endregion

        #region Constructors

        public GraphicsSettings()
        {
            this.LoadConfiguration();
        }

        #endregion
    }
}