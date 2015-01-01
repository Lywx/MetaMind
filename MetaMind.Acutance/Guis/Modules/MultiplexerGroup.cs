namespace MetaMind.Acutance.Guis.Modules
{
    using System.IO;

    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Parsers;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>
    {
        private readonly string commandStartup = @"Configurations/Startup.txt";

        private MultiplexerGroupCommandCreatedListener         commandCreatedListener;
        private MultiplexerGroupCommandNotifiedListener        commandNotifiedListener;

        private MultiplexerGroupKnowledgeRetrievedListener     knowledgeRetrievedListener;

        public MultiplexerGroup(MultiplexerGroupSettings settings)
            : base(settings)
        {
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

        public IView KnowledgeView { get; private set; }

        public override void Draw(GameTime gameTime, byte alpha)
        {
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
            if (this.commandCreatedListener != null)
            {
                EventManager.RemoveListener(this.commandCreatedListener);
            }

            this.commandCreatedListener = null;

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
            this.CommandView  .UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.CommandView  .UpdateStructure(gameTime);
            this.KnowledgeView.UpdateStructure(gameTime);
        }

        private void LoadData()
        {
            var commandStartupFile = string.Empty;
            var scheduleFolder     = string.Empty;

            foreach (var line in File.ReadAllLines(this.commandStartup))
            {
                commandStartupFile = Parser.ParseLine(line, @"^Startup=(.*)")       .Replace("Startup=", string.Empty); 
                scheduleFolder     = Parser.ParseLine(line, @"^ScheduleFolder=(.*)").Replace("ScheduleFolder=", string.Empty); 
            }

            foreach (var command in this.Settings.Commands.ToArray())
            {
                this.CommandView.Control.AddItem(command);
            }

            if (!string.IsNullOrEmpty(scheduleFolder))
            {
            }

            if (!string.IsNullOrEmpty(commandStartupFile))
            {
                this.KnowledgeView.Control.LoadResult(commandStartupFile, true);
            }
        }

        private void LoadEvents()
        {
            if (this.commandCreatedListener == null)
            {
                this.commandCreatedListener = new MultiplexerGroupCommandCreatedListener(this.CommandView);
            }

            EventManager.AddListener(this.commandCreatedListener);

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