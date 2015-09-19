namespace MetaMind.Engine.Gui.Control.Views.Regions
{
    using System;
    using Microsoft.Xna.Framework;
    using Setting.Color;

    public class ViewRegionSettings : ICloneable
    {
        /// <summary>
        /// Rectangle cropping margin.
        /// </summary>
        public Point RegionMargin = new Point(4, 4);

        /// <summary>
        /// Rectanlge drawing thick.
        /// </summary>
        public float RegionThick = 2f;

        public Color RegionColor = Palette.Transparent20;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}