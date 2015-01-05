// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphicsSettings.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Settings
{
    using System.Linq;
    using System.Windows.Forms;

    public static class GraphicsSettings
    {
        public static Screen Screen = Screen.AllScreens.First(e => e.Primary);

        public static bool Fullscreen;

        public static int Width
        {
            get
            {
                return Fullscreen ? Screen.Bounds.Width : 1280;
            }
        }

        public static int Height
        {
            // allow taskbar to show
            get
            {
                return Fullscreen ? Screen.Bounds.Height - 1 : 720;
            }
        }
    }
}