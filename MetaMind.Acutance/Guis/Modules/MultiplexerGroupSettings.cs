namespace MetaMind.Acutance.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    public class MultiplexerGroupSettings
    {
        public MultiplexerGroupSettings(ICommandlist commandlist, IModulelist modulelist)
        {
            this.Commandlist = commandlist;
            this.Modulelist  = modulelist;
        }

        #region Module View Data

        public ItemSettings ModuleItemSettings { get; set; }

        public List<Module> Modules
        {
            get { return this.Modulelist.Modules; }
        }

        public IViewFactory ModuleViewFactory { get; set; }

        public PointView2DSettings ModuleViewSettings { get; set; }

        private IModulelist Modulelist { get; set; }

        #endregion Module View Data

        #region Command View Data

        public ItemSettings CommandItemSettings { get; set; }

        public ICommandlist Commandlist { get; set; }

        public List<Command> Commands
        {
            get { return this.Commandlist.Commands; }
        }

        public IViewFactory CommandViewFactory { get; set; }

        public PointView2DSettings CommandViewSettings { get; set; }

        #endregion Command View Data

        #region Knowledge View Data

        public ItemSettings KnowledgeItemSettings { get; set; }

        public IViewFactory KnowledgeViewFactory { get; set; }

        public PointView2DSettings KnowledgeViewSettings { get; set; }

        #endregion Knowledge View Data
    }
}