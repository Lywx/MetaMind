namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>
    {
        private MultiplexerGroupCallCreatedListener            callCreatedListener;
        private MultiplexerGroupCallNotifiedListener           callNotifiedListener;

        private MultiplexerGroupKnowledgeRetrievedListener     knowledgeRetrievedListener;

        private MultiplexerBanner banner;

        public MultiplexerGroup(MultiplexerGroupSettings settings)
            : base(settings)
        {
            this.CallView = new View(
                this.Settings.CallViewSettings,
                this.Settings.CallItemSettings,
                this.Settings.CallViewFactory);

            this.KnowledgeView = new View(
                this.Settings.KnowledgeViewSettings,
                this.Settings.KnowledgeItemSettings,
                this.Settings.KnowledgeViewFactory);

            this.banner = new MultiplexerBanner(settings);
        }

        public IView CallView { get; private set; }

        public IView KnowledgeView { get; private set; }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.KnowledgeView.Draw(gameTime, alpha);
            this.CallView     .Draw(gameTime, alpha);

            this.banner       .Draw(gameTime, alpha);
        }

        public void Load()
        {
            this.LoadData();
            this.LoadEvents();
        }

        public void Unload()
        {
            if (this.callCreatedListener != null)
            {
                EventManager.RemoveListener(this.callCreatedListener);
            }

            this.callCreatedListener = null;

            if (this.callNotifiedListener != null)
            {
                EventManager.RemoveListener(this.callNotifiedListener);
            }

            this.callNotifiedListener = null;

            if (this.knowledgeRetrievedListener != null)
            {
                EventManager.RemoveListener(this.knowledgeRetrievedListener);
            }

            this.knowledgeRetrievedListener = null;
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.KnowledgeView.UpdateInput(gameTime);
            this.CallView     .UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.CallView     .UpdateStructure(gameTime);
            this.KnowledgeView.UpdateStructure(gameTime);

            this.banner       .Update(gameTime);
        }

        private void LoadData()
        {
            foreach (var call in this.Settings.Calls.ToArray())
            {
                this.CallView.Control.AddItem(call);
            }

            this.KnowledgeView.Control.LoadResult("Basic.txt", true);
        }

        private void LoadEvents()
        {
            if (this.callCreatedListener == null)
            {
                this.callCreatedListener = new MultiplexerGroupCallCreatedListener(this.CallView);
            }

            EventManager.AddListener(this.callCreatedListener);

            if (this.callNotifiedListener == null)
            {
                this.callNotifiedListener = new MultiplexerGroupCallNotifiedListener(this.CallView, this.KnowledgeView);
            }

            EventManager.AddListener(this.callNotifiedListener);

            if (this.knowledgeRetrievedListener == null)
            {
                this.knowledgeRetrievedListener = new MultiplexerGroupKnowledgeRetrievedListener(this.KnowledgeView);
            }

            EventManager.AddListener(this.knowledgeRetrievedListener);
        }
    }
}