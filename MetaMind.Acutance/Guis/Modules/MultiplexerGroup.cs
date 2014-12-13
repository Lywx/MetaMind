namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>
    {
        public MultiplexerGroup(MultiplexerGroupSettings settings)
            : base(settings)
        {
            this.TraceView = new View(
                this.Settings.TraceViewSettings,
                this.Settings.TraceItemSettings,
                this.Settings.TraceViewFactory);

            this.EventView = new View(
                this.Settings.EventViewSettings,
                this.Settings.EventItemSettings,
                this.Settings.EventViewFactory);

            this.KnowledgeView = new View(
                this.Settings.KnowledgeViewSettings,
                this.Settings.KnowledgeItemSettings,
                this.Settings.KnowledgeViewFactory);
        }

        public IView EventView { get; private set; }

        public IView KnowledgeView { get; private set; }

        public IView TraceView { get; private set; }

        public void Load()
        {
            foreach (var trace in this.Settings.Traces.ToArray())
            {
                this.TraceView.Control.AddItem(trace);
            }

            this.KnowledgeView.Control.LoadResult("Basic.txt");
        }

        public void Unload()
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.KnowledgeView.UpdateInput(gameTime);
            this.TraceView    .UpdateInput(gameTime);
            this.EventView    .UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.TraceView    .UpdateStructure(gameTime);
            this.EventView    .UpdateStructure(gameTime);
            this.KnowledgeView.UpdateStructure(gameTime);
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.KnowledgeView.Draw(gameTime, alpha);
            this.TraceView    .Draw(gameTime, alpha);
            this.EventView    .Draw(gameTime, alpha);
        }
    }
}