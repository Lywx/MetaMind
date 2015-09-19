// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Component.Graphics
{
    using System.Linq;
    using System.Windows.Forms;
    using Setting.Loader;

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
                // Allow taskbar to show
                return this.IsFullscreen ? this.Screen.Bounds.Height - 1 : this.height;
            }

            set { this.height = value; }
        }

        public int Width
        {
            get
            {
                // Allow taskbar to show
                return this.IsFullscreen ? this.Screen.Bounds.Width : this.width;
            }

            set { this.width = value; }
        }

        public int FPS { get; set; }

        public Screen Screen => Screen.AllScreens.First(e => e.Primary);

        public bool IsFullscreen { get; set; }

        public bool IsMouseVisible { get; set; }

        #endregion Graphics Data

        #region Configuration

        public string ConfigurationFile => "Graphics.txt";

        public void LoadConfiguration()
        {
            var configuration = ConfigurationLoader.LoadUniquePairs(this);

            this.FPS    = FileLoader.ReadInts(configuration, "FPS", 0, 30);
            this.Width  = FileLoader.ReadInts(configuration, "Resolution", 0, 800);
            this.Height = FileLoader.ReadInts(configuration, "Resolution", 1, 600);

            // TODO: I need to redraw mouse cursor differently in games
            // TODO: So this MouseVisible settings are obsolete 
            this.IsMouseVisible = FileLoader.ReadBool(configuration, "IsMouseVisible", true);
            this.IsFullscreen   = FileLoader.ReadBool(configuration, "IsFullscreen", false);
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