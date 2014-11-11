namespace MetaMind.Engine.Guis.Elements.Views
{
    using System;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class ViewSettings2D : ICloneable
    {
        //---------------------------------------------------------------------
        public Font  NameFont         = Font.UiStatisticsFont;
        public Color NameColor        = Color.White;
        public float NameSize         = 1.5f;
        public Point NameMargin       = new Point( 47, 47 );
        //---------------------------------------------------------------------
        public int   ColumnNumDisplay = 3;
        public int   ColumnNumMax     = 5;
        public int   RowNumDisplay    = 3;
        public int   RowNumMax        = 500;
        //---------------------------------------------------------------------
        public Point StartPoint       = new Point( 160, GraphicsSettings.Height / 2 );
        public Point RootMargin       = new Point( 251, 150 );
        //---------------------------------------------------------------------

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}