// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Designer.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Components.Content
{
    public class Designer
    {
        public Designer(int screenWidth, int screenHeight)
        {
            this.ScreenWidth  = screenWidth;
            this.ScreenHeight = screenHeight;
        }

        public int ScreenWidth { get; private set; }

        public int ScreenHeight { get; private set; }
    }
}