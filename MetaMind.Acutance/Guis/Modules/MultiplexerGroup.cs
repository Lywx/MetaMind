namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>, IConfigurationLoader
    {
        private MultiplexerGroupCommandNotifiedListener    commandNotifiedListener;

        private MultiplexerGroupKnowledgeRetrievedListener knowledgeRetrievedListener;

        private MultiplexerGroupKnowledgeReloadListener    knowledgeReloadListener;

        private MultiplexerGroupModuleCreatedListener      moduleCreatedListener;

        private MultiplexerGroupSessionSavedListener       sessionSavedListener;

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

        public IView CommandView { get; private set; }

        public string ConfigurationFile
        {
            get
            {
                return "Startup.txt";
            }
        }

        public IView KnowledgeView { get; private set; }

        public IView ModuleView { get; set; }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.ModuleView   .Draw(gameTime, alpha);
            this.KnowledgeView.Draw(gameTime, alpha);
            this.CommandView  .Draw(gameTime, alpha);
        }

        public void Load()
        {
            this.LoadData();
            this.LoadEvents();
        }

        public void ReloadScheduleData()
        {
            this.UnloadScheduleData();
            this.LoadScheduleData();
        }

        public void Unload()
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

        public override void UpdateInput(GameTime gameTime)
        {
            this.KnowledgeView.UpdateInput(gameTime);
            this.ModuleView   .UpdateInput(gameTime);
            this.CommandView  .UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.CommandView  .UpdateStructure(gameTime);
            this.ModuleView   .UpdateStructure(gameTime);
            this.KnowledgeView.UpdateStructure(gameTime);
        }

        private void LoadData()
        {
            this.LoadModuleData();
            this.LoadScheduleData();
            this.LoadFormatData();
        }

        private void LoadEvents()
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

        private void LoadModuleData()
        {
            foreach (var module in this.Settings.Modules.ToArray())
            {
                if (module.ParentModuleEntry == null)
                {
                    this.ModuleView.Control.AddItem(module);
                }
            }
        }

        private void LoadScheduleData()
        {
            foreach (var schedule in ScheduleLoader.Load(this))
            {
                // won't add to view but add to command list
                this.Settings.Commands.Add(schedule);
            }
        }

        private void LoadFormatData()
        {
            Format.Load(new Format());
        }

        private void UnloadScheduleData()
        {
            CommandEntryFileter.RemoveRunningShedule(this.Settings.Commands);
        }
    }
}