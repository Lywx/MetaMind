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

        private void LoadEvents(IGameInterop gameInterop)
        {
            if (this.moduleCreatedListener == null)
            {
                this.moduleCreatedListener = new MultiplexerGroupModuleCreatedListener(this.ModuleView);
            }

            gameInterop.Event.AddListener(this.moduleCreatedListener);

            if (this.commandNotifiedListener == null)
            {
                this.commandNotifiedListener = new MultiplexerGroupCommandNotifiedListener(this.CommandView, this.ModuleView, this.KnowledgeView);
            }

            gameInterop.Event.AddListener(this.commandNotifiedListener);

            if (this.knowledgeRetrievedListener == null)
            {
                this.knowledgeRetrievedListener = new MultiplexerGroupKnowledgeRetrievedListener(this.KnowledgeView);
            }

            gameInterop.Event.AddListener(this.knowledgeRetrievedListener);

            if (this.knowledgeReloadListener == null)
            {
                this.knowledgeReloadListener = new MultiplexerGroupKnowledgeReloadListener(this.KnowledgeView);
            }

            gameInterop.Event.AddListener(this.knowledgeReloadListener);

            if (this.sessionSavedListener == null)
            {
                this.sessionSavedListener = new MultiplexerGroupSessionSavedListener(this);
            }

            gameInterop.Event.AddListener(this.sessionSavedListener);
        }

        private void UnloadEvents(IGameInterop gameInterop)
        {
            if (this.moduleCreatedListener != null)
            {
                gameInterop.Event.RemoveListener(this.moduleCreatedListener);
            }

            this.moduleCreatedListener = null;

            if (this.commandNotifiedListener != null)
            {
                gameInterop.Event.RemoveListener(this.commandNotifiedListener);
            }

            this.commandNotifiedListener = null;

            if (this.knowledgeRetrievedListener != null)
            {
                gameInterop.Event.RemoveListener(this.knowledgeRetrievedListener);
            }

            this.knowledgeRetrievedListener = null;

            if (this.knowledgeReloadListener != null)
            {
                gameInterop.Event.RemoveListener(this.knowledgeReloadListener);
            }

            this.knowledgeReloadListener = null;

            if (this.sessionSavedListener != null)
            {
                gameInterop.Event.RemoveListener(this.sessionSavedListener);
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

        public void LoadConfiguration()
        {
            this.CommandFormatDataLoad();
        }

        #endregion

        #region Load and Unload

        public void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound)
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

        public void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound)
        {
            this.UnloadEvents(gameInterop);
        }

        #region Commands

        private void CommandFormatDataLoad()
        {
            new FormatHelper().LoadConfiguration();
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

        public override void Update(IGameInput gameInput, GameTime gameTime)
        {
            this.KnowledgeView.Update(gameInput, gameTime);
            this.ModuleView   .Update(gameInput, gameTime);
            this.CommandView  .Update(gameInput, gameTime);
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