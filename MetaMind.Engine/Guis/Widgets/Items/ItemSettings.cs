using System;
using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Items
{
    public class ItemSettings : ICloneable
    {
        //------------------------------------------------------------------
        public Color ErrorColor       = Color.White;
        public Color TransparentColor1 = new Color( 20, 20, 20, 0 );
        public Color TransparentColor2 = new Color( 60, 60, 60, 0 );
        public Color TransparentColor3 = new Color( 80, 80, 80, 0 );
        public Color TransparentColor4 = new Color( 120, 120, 120, 0 );
        public Color TransparentColor5 = new Color( 160, 160, 160, 0 );

        //------------------------------------------------------------------
        public int   NameXLMargin                  = 10;
        public int   NameXRMargin                  = 30;
        public int   NameYTMargin                  = 5;
        public Font  NameFont                      = Font.InfoSimSunFont;
        public Color NameColor                     = Color.White;
        public float NameSize                      = 1f;
        public Point NameFrameSize                 = new Point( 256, 34 );
        public Color NameFrameRegularColor         = new Color( 0, 0, 0, 0 );
        public Color NameFrameMouseOverColor       = new Color( 23, 41, 61, 2 );  // sea blue
        public Color NameFrameModificationColor    = new Color( 200, 200, 0, 2 ); // bright yellow
        public Color NameFrameSelectionColor       = new Color( 0, 20, 250, 2 );
        public Color NameFrameHighlightColor       = new Color( 0, 139, 0, 2 );
        public Color NameFrameSynchronizationColor = new Color( 128, 128, 128, 2 );

        //------------------------------------------------------------------
        public float IdSize         = 0.7f;
        public Color IdColor        = Color.White;
        public Font  IdFont         = Font.UiStatisticsFont;
        public Point IdFrameSize    = new Point( 48, 24 );
        public Point IdFrameMargin  = new Point( 2, 2 );
        public Color IdFrameColor   = new Color( 139, 0, 0, 2 );

        //------------------------------------------------------------------

        public float ExperienceSize         = 0.7f;
        public Color ExperienceColor        = Color.White;
        public Point ExperienceFrameSize    = new Point( 96, 24 );
        public Point ExperienceFrameMargin  = new Point( 2, 2 );
        public Color ExperienceFrameColor   = new Color( 16, 32, 32, 2 );

        //------------------------------------------------------------------
        public Point RootFrameSize    = new Point( 96, 96 );
        public Point RootFrameMargin = new Point( 2, 2 );
        public Color RootFrameColor   = new Color( 16, 32, 32, 2 );

        //------------------------------------------------------------------
        public static ItemSettings Default
        {
            get { return new ItemSettings(); }
        }


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}