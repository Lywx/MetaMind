namespace MetaMind.Acutance.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Components.Graphics;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroupSettings
    {
        public readonly int ViewVMargin = 28;

        public readonly Point ViewStartPoint = new Point(10, 108);

        //---------------------------------------------------------------------
        public readonly TraceViewFactory  ModuleViewFactory = new TraceViewFactory();
        
        public readonly TraceViewSettings ModuleViewSettings;

        public readonly TraceItemSettings ModuleItemSettings;

        public readonly Point ModuleStartPoint;

        public readonly int   ModuleRowNumDisplay              = 13;

        public readonly int   ModuleRowNumDisplayFullscreen    = 13;

        public readonly int   ModuleRowNumMax                  = 100;

        public readonly int   ModuleColumnNumDisplay           = 1;

        public readonly int   ModuleColumnNumDisplayFullscreen = 1;

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
            this.ModuleItemSettings = new TraceItemSettings();

            var traceViewColumnWidth = 500;
            var traceItemFixedWidth = this.ModuleItemSettings.ExperienceFrameSize.X + this.ModuleItemSettings.IdFrameSize.X;

            this.ModuleItemSettings.NameFrameSize = new Point(traceViewColumnWidth - traceItemFixedWidth, 24);
            this.ModuleItemSettings.Reconfigure();

            this.ModuleStartPoint = new Point(GraphicsSettings.Width - this.ViewStartPoint.X - 500, this.ViewStartPoint.Y);
            this.ModuleViewSettings = new TraceViewSettings
                                         {
                                             StartPoint = this.ModuleStartPoint,
                                             RootMargin = new Point(traceViewColumnWidth, this.ModuleItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.IsFullscreen
                                                     ? this.ModuleColumnNumDisplayFullscreen
                                                     : this.ModuleColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.IsFullscreen
                                                     ? this.ModuleColumnNumDisplayFullscreen
                                                     : this.ModuleColumnNumDisplay,

                                             RowNumDisplay = GraphicsSettings.IsFullscreen
                                                     ? this.ModuleRowNumDisplayFullscreen
                                                     : this.ModuleRowNumDisplay,
                                             RowNumMax     = this.ModuleRowNumMax,
                                         };

            //-----------------------------------------------------------------
            this.CommandItemSettings = new CommandItemSettings();

            var commandViewColumnWidth = (this.ViewWidthMax - 500) / (GraphicsSettings.IsFullscreen 
                                              ? this.CommandColumnNumDisplayFullscreen
                                              : this.CommandColumnNumDisplay);
            var commandItemFixedWidth = this.CommandItemSettings.ExperienceFrameSize.X + this.CommandItemSettings.IdFrameSize.X;

            this.CommandItemSettings.NameFrameSize = new Point(commandViewColumnWidth - commandItemFixedWidth, 24);
            this.CommandItemSettings.Reconfigure();

            this.CommandStartPoint = this.ViewStartPoint;
            this.CommandViewSettings = new CommandViewSettings(this.Commandlist)
                                         {
                                             StartPoint = this.CommandStartPoint,
                                             RootMargin = new Point(commandViewColumnWidth, this.CommandItemSettings.NameFrameSize.Y),

                                             ColumnNumMax = GraphicsSettings.IsFullscreen
                                                     ? this.CommandColumnNumDisplayFullscreen
                                                     : this.CommandColumnNumDisplay,
                                             ColumnNumDisplay = GraphicsSettings.IsFullscreen
                                                     ? this.CommandColumnNumDisplayFullscreen
                                                     : this.CommandColumnNumDisplay,

                                             RowNumDisplay = GraphicsSettings.IsFullscreen
                                                     ? this.CommandRowNumDisplayFullscreen
                                                     : this.CommandRowNumDisplay,
                                             RowNumMax     = this.CommandRowNumMax,
                                         };

            //-----------------------------------------------------------------
            this.KnowledgeItemSettings = new KnowledgeItemSettings();

            var knowledgeViewColumnWidth = this.ViewWidthMax / this.KnowledgeViewColumnNumDisplay;
            var knowledgeItemFixedWidth = this.KnowledgeItemSettings.IdFrameSize.X;

            this.KnowledgeItemSettings.NameFrameSize = new Point(knowledgeViewColumnWidth - knowledgeItemFixedWidth, 24);
            this.KnowledgeItemSettings.Reconfigure();

            this.KnowledgeStartPoint = this.CommandViewSettings.StartPoint + new Point(
                                             0,
                                             this.CommandViewSettings.RowNumDisplay * this.CommandViewSettings.RootMargin.Y + this.ViewVMargin);

            this.KnowledgeViewSettings = new KnowledgeViewSettings
                                             {
                                                 StartPoint = this.KnowledgeStartPoint,
                                                 RootMargin = new Point(knowledgeViewColumnWidth, this.ModuleItemSettings.NameFrameSize.Y),

                                                 ColumnNumMax     = this.KnowledgeViewColumnNumDisplay,
                                                 ColumnNumDisplay = this.KnowledgeViewColumnNumDisplay,

                                                 RowNumDisplay = GraphicsSettings.IsFullscreen
                                                         ? this.KnowledgeViewRowNumDisplayFullscreen
                                                         : this.KnowledgeViewRowNumDisplay,
                                                 RowNumMax     = this.KnowledgeViewRowNumMax,
                                             };
        }

        private int ViewWidthMax
        {
            get { return GraphicsSettings.Width - this.ViewStartPoint.X * 2; }
        }

        public ICommandlist Commandlist  
        {
            get { return Acutance.Session.Commandlist; }
        }

        public List<CommandEntry> Commands
        {
            get { return Acutance.Session.Commandlist.Commands; }
        }

        public List<TraceEntry> Modules
        {
            get { return Acutance.Session.Tracelist.Traces; }
        }
    }
}