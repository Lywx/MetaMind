namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class ItemSettings : ICloneable
    {
        //---------------------------------------------------------------------
        public Point RootFrameSize   = new Point(24, 24);

        public Point RootFrameMargin = new Point(2, 2);

        public Color RootFrameColor  = Color.Transparent;

        //---------------------------------------------------------------------
        public float IdSize        = 0.7f;

        public Color IdColor       = Color.White;

        public Font  IdFont        = Font.UiStatistics;

        public Point IdFrameSize   = new Point(48, 24);

        public Point IdFrameMargin = new Point(2, 2);

        public Color IdFrameColor  = Palette.TransparentColor1;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}