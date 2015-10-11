namespace MetaMind.Engine.Entities.Controls.Views.Regions
{
    using System;
    using Engine.Settings;
    using Microsoft.Xna.Framework;

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

        public Color RegionColor = MMPalette.Transparent20;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}