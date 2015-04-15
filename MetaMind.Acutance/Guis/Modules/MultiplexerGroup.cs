namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>, IConfigurationFileLoader
    {
        #region Events

        // TODO: Redesign events
        private void LoadEvents(IGameInterop gameInterop)
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

            base.LoadInterop(gameInterop);
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

        public void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            this.LoadConfiguration();
            this.LoadData();
            this.LoadEvents(gameInterop);
        }

        private void LoadData()
        {
            this.LoadModuleData();
            this.LoadScheduleData();
        }

        public void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            this.UnloadInterop(gameInterop);
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

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.ModuleView   .Draw(gameGraphics, gameTime, alpha);
            this.KnowledgeView.Draw(gameGraphics, gameTime, alpha);
            this.CommandView  .Draw(gameGraphics, gameTime, alpha);
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.KnowledgeView.UpdateInput(gameInput, gameTime);
            this.ModuleView   .UpdateInput(gameInput, gameTime);
            this.CommandView  .UpdateInput(gameInput, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.CommandView  .Update(gameTime);
            this.ModuleView   .Update(gameTime);
            this.KnowledgeView.Update(gameTime);
        }
        #endregion
    }
}