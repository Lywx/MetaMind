// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewScrollbarSettings.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using Microsoft.Xna.Framework;

    public class ViewScrollbarSettings 
    {
        public byte BrightnessMax = 200;

        public byte BrightnessDecreasingStep = 10;

        public Color Color = Color.White;

        public int Width = 5;

        public ViewScrollbarSettings()
        {
            
        }
    }
}