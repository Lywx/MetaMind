// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui
{
    public struct Size
    {
        public int Width;

        public int Height;

        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public static Size Zero => new Size(0, 0);
    }
}