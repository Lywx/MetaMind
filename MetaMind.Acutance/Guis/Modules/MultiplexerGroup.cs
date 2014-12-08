namespace MetaMind.Acutance.Guis.Modules
{
    using System.IO;
    using System.Linq;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Elements.Views;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>
    {
        public MultiplexerGroup(MultiplexerGroupSettings settings)
            : base(settings)
        {
            this.PTraceView    = new View(this.Settings.PTraceViewSettings,     this.Settings.PTraceItemSettings,     this.Settings.PTraceViewFactory);
            this.NTraceView    = new View(this.Settings.NTraceViewSettings,     this.Settings.NTraceItemSettings,     this.Settings.NTraceViewFactory);
            this.KnowledgeView = new View(this.Settings.KnowledgeViewSettings,  this.Settings.KnowledgeItemSettings,  this.Settings.KnowledgeViewFactory);
        }

        public IView NTraceView { get; private set; }

        public IView KnowledgeView { get; private set; }

        public IView PTraceView { get; private set; }

        public void Load()
        {
            foreach (var trace in Acutance.Adventure.Tracelist.Traces.ToArray())
            {
                if (trace.Positive)
                {
                    this.PTraceView.Control.AddItem(trace);
                }
                else
                {
                    this.NTraceView.Control.AddItem(trace);
                }
            }

            // TODO: implement mode selection
            var lines = File.ReadLines(Path.Combine(FolderManager.DataFolderPath, "Basic.txt"));
            foreach (var line in lines.Where(line => !string.IsNullOrWhiteSpace(line)))
            {
                KnowledgeView.Control.AddItem(new TraceEntry { Name = line });
            }
        }

        public void Unload()
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.KnowledgeView.UpdateInput(gameTime);
            this.PTraceView   .UpdateInput(gameTime);
            this.NTraceView   .UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.PTraceView   .UpdateStructure(gameTime);
            this.NTraceView   .UpdateStructure(gameTime);
            this.KnowledgeView.UpdateStructure(gameTime);
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.KnowledgeView.Draw(gameTime, alpha);
            this.PTraceView   .Draw(gameTime, alpha);
            this.NTraceView   .Draw(gameTime, alpha);
        }
    }
}