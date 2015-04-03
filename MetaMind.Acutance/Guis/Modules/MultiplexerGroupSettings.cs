namespace MetaMind.Acutance.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Components.Graphics;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroupSettings
    {
        public readonly int   ViewSpace80Characters = 750;

        public readonly int   ViewVMargin = 24;

        public readonly Point ViewStartPoint = new Point(20, 108);

        #region Module View

        public readonly int ModuleRowNumDisplay = 3;

        public readonly int ModuleRowNumDisplayFullscreen = 3;

        public readonly int ModuleRowNumMax = 100;

        public readonly int ModuleColumnNumDisplay = 2;

        public readonly int ModuleColumnNumDisplayFullscreen = 2;

        public ModuleViewFactory ModuleViewFactory = new ModuleViewFactory(Acutance.Session.Modulelist);

        public ModuleViewSettings ModuleViewSettings;

        public ModuleItemSettings ModuleItemSettings;

        public Point ModuleStartPoint;

        #endregion

        #region Command View
        
        public readonly int   CommandRowNumDisplay              = 24;

        public readonly int   CommandRowNumDisplayFullscreen    = 32;

        public readonly int   CommandRowNumMax                  = 100;

        public readonly int   CommandColumnNumDisplay           = 1;

        public readonly int   CommandColumnNumDisplayFullscreen = 1;

        public CommandViewFactory  CommandViewFactory = new CommandViewFactory();
        
        public CommandViewSettings CommandViewSettings;

        public CommandItemSettings CommandItemSettings;

        public Point               CommandStartPoint;

        #endregion

        #region Knowledge View
        
        public readonly int KnowledgeViewRowNumDisplayFullscreen = 27;

        public readonly int KnowledgeViewRowNumDisplay           = 20;

        public readonly int KnowledgeViewRowNumMax               = 100;

        public readonly int KnowledgeViewColumnNumDisplay        = 1;

        public KnowledgeViewFactory  KnowledgeViewFactory = new KnowledgeViewFactory();

        public KnowledgeViewSettings KnowledgeViewSettings;

        public KnowledgeItemSettings KnowledgeItemSettings;

        public Point                 KnowledgeStartPoint;

        #endregion

        public MultiplexerGroupSettings()
        {
            // module view
            var moduleViewWidth = this.ViewSpace80Characters / (GraphicsSettings.IsFullscreen
                                         ? this.ModuleColumnNumDisplayFullscreen
                                         : this.ModuleColumnNumDisplay);
            var moduleStartPoint  = this.ViewStartPoint;
            var moduleColumnWidth = moduleViewWidth / (GraphicsSettings.IsFullscreen
                                          ? this.CommandColumnNumDisplayFullscreen
                                          : this.CommandColumnNumDisplay);
            this.CreateModuleView(moduleColumnWidth, moduleStartPoint);

            // command view
            var commandStartPoint = moduleStartPoint + new Point((GraphicsSettings.IsFullscreen
                                               ? ModuleColumnNumDisplayFullscreen
                                               : ModuleColumnNumDisplay) * moduleColumnWidth, 0);
            var commandColumnWidth = this.ViewWidthMax - moduleViewWidth * (GraphicsSettings.IsFullscreen
                                            ? ModuleColumnNumDisplayFullscreen
                                            : ModuleColumnNumDisplay);
            this.CreateCommandView(commandColumnWidth, commandStartPoint);

            // knowledge view
            var knowledgeStartPoint = this.ModuleStartPoint
                                      + new Point(0, this.ModuleViewSettings.RowNumDisplay * this.ModuleViewSettings.RootMargin.Y + this.ViewVMargin);
            var knowledgeColumnWidth = this.ViewSpace80Characters;
            this.CreateKnowledgeView(knowledgeColumnWidth, knowledgeStartPoint);
        }

        private void CreateModuleView(int viewColumnWidth, Point starPoint)
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

        private void CreateCommandView(int viewColumnWidth, Point startPoint)
        {
            this.CommandItemSettings = new CommandItemSettings();

            var itemFixedWidth = this.CommandItemSettings.IdFrameSize.X;
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