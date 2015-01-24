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

        public readonly Point ViewStartPoint = new Point(20, 108);

        //---------------------------------------------------------------------
        public ModuleViewFactory  ModuleViewFactory = new ModuleViewFactory(Acutance.Session.Modulelist);
        
        public ModuleViewSettings ModuleViewSettings;

        public ModuleItemSettings ModuleItemSettings;

        public Point ModuleStartPoint;

        public readonly int   ModuleRowNumDisplay              = 13;

        public readonly int   ModuleRowNumDisplayFullscreen    = 13;

        public readonly int   ModuleRowNumMax                  = 100;

        public readonly int   ModuleColumnNumDisplay           = 1;

        public readonly int   ModuleColumnNumDisplayFullscreen = 1;

        //---------------------------------------------------------------------
        public CommandViewFactory  CommandViewFactory = new CommandViewFactory();
        
        public CommandViewSettings CommandViewSettings;

        public CommandItemSettings CommandItemSettings;

        public Point CommandStartPoint;

        public readonly int   CommandRowNumDisplay              = 13;

        public readonly int   CommandRowNumDisplayFullscreen    = 13;

        public readonly int   CommandRowNumMax                  = 100;

        public readonly int   CommandColumnNumDisplay           = 1;

        public readonly int   CommandColumnNumDisplayFullscreen = 1;

        //---------------------------------------------------------------------
        public KnowledgeViewFactory  KnowledgeViewFactory = new KnowledgeViewFactory();

        public KnowledgeViewSettings KnowledgeViewSettings;

        public KnowledgeItemSettings KnowledgeItemSettings;

        public Point KnowledgeStartPoint;

        public readonly int KnowledgeViewRowNumDisplayFullscreen = 17;

        public readonly int KnowledgeViewRowNumDisplay           = 9;

        public readonly int KnowledgeViewRowNumMax               = 100;

        public readonly int KnowledgeViewColumnNumDisplay        = 1;

        public void CreateModuleView(int viewColumnWidth, Point starPoint)
        {
            this.ModuleItemSettings = new ModuleItemSettings();

            var itemFixedWidth = this.ModuleItemSettings.ExperienceFrameSize.X + this.ModuleItemSettings.IdFrameSize.X;
            this.ModuleItemSettings.NameFrameSize = new Point(viewColumnWidth - itemFixedWidth, 24);
            this.ModuleItemSettings.Reconfigure();

            this.ModuleStartPoint = starPoint;
            this.ModuleViewSettings = new ModuleViewSettings(this.Modulelist)
                                         {
                                             StartPoint = this.ModuleStartPoint,
                                             RootMargin = new Point(viewColumnWidth, this.ModuleItemSettings.NameFrameSize.Y),

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

            
        }

        public void CreateCommandView(int viewColumnWidth, Point startPoint)
        {
            this.CommandItemSettings = new CommandItemSettings();

            var itemFixedWidth = this.CommandItemSettings.ExperienceFrameSize.X + this.CommandItemSettings.IdFrameSize.X;
            this.CommandItemSettings.NameFrameSize = new Point(viewColumnWidth - itemFixedWidth, 24);
            this.CommandItemSettings.Reconfigure();

            this.CommandStartPoint = startPoint;
            this.CommandViewSettings = new CommandViewSettings(this.Commandlist)
                                         {
                                             StartPoint = this.CommandStartPoint,
                                             RootMargin = new Point(viewColumnWidth, this.CommandItemSettings.NameFrameSize.Y),

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

            
        }

        public MultiplexerGroupSettings()
        {
            // module view
            var moduleStartPoint = this.ViewStartPoint;
            var moduleViewWidth = 500;
            var moduleColumnWidth = moduleViewWidth / (GraphicsSettings.IsFullscreen
                                          ? this.CommandColumnNumDisplayFullscreen
                                          : this.CommandColumnNumDisplay);
            this.CreateModuleView(moduleColumnWidth, moduleStartPoint);

            // command view
            var commandStartPoint = moduleStartPoint + new Point(moduleColumnWidth, 0);
            var commandColumnWidth = this.ViewWidthMax - moduleViewWidth;
            this.CreateCommandView(commandColumnWidth, commandStartPoint);

            // knowledge view
            var knowledgeStartPoint = this.ModuleStartPoint
                                      + new Point(0, this.ModuleViewSettings.RowNumDisplay * this.ModuleViewSettings.RootMargin.Y + this.ViewVMargin);
            var knowledgeColumnWidth = this.ViewWidthMax / this.KnowledgeViewColumnNumDisplay;
            this.CreateKnowledgeView(knowledgeColumnWidth, knowledgeStartPoint);
        }

        private void CreateKnowledgeView(int viewColumnWidth, Point startPoint)
        {
            this.KnowledgeItemSettings = new KnowledgeItemSettings();

            var itemFixedWidth = this.KnowledgeItemSettings.IdFrameSize.X;

            this.KnowledgeItemSettings.NameFrameSize = new Point(viewColumnWidth - itemFixedWidth, 24);
            this.KnowledgeItemSettings.Reconfigure();

            this.KnowledgeStartPoint = startPoint;

            this.KnowledgeViewSettings = new KnowledgeViewSettings
                                             {
                                                 StartPoint = this.KnowledgeStartPoint,
                                                 RootMargin = new Point(viewColumnWidth, this.ModuleItemSettings.NameFrameSize.Y),

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

        public IModulelist Modulelist  
        {
            get { return Acutance.Session.Modulelist; }
        }

        public List<ModuleEntry> Modules
        {
            get { return Acutance.Session.Modulelist.Modules; }
        }
    }
}