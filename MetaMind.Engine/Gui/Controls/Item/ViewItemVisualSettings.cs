// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameSettings.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ViewItemVisualSettings : ICloneable
    {
        #region Geometry

        public ViewItemVisualState<Margin> Margin;

        public ViewItemVisualState<Point> Size;

        #endregion

        public ViewItemVisualState<Color> Color;

        public ViewItemVisualState<Texture2D> Image;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}