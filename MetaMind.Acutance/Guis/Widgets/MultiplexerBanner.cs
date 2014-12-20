namespace MetaMind.Acutance.Guis.Widgets
{
    using C3.Primtive2DXna;

    using MetaMind.Acutance.Guis.Modules;
    using MetaMind.Engine;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class MultiplexerBanner : EngineObject
    {
        private readonly TimelineText call;
        private readonly TimelineText knowledge;
        private readonly TimelineText trace;

        private MultiplexerGroupSettings settings;

        public MultiplexerBanner(MultiplexerGroupSettings settings)
        {
            this.settings  = settings;

            this.trace     = new TimelineText("Trace"         , this.TitlePosition(this.settings.TraceStartPoint)    , 0.8f, Font.UiRegularFont);
            this.call      = new TimelineText("Notification"  , this.TitlePosition(this.settings.CallStartPoint)     , 0.8f, Font.UiRegularFont);
            this.knowledge = new TimelineText("Knowledge Base", this.TitlePosition(this.settings.KnowledgeStartPoint), 0.8f, Font.UiRegularFont);
        }

        public void Draw(GameTime gameTime, byte alpha)
        {
            this.trace    .Draw(gameTime, alpha);
            this.call     .Draw(gameTime, alpha);
            this.knowledge.Draw(gameTime, alpha);
        }

        public void Update(GameTime gameTime)
        {
        }

        private Vector2 TitlePosition(Point startPoint)
        {
            return startPoint.ToVector2() + new Vector2(0, -20);
        }
    }
}