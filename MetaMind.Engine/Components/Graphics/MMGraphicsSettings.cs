﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Graphics
{
    using System.Linq;
    using System.Windows.Forms;
    using Extensions;
    using NLog;
    using Services.IO;

    /// <summary>
    /// Graphics settings data. 
    /// </summary>
    public class MMGraphicsSettings : IPlainConfigurationFileLoader, IMMParameter
    {
#if DEBUG
        #region Logger

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion
#endif

        #region Dependency

        public Screen Screen => Screen.AllScreens.First(e => e.Primary);

        public int ScreenWidth => this.Screen.Bounds.Width;

        public int ScreenHeight => this.Screen.Bounds.Height;

        #endregion

        #region Resolution Data 

        private int width;

        private int height;

        public int Height
        {
            get
            {
                return this.IsFullscreen
                           ? this.Screen.Bounds.Height - 1
                           : this.height;
            }

            set { this.height = value; }
        }

        public int Width
        {
            get
            {
                return this.IsFullscreen ? this.Screen.Bounds.Width : this.width;
            }

            set { this.width = value; }
        }

        public float Aspect { get; set; }

        #endregion

        #region Performance Data

        public int FPS { get; set; }

        public bool IsFullscreen { get; set; }

        private bool isWindowMode;

        public bool IsWindowMode
        {
            get { return this.isWindowMode; }
            set { this.isWindowMode = !this.IsFullscreen && value; }
        }

        #endregion

        #region Constructors

        public MMGraphicsSettings()
        {
            this.LoadConfiguration();
            this.CheckConfiguration();
        }

        #endregion

        #region Configuration

        public string ConfigurationFilename => "Graphics.txt";

        public void CheckConfiguration()
        {
            this.CheckResolution();
            this.CheckFPS();
        }

        private void CheckFPS()
        {
            if (this.FPS < 30)
            {
                logger.Error($"Invalid FPS detected: {this.FPS}.");
            }
        }

        public void LoadConfiguration()
        {
            var configuration = PlainConfigurationLoader.LoadUnique(this);

            var dividend = PlainConfigurationReader.ReadValueInts(configuration, "Resolution Aspect", 0, 16); 
            var divisor  = PlainConfigurationReader.ReadValueInts(configuration, "Resolution Aspect", 1, 9);
            this.Aspect  = (float)dividend / divisor;

            this.Width  = PlainConfigurationReader.ReadValueInts(configuration, "Resolution", 0, 800);
            this.Height = PlainConfigurationReader.ReadValueInts(configuration, "Resolution", 1, 600);

            this.IsFullscreen = PlainConfigurationReader.ReadValueBool(configuration, "Full Screen", false);
            this.IsWindowMode = PlainConfigurationReader.ReadValueBool(configuration, "Window Mode", true);

            this.FPS = PlainConfigurationReader.ReadValueInt(configuration, "FPS", 30);
        }

        #endregion

        #region Validation

        private void CheckResolution()
        {
            var aspect = ((double)this.Width / this.Height);
            if (!aspect.NearlyEqual(this.Aspect, 0.1))
            {
                logger.Error($"Invalid screen aspect detected: {aspect}.");
            }
        }

        #endregion
    }
}