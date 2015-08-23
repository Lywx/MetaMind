// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Graphics
{
    using System.Windows.Forms;
    using Engine.Settings.Loaders;
    using Engine.Settings.Systems;

    public class GraphicsSettings : IConfigurationLoader, IParameter
    {
        #region Graphics Data

        private int height;

        private int width;

        public float Aspect => (float)this.Width / this.Height;

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

        public Screen Screen => Monitor.Screen;

        public bool IsFullscreen { get; set; }

        public bool IsMouseVisible { get; set; }

        #endregion Graphics Data

        #region Configuration

        public string ConfigurationFile => "Graphics.txt";

        public void LoadConfiguration()
        {
            var configuration = ConfigurationLoader.LoadUniquePairs(this);

            this.FPS    = FileLoader.ExtractInts(configuration, "FPS", 0, 30);
            this.Width  = FileLoader.ExtractInts(configuration, "Resolution", 0, 800);
            this.Height = FileLoader.ExtractInts(configuration, "Resolution", 1, 600);

            this.IsMouseVisible = FileLoader.ExtractBool(configuration, "IsMouseVisible", true);
            this.IsFullscreen   = FileLoader.ExtractBool(configuration, "IsFullscreen", false);
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