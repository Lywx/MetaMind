namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Loaders;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroupFactory : IConfigurationParameterLoader<GraphicsSettings>
    {
        #region Parameters Data

        private bool IsFullscreen { get; set; }

        private int Width { get; set; }

        private int Height { get; set; }

        public void ParameterLoad(GraphicsSettings parameter)
        {
            this.Width        = parameter.Width;
            this.Height       = parameter.Height;

            this.IsFullscreen = parameter.IsFullscreen;
        }

        #endregion

        #region Shared Data

        private readonly int   ViewSpace80Characters = 750;

        private readonly Point ViewStartPoint        = new Point(20, 108);

        private readonly int   ViewVMargin           = 24;

        private int            ViewWidthMax
        {
            get
            {
                return this.Width - this.ViewStartPoint.X * 2;
            }
        }

        #endregion

        #region Module View Data

        private readonly int ModuleColumnNumDisplay           = 2;

        private readonly int ModuleColumnNumDisplayFullscreen = 2;

        private readonly int ModuleRowNumDisplay              = 3;

        private readonly int ModuleRowNumDisplayFullscreen    = 3;

        private readonly int ModuleRowNumMax                  = 100;

        private Point              ModuleStartPoint;

        private int                ModuleViewWidth;

        private int                ModuleColumnWidth;

        private ModuleItemSettings ModuleItemSettings;

        private ModuleViewSettings ModuleViewSettings;

        private ModuleViewFactory  ModuleViewFactory = new ModuleViewFactory(Acutance.Session.Modulelist);

        #endregion

        #region Command View Data

        private readonly int   CommandColumnNumDisplay           = 1;

        private readonly int   CommandColumnNumDisplayFullscreen = 1;

        private readonly int   CommandRowNumDisplay              = 24;

        private readonly int   CommandRowNumDisplayFullscreen    = 32;

        private readonly int   CommandRowNumMax                  = 100;

        private CommandItemSettings CommandItemSettings;

        private Point               CommandStartPoint;

        private int                 CommandColumnWidth;

        private CommandViewFactory  CommandViewFactory = new CommandViewFactory();

        private CommandViewSettings CommandViewSettings;

        #endregion

        #region Knowledge View Data

        private readonly int KnowledgeViewColumnNumDisplay        = 1;

        private readonly int KnowledgeViewRowNumDisplay           = 20;

        private readonly int KnowledgeViewRowNumDisplayFullscreen = 27;

        private readonly int KnowledgeViewRowNumMax               = 100;

        private int                   KnowledgeColumnWidth;

        private Point                 KnowledgeStartPoint;

        private KnowledgeItemSettings KnowledgeItemSettings;

        private KnowledgeViewFactory  KnowledgeViewFactory = new KnowledgeViewFactory();

        private KnowledgeViewSettings KnowledgeViewSettings;

        #endregion

        public MultiplexerGroupFactory()
        {
            this.ParameterLoad(GameEngine.GraphicsSettings);
        }

        private MultiplexerGroupSettings CreateGroupSettings()
        {
            this.StructureView();

            this.CreateModuleView(this.ModuleColumnWidth, this.ModuleStartPoint);
            this.CreateCommandView(this.CommandColumnWidth, this.CommandStartPoint);
            this.CreateKnowledgeView(this.KnowledgeColumnWidth, this.KnowledgeStartPoint);
        }

        private void StructureView()
        {
            // module view
            this.ModuleStartPoint = this.ViewStartPoint;
            this.ModuleViewWidth = this.IsFullscreen
                                       ? (this.ViewSpace80Characters / this.ModuleColumnNumDisplayFullscreen)
                                       : (this.ViewSpace80Characters / this.ModuleColumnNumDisplay);
            this.ModuleColumnWidth = this.IsFullscreen
                                         ? (this.ModuleViewWidth / this.CommandColumnNumDisplayFullscreen)
                                         : (this.ModuleViewWidth / this.CommandColumnNumDisplay);

            // command view
            this.CommandStartPoint = this.IsFullscreen
                                         ? (this.ModuleStartPoint
                                            + new Point(this.ModuleColumnWidth * this.ModuleColumnNumDisplayFullscreen, 0))
                                         : (this.ModuleStartPoint
                                            + new Point(this.ModuleColumnWidth * this.ModuleColumnNumDisplay, 0));
            this.CommandColumnWidth = this.IsFullscreen
                                          ? (this.ViewWidthMax - this.ModuleViewWidth * this.ModuleColumnNumDisplayFullscreen)
                                          : (this.ViewWidthMax - this.ModuleViewWidth * this.ModuleColumnNumDisplay);

            // knowledge view
            this.KnowledgeStartPoint = this.ModuleStartPoint + new Point( 0, this.ModuleViewSettings.RowNumDisplay * this.ModuleViewSettings.PointMargin.Y + this.ViewVMargin);
            this.KnowledgeColumnWidth = this.ViewSpace80Characters;
        }

        private void CreateCommandView(int viewColumnWidth, Point startPoint)
        {
            this.CommandItemSettings = new CommandItemSettings();

            var itemFixedWidth = this.CommandItemSettings.IdFrameSize.X;
            this.CommandItemSettings.NameFrameSize = new Point(viewColumnWidth - itemFixedWidth, 24);
            this.CommandItemSettings.Configure();

            this.CommandStartPoint = startPoint;
            this.CommandViewSettings = new CommandViewSettings(this.Commandlist)
                                           {
                                               PointStart = this.CommandStartPoint,
                                               PointMargin = new Point(viewColumnWidth, this.CommandItemSettings.NameFrameSize.Y),

                                               ColumnNumMax = this.IsFullscreen
                                                                  ? this.CommandColumnNumDisplayFullscreen
                                                                  : this.CommandColumnNumDisplay,
                                               ColumnNumDisplay = this.IsFullscreen
                                                                      ? this.CommandColumnNumDisplayFullscreen
                                                                      : this.CommandColumnNumDisplay,

                                               RowNumDisplay = this.IsFullscreen
                                                                   ? this.CommandRowNumDisplayFullscreen
                                                                   : this.CommandRowNumDisplay,
                                               RowNumMax = this.CommandRowNumMax,
                                           };
        }

        private void CreateKnowledgeView(int viewColumnWidth, Point startPoint)
        {
            this.KnowledgeItemSettings = new KnowledgeItemSettings();

            var itemFixedWidth = this.KnowledgeItemSettings.IdFrameSize.X;
            this.KnowledgeItemSettings.NameFrameSize = new Point(viewColumnWidth - itemFixedWidth, 24);
            this.KnowledgeItemSettings.Configure();

            this.KnowledgeStartPoint = startPoint;

            this.KnowledgeViewSettings = new KnowledgeViewSettings
                                             {
                                                 PointStart = this.KnowledgeStartPoint,
                                                 PointMargin = new Point(viewColumnWidth, this.ModuleItemSettings.NameFrameSize.Y),

                                                 ColumnNumMax = this.KnowledgeViewColumnNumDisplay,
                                                 ColumnNumDisplay = this.KnowledgeViewColumnNumDisplay,

                                                 RowNumDisplay = this.IsFullscreen
                                                                     ? this.KnowledgeViewRowNumDisplayFullscreen
                                                                     : this.KnowledgeViewRowNumDisplay,
                                                 RowNumMax = this.KnowledgeViewRowNumMax,
                                             };
        }

        void CreateView(int viewColumnWidth, Point start, int )
        {
            IAdjustable itemSettings = new
        }

        private void CreateModuleView(int viewColumnWidth, Point starPoint)
        {
            this.ModuleItemSettings = new ModuleItemSettings();

            var itemFixedWidth = this.ModuleItemSettings.ExperienceFrameSize.X + this.ModuleItemSettings.IdFrameSize.X;
            this.ModuleItemSettings.NameFrameSize = new Point(viewColumnWidth - itemFixedWidth, 24);
            this.ModuleItemSettings.Configure();

            this.ModuleStartPoint = starPoint;
            this.ModuleViewSettings = new ModuleViewSettings(this.Modulelist)
                                          {
                                              PointStart = this.ModuleStartPoint,
                                              PointMargin = new Point(viewColumnWidth, this.ModuleItemSettings.NameFrameSize.Y),

                                              ColumnNumMax = this.IsFullscreen
                                                                 ? this.ModuleColumnNumDisplayFullscreen
                                                                 : this.ModuleColumnNumDisplay,
                                              ColumnNumDisplay = this.IsFullscreen
                                                                     ? this.ModuleColumnNumDisplayFullscreen
                                                                     : this.ModuleColumnNumDisplay,

                                              RowNumDisplay = this.IsFullscreen
                                                                  ? this.ModuleRowNumDisplayFullscreen
                                                                  : this.ModuleRowNumDisplay,
                                              RowNumMax     = this.ModuleRowNumMax,
                                          };
        }
    }
}