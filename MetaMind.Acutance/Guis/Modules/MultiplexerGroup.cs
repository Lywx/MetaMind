namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>, IConfigurationLoader
    {
        #region Events

        // TODO: Redesign events
        private void LoadEvents(IGameInteropService interop)
        {
            // Module View 
            this.Listeners.Add(new MultiplexerGroupModuleCreatedListener(this.ModuleView));

            // Command View 
            this.Listeners.Add(new MultiplexerGroupCommandNotifiedListener(this.CommandView, this.ModuleView, this.KnowledgeView));

            // Knowledge View 
            this.Listeners.Add(new MultiplexerGroupKnowledgeRetrievedListener(this.KnowledgeView));
            this.Listeners.Add(new MultiplexerGroupKnowledgeReloadListener(this.KnowledgeView));

            // Session 
            this.Listeners.Add(new MultiplexerGroupSessionSavedListener(this));

            base.LoadInterop(interop);
        }

        #endregion

        #region Views

        public IView CommandView { get; private set; }

        public IView KnowledgeView { get; private set; }

        public IView ModuleView { get; set; }

        #endregion

        #region Constructors

        public MultiplexerGroup(MultiplexerGroupSettings settings)
            : base(settings)
        {
            this.ModuleView = new PointView(
                this.Settings.ModuleViewSettings,
                this.Settings.ModuleItemSettings,
                this.Settings.ModuleViewFactory);

            this.CommandView = new PointView(
                this.Settings.CommandViewSettings,
                this.Settings.CommandItemSettings,
                this.Settings.CommandViewFactory);

            this.KnowledgeView = new PointView(
                this.Settings.KnowledgeViewSettings,
                this.Settings.KnowledgeItemSettings,
                this.Settings.KnowledgeViewFactory);
        }


        #endregion

        #region Configuration

        public string ConfigurationFile
        {
            get
            {
                return "Startup.txt";
            }
        }

        public void LoadConfiguration()
        {
            this.CommandFormatDataLoad();
        }

        #endregion

        #region Load and Unload

        public void Load(IGameInputService input, Engine.Services.IGameInteropService interop)
        {
            this.LoadConfiguration();
            this.LoadData();
            this.LoadEvents(interop);
        }

        private void LoadData()
        {
            this.LoadModuleData();
            this.LoadScheduleData();
        }

        public void Unload(IGameInputService input, IGameInteropService interop)
        {
            this.UnloadInterop(interop);
        }

        #region Commands

        private void CommandFormatDataLoad()
        {
            new FormatUtils().LoadConfiguration();
        }


        #endregion

        #region Module

        private void LoadModuleData()
        {
            foreach (var module in this.Settings.Modules.ToArray())
            {
                if (module.Parent == null)
                {
                    this.ModuleView.Control.AddItem(module);
                }
            }
        }

        #endregion

        #region Schedule

        public void ScheduleDataReload()
        {
            this.ScheduleDataUnload();
            this.LoadScheduleData();
        }

        private void LoadScheduleData()
        {
            foreach (var schedule in ScheduleLoader.Load(this))
            {
                // won't add to view but add to command list
                this.Settings.Commands.Add(schedule);
            }
        }

        private void ScheduleDataUnload()
        {
            CommandFileter.RemoveRunningShedule(this.Settings.Commands);
        }

        #endregion

        #endregion

        #region Update and Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.ModuleView   .Draw(graphics, time, alpha);
            this.KnowledgeView.Draw(graphics, time, alpha);
            this.CommandView  .Draw(graphics, time, alpha);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.KnowledgeView.UpdateInput(input, time);
            this.ModuleView   .UpdateInput(input, time);
            this.CommandView  .UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.CommandView  .Update(time);
            this.ModuleView   .Update(time);
            this.KnowledgeView.Update(time);
        }
        #endregion
    }
}