namespace MetaMind.Acutance.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroupSettings
    {
        public readonly int ViewVMargin = 15;

        //---------------------------------------------------------------------
        public readonly TraceViewFactory  TraceViewFactory = new TraceViewFactory();
        
        public readonly TraceViewSettings TraceViewSettings;

        public readonly TraceItemSettings TraceItemSettings;

        public readonly Point TraceStartPoint                 = new Point(0, 108);

        public readonly int   TraceRowNumDisplay              = 5;

        public readonly int   TraceRowNumDisplayFullscreen    = 5;

        public readonly int   TraceRowNumMax                  = 100;

        public readonly int   TraceColumnNumDisplay           = 3;

        public readonly int   TraceColumnNumDisplayFullscreen = 4;

        //---------------------------------------------------------------------
        public readonly EventViewFactory  EventViewFactory = new EventViewFactory();
        
        public readonly EventViewSettings EventViewSettings;

        public readonly EventItemSettings EventItemSettings;

        public readonly Point EventStartPoint;

        public readonly int   EventRowNumDisplay              = 8;

        public readonly int   EventRowNumDisplayFullscreen    = 8;

        public readonly int   EventRowNumMax                  = 100;

        public readonly int   EventColumnNumDisplay           = 2;

        public readonly int   EventColumnNumDisplayFullscreen = 2;

        //---------------------------------------------------------------------
        public readonly KnowledgeViewFactory  KnowledgeViewFactory = new KnowledgeViewFactory();

        public readonly KnowledgeViewSettings KnowledgeViewSettings;

        public readonly KnowledgeItemSettings KnowledgeItemSettings;

        public readonly int KnowledgeViewRowNumDisplayFullscreen = 18;

        public readonly int KnowledgeViewRowNumDisplay           = 10;

        public readonly int KnowledgeViewRowNumMax               = 100;

        public readonly int KnowledgeViewColumnNumDisplay        = 1;

        public MultiplexerGroupSettings()
        {
            this.TraceItemSettings = new TraceItemSettings();

            var traceViewColumnWidth = GraphicsSettings.Width / (GraphicsSettings.Fullscreen
                                              ? this.TraceColumnNumDisplayFullscreen
                                              : this.TraceColumnNumDisplay);
            var traceItemFixedWidth = this.TraceItemSettings.ExperienceFrameSize.X + this.TraceItemSettings.IdFrameSize.X;

            this.TraceItemSettings.NameFrameSize = new Point(traceViewColumnWidth - traceItemFixedWidth, 24);
            this.TraceItemSettings.Reconfigure();

            this.TraceViewSettings = new TraceViewSettings
                                         {
                                             StartPoint = this.TraceStartPoint,
                                             RootMargin = new Point(traceViewColumnWidth, this.TraceItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.Fullscreen
                                                     ? this.TraceColumnNumDisplayFullscreen
                                                     : this.TraceColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.TraceColumnNumDisplayFullscreen
                                                     : this.TraceColumnNumDisplay,

                                             RowNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.TraceRowNumDisplayFullscreen
                                                     : this.TraceRowNumDisplay,
                                             RowNumMax     = this.TraceRowNumMax,
                                         };

            //-----------------------------------------------------------------
            this.EventItemSettings = new EventItemSettings();

            var eventViewColumnWidth = GraphicsSettings.Width / (GraphicsSettings.Fullscreen
                                              ? this.EventColumnNumDisplayFullscreen
                                              : this.EventColumnNumDisplay);
            var eventItemFixedWidth = this.EventItemSettings.ExperienceFrameSize.X + this.EventItemSettings.IdFrameSize.X;

            this.EventItemSettings.NameFrameSize = new Point(eventViewColumnWidth - eventItemFixedWidth, 24);
            this.EventItemSettings.Reconfigure();

            this.EventStartPoint = this.TraceViewSettings.StartPoint + new Point(0, this.TraceViewSettings.RowNumDisplay * this.TraceViewSettings.RootMargin.Y + this.ViewVMargin);
            this.EventViewSettings = new EventViewSettings
                                         {
                                             StartPoint = this.EventStartPoint,
                                             RootMargin = new Point(eventViewColumnWidth, this.EventItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.Fullscreen
                                                     ? this.EventColumnNumDisplayFullscreen
                                                     : this.EventColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.EventColumnNumDisplayFullscreen
                                                     : this.EventColumnNumDisplay,

                                             RowNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.EventRowNumDisplayFullscreen
                                                     : this.EventRowNumDisplay,
                                             RowNumMax     = this.EventRowNumMax,
                                         };

            //-----------------------------------------------------------------
            this.KnowledgeItemSettings = new KnowledgeItemSettings();

            this.KnowledgeViewSettings = new KnowledgeViewSettings
                                             {
                                                 StartPoint = this.EventViewSettings.StartPoint + new Point(0, this.EventViewSettings.RowNumDisplay * this.EventViewSettings.RootMargin.Y + this.ViewVMargin),
                                                 RootMargin = new Point(traceViewColumnWidth, this.TraceItemSettings.NameFrameSize.Y),

                                                 ColumnNumMax     = this.KnowledgeViewColumnNumDisplay,
                                                 ColumnNumDisplay = this.KnowledgeViewColumnNumDisplay,

                                                 RowNumDisplay = GraphicsSettings.Fullscreen
                                                         ? this.KnowledgeViewRowNumDisplayFullscreen
                                                         : this.KnowledgeViewRowNumDisplay,
                                                 RowNumMax     = this.KnowledgeViewRowNumMax,
                                             };
        }

        public List<TraceEntry> Traces
        {
            get { return Acutance.Adventure.Tracelist.Traces; }
        }
    }
}