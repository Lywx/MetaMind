namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Elements.Views;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>
    {
        private readonly IView knowledgeView;

        private readonly IView traceView;

        public MultiplexerGroup(MultiplexerGroupSettings settings)
            : base(settings)
        {
            this.traceView     = new View(this.Settings.TraceViewSettings,     this.Settings.TraceItemSettings,     this.Settings.TraceViewFactory);
            this.knowledgeView = new View(this.Settings.KnowledgeViewSettings, this.Settings.KnowledgeItemSettings, this.Settings.KnowledgeViewFactory);
        }

        public IView TraceView
        {
            get { return this.traceView; }
        }

        public void Load()
        {
            foreach (var trace in Acutance.Adventure.Tracelist.Traces.ToArray())
            {
                this.TraceView.Control.AddItem(trace);
            }
        }

        public void Unload()
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.TraceView.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.TraceView    .UpdateStructure(gameTime);
            this.knowledgeView.UpdateStructure(gameTime);
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.knowledgeView.Draw(gameTime, alpha);
            this.TraceView.Draw(gameTime, alpha);
        }
    }
}