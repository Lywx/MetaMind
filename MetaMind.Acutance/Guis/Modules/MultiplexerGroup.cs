namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Events;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>, IConfigurationLoader
    {
        private MultiplexerGroupCommandNotifiedListener        commandNotifiedListener;

        private MultiplexerGroupModuleCreatedListener          moduleCreatedListener;

        private MultiplexerGroupKnowledgeRetrievedListener     knowledgeRetrievedListener;

        public MultiplexerGroup(MultiplexerGroupSettings settings)
            : base(settings)
        {
            this.ModuleView = new View(
                this.Settings.ModuleViewSettings,
                this.Settings.ModuleItemSettings,
                this.Settings.ModuleViewFactory);

            this.CommandView = new View(
                this.Settings.CommandViewSettings,
                this.Settings.CommandItemSettings,
                this.Settings.CommandViewFactory);

            this.KnowledgeView = new View(
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
            // load modules
            foreach (var module in this.Settings.Modules.ToArray())
            {
                this.ModuleView.Control.AddItem(module);
            }

            // load schedules
            foreach (var schedule in ScheduleLoader.Load(this))
            {
                // won't add to view but add to command list
                this.Settings.Commands.Add(schedule);
            }
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
                this.commandNotifiedListener = new MultiplexerGroupCommandNotifiedListener(this.CommandView, this.KnowledgeView);
            }

            EventManager.AddListener(this.commandNotifiedListener);

            if (this.knowledgeRetrievedListener == null)
            {
                this.knowledgeRetrievedListener = new MultiplexerGroupKnowledgeRetrievedListener(this.KnowledgeView);
            }

            EventManager.AddListener(this.knowledgeRetrievedListener);
        }
    }
}