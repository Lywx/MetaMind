namespace MetaMind.Acutance.Guis.Modules
{
    using System;

    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;

    using Microsoft.Xna.Framework;

    public class MultiplexerModuleSettings
    {
        //---------------------------------------------------------------------
        public readonly TraceViewFactory  TraceViewFactory = new TraceViewFactory();
        public readonly TraceViewSettings TraceViewSettings;
        public readonly TraceItemSettings TraceItemSettings;

        public readonly TraceViewFactory  KnowledgeViewFactory = new TraceViewFactory();
        public readonly TraceViewSettings KnowledgeViewSettings;
        public readonly TraceItemSettings KnowledgeItemSettings;


        public Color BarFrameAscendColor     = new Color(78, 255, 27, 200);

        public Color BarFrameBackgroundColor = new Color(30, 30, 40, 10);

        public Color BarFrameDescendColor    = new Color(255, 0, 27, 200);

        public Point BarFrameSize            = new Point(500, 8);

        public int   BarFrameXC              = GraphicsSettings.Width / 2;

        public int   BarFrameYC              = 32;

        //---------------------------------------------------------------------
        public Color   SynchronizationColor    = Color.White;

        public Font    SynchronizationNameFont = Font.InfoSimSunFont;

        public float   SynchronizationNameSize = 0.7f;

        public Font    SynchronizationTimeFont = Font.UiRegularFont;

        public float   SynchronizationTimeSize = 0.7f;

        private const int TraceColumnNumDisplay = 3;

        private const int TraceColumnNumDisplayFullscreen = 4;

        public MultiplexerModuleSettings()
        {
            //-----------------------------------------------------------------
            this.TraceItemSettings = new TraceItemSettings
                                         {
                                             NameFrameRegularColor   = ColorPalette.TransparentColor1,
                                             NameFrameMouseOverColor = ColorPalette.TransparentColor2,
                                         };

            var averageWidth = GraphicsSettings.Width / (GraphicsSettings.Fullscreen
                                                             ? TraceColumnNumDisplayFullscreen
                                                             : TraceColumnNumDisplay);
            var fixedWidth = this.TraceItemSettings.ExperienceFrameSize.X + this.TraceItemSettings.IdFrameSize.X;

            this.TraceItemSettings.NameFrameSize = new Point(averageWidth - fixedWidth, 24);
            this.TraceItemSettings.RootFrameSize = this.TraceItemSettings.NameFrameSize;

            //-----------------------------------------------------------------
            this.TraceViewSettings = new TraceViewSettings
                                         {
                                             StartPoint = new Point(0, 75),
                                             RootMargin = new Point(averageWidth, this.TraceItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.Fullscreen
                                                     ? TraceColumnNumDisplayFullscreen
                                                     : TraceColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.Fullscreen
                                                     ? TraceColumnNumDisplayFullscreen
                                                     : TraceColumnNumDisplay,

                                             RowNumDisplay = 10,
                                             RowNumMax     = 100,
                                         };

            //-----------------------------------------------------------------
            this.KnowledgeItemSettings = new TraceItemSettings
                                             {
                                                 NameFrameSize = new Point(GraphicsSettings.Width - fixedWidth, 24)
                                             };
            this.KnowledgeItemSettings.RootFrameSize = this.KnowledgeItemSettings.NameFrameSize;

            //-----------------------------------------------------------------m
            const int ViewMargin = 15;
            this.KnowledgeViewSettings = new TraceViewSettings
                                         {
                                             StartPoint = new Point(0, 75 + this.TraceViewSettings.RowNumDisplay * 24 + ViewMargin),
                                             RootMargin = new Point(averageWidth, this.TraceItemSettings.NameFrameSize.Y),

                                             ColumnNumMax     = 1,
                                             ColumnNumDisplay = 1,

                                             RowNumDisplay = GraphicsSettings.Fullscreen ? 22 : 14,
                                             RowNumMax     = 100,
                                         };
        }
    }
}