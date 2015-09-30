// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewScrollbarSettings.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views.Scrolls
{
    using Microsoft.Xna.Framework;
    using Setting.Color;

    public class ViewScrollbarSettings 
    {
        public byte ColorBrightnessMax = 255;

        public byte ColorBrightnessMin = 0;

        public int ColorBrightnessFadeSpeed = 255 * 1;

        public Color RegularColor = Palette.Transparent200;

        public Color DraggingColor = Color.White;

        public int Width = 5;
    }
}