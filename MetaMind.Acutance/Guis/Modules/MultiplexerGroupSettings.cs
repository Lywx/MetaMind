namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroupSettings
    {
        //---------------------------------------------------------------------
        public readonly TraceViewFactory  PTraceViewFactory = new TraceViewFactory();
        
        public readonly TraceViewSettings PTraceViewSettings;

        public readonly TraceItemSettings PTraceItemSettings;

        public readonly TraceViewFactory  NTraceViewFactory = new TraceViewFactory();
        
        public readonly TraceViewSettings NTraceViewSettings;

        public readonly TraceItemSettings NTraceItemSettings;

        public readonly KnowledgeViewFactory  KnowledgeViewFactory = new KnowledgeViewFactory();

        public readonly KnowledgeViewSettings KnowledgeViewSettings;

        public readonly KnowledgeItemSettings KnowledgeItemSettings;

        public readonly Point TraceStartPoint                 = new Point(0, 108);

        public readonly int   TraceColumnNumDisplay           = 3;

        public readonly int   TraceColumnNumDisplayFullscreen = 4;

        public MultiplexerGroupSettings()
        {
            this.PTraceItemSettings = new TraceItemSettings
                                         {
                                             NameFrameRegularColor   = ColorPalette.TransparentColor1,
                                             NameFrameMouseOverColor = ColorPalette.TransparentColor2,
                                         };

            this.NTraceItemSettings = new TraceItemSettings
                                         {
                                             NameFrameRegularColor   = ColorPalette.TransparentColor1,
                                             NameFrameMouseOverColor = ColorPalette.TransparentColor2,
                                             NameFrameStoppedColor   = new Color(0, 120, 20, 20),
                                         };

            var averageWidth = GraphicsSettings.Width / (GraphicsSettings.Fullscreen
                                      ? this.TraceColumnNumDisplayFullscreen
                                      : this.TraceColumnNumDisplay);
            var fixedWidth = this.PTraceItemSettings.ExperienceFrameSize.X + this.PTraceItemSettings.IdFrameSize.X;

            this.PTraceItemSettings.NameFrameSize = new Point(averageWidth - fixedWidth, 24);
            this.PTraceItemSettings.RootFrameSize = this.PTraceItemSettings.NameFrameSize;

            this.NTraceItemSettings.NameFrameSize = new Point(averageWidth - fixedWidth, 24);
            this.NTraceItemSettings.RootFrameSize = this.NTraceItemSettings.NameFrameSize;

            //-----------------------------------------------------------------
            this.PTraceViewSettings = new TraceViewSettings
                                         {
                                             Positive = true,

                                             StartPoint = this.TraceStartPoint,
                                             RootMargin = new Point(averageWidth, this.PTraceItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.Fullscreen
                                                     ? this.TraceColumnNumDisplayFullscreen
                                                     : this.TraceColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.TraceColumnNumDisplayFullscreen
                                                     : this.TraceColumnNumDisplay,

                                             RowNumDisplay = 10,
                                             RowNumMax     = 100,
                                         };
            const int ViewMargin = 15;

            this.NTraceViewSettings = new TraceViewSettings
                                         {
                                             Positive = false,

                                             StartPoint = this.PTraceViewSettings.StartPoint + new Point(0, this.PTraceViewSettings.RowNumDisplay * this.PTraceViewSettings.RootMargin.Y + ViewMargin),
                                             RootMargin = new Point(averageWidth, this.NTraceItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.Fullscreen
                                                     ? this.TraceColumnNumDisplayFullscreen
                                                     : this.TraceColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.TraceColumnNumDisplayFullscreen
                                                     : this.TraceColumnNumDisplay,

                                             RowNumDisplay = 2,
                                             RowNumMax     = 100,
                                         };

            //-----------------------------------------------------------------
            this.KnowledgeItemSettings = new KnowledgeItemSettings();

            //-----------------------------------------------------------------m
            this.KnowledgeViewSettings = new KnowledgeViewSettings
                                             {
                                                 StartPoint = this.NTraceViewSettings.StartPoint + new Point(0, this.NTraceViewSettings.RowNumDisplay * this.NTraceViewSettings.RootMargin.Y + ViewMargin),
                                                 RootMargin = new Point(averageWidth, this.PTraceItemSettings.NameFrameSize.Y),

                                                 ColumnNumMax     = 1,
                                                 ColumnNumDisplay = 1,

                                                 RowNumDisplay = GraphicsSettings.Fullscreen ? 19 : 11,
                                                 RowNumMax     = 100,
                                             };
        }
    }
}