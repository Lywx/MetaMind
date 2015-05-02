// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameSettings.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using System;

    using Microsoft.Xna.Framework;

    public class FrameSettings : ICloneable
    {
        #region Geometry

        public Point Margin;

        public Point Size;

        #endregion

        #region Colors

        public Color ModificationColor;

        public Color MouseOverColor;

        public Color PendingColor;

        public Color RegularColor;

        public Color SelectionColor;

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}