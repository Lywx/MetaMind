// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Designer.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Components.Content.Texture
{
    public struct ImageDesign
    {
        public ImageDesign(int screenWidth, int screenHeight)
        {
            this.ScreenWidth  = screenWidth;
            this.ScreenHeight = screenHeight;
        }

        public int ScreenWidth { get; private set; }

        public int ScreenHeight { get; private set; }
    }
}