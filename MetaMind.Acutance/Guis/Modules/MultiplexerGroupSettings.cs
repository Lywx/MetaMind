namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroupSettings
    {
        //---------------------------------------------------------------------
        public readonly TraceViewFactory  TraceViewFactory = new TraceViewFactory();
        
        public readonly TraceViewSettings TraceViewSettings;

        public readonly TraceItemSettings TraceItemSettings;

        public readonly TraceViewFactory  KnowledgeViewFactory = new TraceViewFactory();

        public readonly TraceViewSettings KnowledgeViewSettings;

        public readonly TraceItemSettings KnowledgeItemSettings;

        public readonly Point TraceStartPoint =  new Point(0, 108);

        public readonly int TraceColumnNumDisplay           = 3;

        public readonly int TraceColumnNumDisplayFullscreen = 4;

        public MultiplexerGroupSettings()
        {
            this.TraceItemSettings = new TraceItemSettings
                                         {
                                             NameFrameRegularColor = ColorPalette.TransparentColor1,
                                             NameFrameMouseOverColor = ColorPalette.TransparentColor2,
                                         };

            var averageWidth = GraphicsSettings.Width / (GraphicsSettings.Fullscreen
                                      ? this.TraceColumnNumDisplayFullscreen
                                      : this.TraceColumnNumDisplay);
            var fixedWidth = this.TraceItemSettings.ExperienceFrameSize.X + this.TraceItemSettings.IdFrameSize.X;

            this.TraceItemSettings.NameFrameSize = new Point(averageWidth - fixedWidth, 24);
            this.TraceItemSettings.RootFrameSize = this.TraceItemSettings.NameFrameSize;

            //-----------------------------------------------------------------
            this.TraceViewSettings = new TraceViewSettings
                                         {
                                             StartPoint = this.TraceStartPoint,
                                             RootMargin = new Point(averageWidth, this.TraceItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.Fullscreen
                                                     ? this.TraceColumnNumDisplayFullscreen
                                                     : this.TraceColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.TraceColumnNumDisplayFullscreen
                                                     : this.TraceColumnNumDisplay,

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
                                                 StartPoint = this.TraceStartPoint + new Point(0, this.TraceViewSettings.RowNumDisplay * 24 + ViewMargin),
                                                 RootMargin = new Point(averageWidth, this.TraceItemSettings.NameFrameSize.Y),

                                                 ColumnNumMax     = 1,
                                                 ColumnNumDisplay = 1,

                                                 RowNumDisplay = GraphicsSettings.Fullscreen ? 22 : 14,
                                                 RowNumMax     = 100,
                                             };
        }
    }
}