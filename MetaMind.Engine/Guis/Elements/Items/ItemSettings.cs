namespace MetaMind.Engine.Guis.Elements.Items
{
    using System;

    using MetaMind.Engine.Components;

    using Microsoft.Xna.Framework;

    public class ItemSettings : ICloneable
    {
        //---------------------------------------------------------------------
        public Point RootFrameSize                 = new Point( 24, 24 );
        public Point RootFrameMargin               = new Point( 2, 2 );
        public Color RootFrameColor                = new Color( 16, 32, 32, 2 );
        //---------------------------------------------------------------------
        public float IdSize                        = 0.7f;
        public Color IdColor                       = Color.White;
        public Font  IdFont                        = Font.UiStatisticsFont;
        public Point IdFrameSize                   = new Point( 48, 24 );
        public Point IdFrameMargin                 = new Point( 2, 2 );
        public Color IdFrameColor                  = new Color( 139, 0, 0, 2 );
        //---------------------------------------------------------------------

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}