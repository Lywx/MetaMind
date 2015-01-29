// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphicsSettings.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Graphics
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    public static class GraphicsSettings
    {
        public static bool IsFullscreen;

        public static Screen Screen = Screen.AllScreens.First(e => e.Primary);

        private static int height;

        private static int width;
        
        private static int fps;

        public static int Height
        {
            // allow taskbar to show
            get
            {
                return IsFullscreen ? Screen.Bounds.Height - 1 : height;
            }
        }

        public static int Width
        {
            get
            {
                return IsFullscreen ? Screen.Bounds.Width : width;
            }
        }

        public static void SetScreenProperties(int width, int height, bool isFullscreen, int fps)
        {
            GraphicsSettings.width  = width;
            GraphicsSettings.height = height;

            IsFullscreen = isFullscreen;
            Fps          = fps;

            if (IsFullscreen)
            {
                GameEngine.Instance.Window.IsBorderless = true;
            }
        }

        public static int Fps
        {
            get { return fps; }
            set
            {
                fps = value;

                GameEngine.Instance.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / (double)fps);
                GameEngine.Instance.IsFixedTimeStep = true;
            }
        }

        public static bool IsMouseVisible
        {
            get { return GameEngine.Instance.IsMouseVisible; }
            set { GameEngine.Instance.IsMouseVisible = value; }
        }

        public static void SetMouseProperties(bool isMouseVisible)
        {
            IsMouseVisible = isMouseVisible;
        }
    }
}
