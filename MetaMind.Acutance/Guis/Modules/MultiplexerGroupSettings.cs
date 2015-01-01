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

        public readonly int   TraceRowNumDisplay              = -1;

        public readonly int   TraceRowNumDisplayFullscreen    = -1;

        public readonly int   TraceRowNumMax                  = 100;

        public readonly int   TraceColumnNumDisplay           = 3;

        public readonly int   TraceColumnNumDisplayFullscreen = 4;

        //---------------------------------------------------------------------
        public readonly CommandViewFactory  CommandViewFactory = new CommandViewFactory();
        
        public readonly CommandViewSettings CommandViewSettings;

        public readonly CommandItemSettings CommandItemSettings;

        public readonly Point CommandStartPoint;

        public readonly int   CommandRowNumDisplay              = 13;

        public readonly int   CommandRowNumDisplayFullscreen    = 13;

        public readonly int   CommandRowNumMax                  = 100;

        public readonly int   CommandColumnNumDisplay           = 3;

        public readonly int   CommandColumnNumDisplayFullscreen = 3;

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
            this.CommandItemSettings = new CommandItemSettings();

            var commandViewColumnWidth = this.ViewWidth / (GraphicsSettings.Fullscreen 
                                              ? this.CommandColumnNumDisplayFullscreen
                                              : this.CommandColumnNumDisplay);
            var commandItemFixedWidth = this.CommandItemSettings.ExperienceFrameSize.X + this.CommandItemSettings.IdFrameSize.X;

            this.CommandItemSettings.NameFrameSize = new Point(commandViewColumnWidth - commandItemFixedWidth, 24);
            this.CommandItemSettings.Reconfigure();

            this.CommandStartPoint = this.TraceViewSettings.StartPoint + new Point(0, this.TraceViewSettings.RowNumDisplay * this.TraceViewSettings.RootMargin.Y + this.ViewVMargin);
            this.CommandViewSettings = new CommandViewSettings(this.Commandlist)
                                         {
                                             StartPoint = this.CommandStartPoint,
                                             RootMargin = new Point(commandViewColumnWidth, this.CommandItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.Fullscreen
                                                     ? this.CommandColumnNumDisplayFullscreen
                                                     : this.CommandColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.CommandColumnNumDisplayFullscreen
                                                     : this.CommandColumnNumDisplay,

                                             RowNumDisplay = GraphicsSettings.Fullscreen
                                                     ? this.CommandRowNumDisplayFullscreen
                                                     : this.CommandRowNumDisplay,
                                             RowNumMax     = this.CommandRowNumMax,
                                         };

            //-----------------------------------------------------------------
            this.KnowledgeItemSettings = new KnowledgeItemSettings();

            var knowledgeViewColumnWidth = this.ViewWidth / this.KnowledgeViewColumnNumDisplay;
            var knowledgeItemFixedWidth = this.KnowledgeItemSettings.IdFrameSize.X;

            this.KnowledgeItemSettings.NameFrameSize = new Point(knowledgeViewColumnWidth - knowledgeItemFixedWidth, 24);
            this.KnowledgeItemSettings.Reconfigure();

            this.KnowledgeStartPoint = this.CommandViewSettings.StartPoint + new Point(
                                             0,
                                             this.CommandViewSettings.RowNumDisplay * this.CommandViewSettings.RootMargin.Y + this.ViewVMargin);

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

        public Commandlist Commandlist  
        {
            get { return Acutance.Adventure.Commandlist; }
        }

        public List<CommandEntry> Commands
        {
            get { return Acutance.Adventure.Commandlist.Commands; }
        }

        public List<TraceEntry> Traces
        {
            get { return Acutance.Adventure.Tracelist.Traces; }
        }
    }
}