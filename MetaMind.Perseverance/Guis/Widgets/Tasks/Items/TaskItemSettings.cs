using MetaMind.Engine.Components;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    public class TaskItemSettings : ItemSettings
    {
        //---------------------------------------------------------------------
        public Color ErrorColor                    = Color.White;

        //---------------------------------------------------------------------
        public float NameSize                      = 0.7f;
        public Color NameColor                     = Color.White;
        public Font  NameFont                      = Font.InfoSimSunFont;
        public Point NameFrameSize                 = new Point( 256, 24 );
        public Point NameFrameMargin               = new Point( 2, 2 );

        //---------------------------------------------------------------------
        public int   NameXLMargin                  = 10;
        public int   NameXRMargin                  = 30;
        public int   NameYTMargin                  = 5;

        //---------------------------------------------------------------------
        public Color NameFrameRegularColor         = new Color( 0, 0, 0, 0 );
        public Color NameFrameMouseOverColor       = new Color( 23, 41, 61, 2 ); 
        public Color NameFrameModificationColor    = new Color( 200, 200, 0, 2 );
        public Color NameFrameSelectionColor       = new Color( 0, 20, 250, 2 );
        public Color NameFrameHighlightColor       = new Color( 0, 139, 0, 2 );
        public Color NameFrameSynchronizationColor = new Color( 128, 128, 128, 2 );

        //---------------------------------------------------------------------
        public float ExperienceSize                = 0.7f;
        public Color ExperienceColor               = Color.White;
        public Font  ExperienceFont                = Font.UiStatisticsFont;
        public Point ExperienceFrameSize           = new Point( 96, 20 );
        public Point ExperienceFrameMargin         = new Point( 2, 2 );
        public Color ExperienceFrameColor          = new Color( 16, 32, 32, 2 );

        //---------------------------------------------------------------------
        public Point ProgressFrameSize             = new Point( 96, 20 );
        public Color ProgressFrameColor            = new Color( 16, 32, 32, 2 );
        public Point ProgressFrameMargin           = new Point( 2, 2 );
        
        //---------------------------------------------------------------------
        public TaskItemSettings()
        {
            //-----------------------------------------------------------------
            RootFrameSize = NameFrameSize;
            //-----------------------------------------------------------------
            NameXLMargin = ( int ) ( NameXLMargin * NameSize );
            NameXRMargin = ( int ) ( NameXRMargin * NameSize );
            NameYTMargin = ( int ) ( NameYTMargin * NameSize );
            //-----------------------------------------------------------------
            IdSize        = 0.7f;
            IdFrameSize   = new Point( 48, 20 );
            //-----------------------------------------------------------------
        }
    }
}