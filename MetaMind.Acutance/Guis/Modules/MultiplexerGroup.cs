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
        #region Listeners

        #region Knowledge View

        private MultiplexerGroupKnowledgeReloadListener knowledgeReloadListener;

        private MultiplexerGroupKnowledgeRetrievedListener knowledgeRetrievedListener;

        #endregion

        #region Command View

        private MultiplexerGroupCommandNotifiedListener commandNotifiedListener;

        #endregion

        #region Module View

        private MultiplexerGroupModuleCreatedListener moduleCreatedListener;

        #endregion

        #region Session

        private MultiplexerGroupSessionSavedListener sessionSavedListener;

        #endregion

        private void EventLoad()
        {
            if (this.moduleCreatedListener == null)
            {
                this.moduleCreatedListener = new MultiplexerGroupModuleCreatedListener(this.ModuleView);
            }

            EventManager.AddListener(this.moduleCreatedListener);

            if (this.commandNotifiedListener == null)
            {
                this.commandNotifiedListener = new MultiplexerGroupCommandNotifiedListener(this.CommandView, this.ModuleView, this.KnowledgeView);
            }

            EventManager.AddListener(this.commandNotifiedListener);

            if (this.knowledgeRetrievedListener == null)
            {
                this.knowledgeRetrievedListener = new MultiplexerGroupKnowledgeRetrievedListener(this.KnowledgeView);
            }

            EventManager.AddListener(this.knowledgeRetrievedListener);

            if (this.knowledgeReloadListener == null)
            {
                this.knowledgeReloadListener = new MultiplexerGroupKnowledgeReloadListener(this.KnowledgeView);
            }

            EventManager.AddListener(this.knowledgeReloadListener);

            if (this.sessionSavedListener == null)
            {
                this.sessionSavedListener = new MultiplexerGroupSessionSavedListener(this);
            }

            EventManager.AddListener(this.sessionSavedListener);
        }

        private void EventUnload()
        {
            if (this.moduleCreatedListener != null)
            {
                EventManager.RemoveListener(this.moduleCreatedListener);
            }

            this.moduleCreatedListener = null;

            if (this.commandNotifiedListener != null)
            {
                EventManager.RemoveListener(this.commandNotifiedListener);
            }

            this.commandNotifiedListener = null;

            if (this.knowledgeRetrievedListener != null)
            {
                EventManager.RemoveListener(this.knowledgeRetrievedListener);
            }

            this.knowledgeRetrievedListener = null;

            if (this.knowledgeReloadListener != null)
            {
                EventManager.RemoveListener(this.knowledgeReloadListener);
            }

            this.knowledgeReloadListener = null;

            if (this.sessionSavedListener != null)
            {
                EventManager.RemoveListener(this.sessionSavedListener);
            }

            this.sessionSavedListener = null;
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

        public void ConfigurationLoad()
        {
            this.CommandFormatDataLoad();
        }

        #endregion

        #region Load and Unload

        public void Load()
        {
            this.ConfigurationLoad();
            this.DataLoad();
            this.EventLoad();
        }

        private void DataLoad()
        {
            this.ModuleDataLoad();
            this.ScheduleDataLoad();
        }

        public void Unload()
        {
            this.EventUnload();
        }

        #region Commands

        private void CommandFormatDataLoad()
        {
            new FormatHelper().ConfigurationLoad();
        }


        #endregion

        #region Module

        private void ModuleDataLoad()
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
            this.ScheduleDataLoad();
        }

        private void ScheduleDataLoad()
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

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.ModuleView   .Draw(gameTime, alpha);
            this.KnowledgeView.Draw(gameTime, alpha);
            this.CommandView  .Draw(gameTime, alpha);
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.KnowledgeView.UpdateInput(gameInput, gameTime);
            this.ModuleView   .UpdateInput(gameInput, gameTime);
            this.CommandView  .UpdateInput(gameInput, gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.CommandView  .UpdateStructure(gameTime);
            this.ModuleView   .UpdateStructure(gameTime);
            this.KnowledgeView.UpdateStructure(gameTime);
        }
        #endregion
    }
}