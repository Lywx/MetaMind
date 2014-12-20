namespace MetaMind.Acutance.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroupSettings
    {
        public readonly int ViewVMargin = 28;

        //---------------------------------------------------------------------
        public readonly TraceViewFactory  TraceViewFactory = new TraceViewFactory();
        
        public readonly TraceViewSettings TraceViewSettings;

        public readonly TraceItemSettings TraceItemSettings;

        public readonly Point TraceStartPoint                 = new Point(10, 108);

        public readonly int   TraceRowNumDisplay              = 5;

        public readonly int   TraceRowNumDisplayFullscreen    = 5;

        public readonly int   TraceRowNumMax                  = 100;

        public readonly int   TraceColumnNumDisplay           = 3;

        public readonly int   TraceColumnNumDisplayFullscreen = 4;

        //---------------------------------------------------------------------
        public readonly CallViewFactory  CallViewFactory = new CallViewFactory();
        
        public readonly CallViewSettings CallViewSettings;

        public readonly CallItemSettings CallItemSettings;

        public readonly Point CallStartPoint;

        public readonly int   CallRowNumDisplay              = 8;

        public readonly int   CallRowNumDisplayFullscreen    = 8;

        public readonly int   CallRowNumMax                  = 100;

        public readonly int   CallColumnNumDisplay           = 3;

        public readonly int   CallColumnNumDisplayFullscreen = 3;

        //---------------------------------------------------------------------
        public readonly KnowledgeViewFactory  KnowledgeViewFactory = new KnowledgeViewFactory();

        public readonly KnowledgeViewSettings KnowledgeViewSettings;

        public readonly KnowledgeItemSettings KnowledgeItemSettings;

        public readonly Point KnowledgeStartPoint;

        public readonly int KnowledgeViewRowNumDisplayFullscreen = 17;

        public readonly int KnowledgeViewRowNumDisplay           = 9;

        public readonly int KnowledgeViewRowNumMax               = 100;

        public readonly int KnowledgeViewColumnNumDisplay        = 1;

        public MultiplexerGroupSettings()
        {
            this.TraceItemSettings = new TraceItemSettings();

            var traceViewColumnWidth = this.ViewWidth / (GraphicsSettings.Fullscreen 
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
            this.CallItemSettings = new CallItemSettings();

            var callViewColumnWidth = this.ViewWidth / (GraphicsSettings.Fullscreen 
                                              ? this.CallColumnNumDisplayFullscreen
                                              : this.CallColumnNumDisplay);
            var callItemFixedWidth = this.CallItemSettings.ExperienceFrameSize.X + this.CallItemSettings.IdFrameSize.X;

            this.CallItemSettings.NameFrameSize = new Point(callViewColumnWidth - callItemFixedWidth, 24);
            this.CallItemSettings.Reconfigure();

            this.CallStartPoint = this.TraceViewSettings.StartPoint + new Point(0, this.TraceViewSettings.RowNumDisplay * this.TraceViewSettings.RootMargin.Y + this.ViewVMargin);
            this.CallViewSettings = new CallViewSettings(this.Calllist)
                                         {
                                             StartPoint = this.CallStartPoint,
                                             RootMargin = new Point(callViewColumnWidth, this.CallItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.Fullscreen
                                                     ? this.CallColumnNumDisplayFullscreen
                                                     : this.CallColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.CallColumnNumDisplayFullscreen
                                                     : this.CallColumnNumDisplay,

                                             RowNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.CallRowNumDisplayFullscreen
                                                     : this.CallRowNumDisplay,
                                             RowNumMax     = this.CallRowNumMax,
                                         };

            //-----------------------------------------------------------------
            this.KnowledgeItemSettings = new KnowledgeItemSettings();

            var knowledgeViewColumnWidth = this.ViewWidth / this.KnowledgeViewColumnNumDisplay;
            var knowledgeItemFixedWidth = this.KnowledgeItemSettings.IdFrameSize.X;

            this.KnowledgeItemSettings.NameFrameSize = new Point(knowledgeViewColumnWidth - knowledgeItemFixedWidth, 24);
            this.KnowledgeItemSettings.Reconfigure();

            this.KnowledgeStartPoint = this.CallViewSettings.StartPoint + new Point(
                                             0,
                                             this.CallViewSettings.RowNumDisplay * this.CallViewSettings.RootMargin.Y + this.ViewVMargin);

            this.KnowledgeViewSettings = new KnowledgeViewSettings
                                             {
                                                 StartPoint = this.KnowledgeStartPoint,
                                                 RootMargin = new Point(knowledgeViewColumnWidth, this.TraceItemSettings.NameFrameSize.Y),

                                                 ColumnNumMax     = this.KnowledgeViewColumnNumDisplay,
                                                 ColumnNumDisplay = this.KnowledgeViewColumnNumDisplay,

                                                 RowNumDisplay = GraphicsSettings.Fullscreen
                                                         ? this.KnowledgeViewRowNumDisplayFullscreen
                                                         : this.KnowledgeViewRowNumDisplay,
                                                 RowNumMax     = this.KnowledgeViewRowNumMax,
                                             };
        }

        private int ViewWidth
        {
            get { return GraphicsSettings.Width - this.TraceStartPoint.X * 2; }
        }

        public Calllist Calllist  
        {
            get { return Acutance.Adventure.Calllist; }
        }

        public List<CallEntry> Calls
        {
            get { return Acutance.Adventure.Calllist.Calls; }
        }

        public List<TraceEntry> Traces
        {
            get { return Acutance.Adventure.Tracelist.Traces; }
        }
    }
}