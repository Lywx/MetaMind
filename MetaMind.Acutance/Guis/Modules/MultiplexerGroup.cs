namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class MultiplexerGroup : Group<MultiplexerGroupSettings>
    {
        private MultiplexerGroupCallCreatedListener            callCreatedListener;
        private MultiplexerGroupCallNotifiedListener           callNotifiedListener;

        private MultiplexerGroupKnowledgeRetrievedListener     knowledgeRetrievedListener;

        private MultiplexerGroupSynchronizationAlertedListener synchronizationAlertedListener;

        public MultiplexerGroup(MultiplexerGroupSettings settings)
            : base(settings)
        {
            this.TraceView = new View(
                this.Settings.TraceViewSettings,
                this.Settings.TraceItemSettings,
                this.Settings.TraceViewFactory);

            this.CallView = new View(
                this.Settings.CallViewSettings,
                this.Settings.CallItemSettings,
                this.Settings.CallViewFactory);

            this.KnowledgeView = new View(
                this.Settings.KnowledgeViewSettings,
                this.Settings.KnowledgeItemSettings,
                this.Settings.KnowledgeViewFactory);
        }

        public IView CallView { get; private set; }

        public IView KnowledgeView { get; private set; }

        public IView TraceView { get; private set; }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.KnowledgeView.Draw(gameTime, alpha);
            this.TraceView    .Draw(gameTime, alpha);
            this.CallView     .Draw(gameTime, alpha);
        }

        public void Load()
        {
            this.LoadData();
            this.LoadEvents();
        }

        public void Unload()
        {
            if (this.synchronizationAlertedListener != null)
            {
                EventManager.RemoveListener(this.synchronizationAlertedListener);
            }

            this.synchronizationAlertedListener = null;

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
            this.TraceView    .UpdateInput(gameTime);
            this.CallView     .UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.TraceView    .UpdateStructure(gameTime);
            this.CallView     .UpdateStructure(gameTime);
            this.KnowledgeView.UpdateStructure(gameTime);
        }

        private void LoadData()
        {
            foreach (var trace in this.Settings.Traces.ToArray())
            {
                this.TraceView.Control.AddItem(trace);
            }

            foreach (var call in this.Settings.Calls.ToArray())
            {
                this.CallView.Control.AddItem(call);
            }

            this.KnowledgeView.Control.LoadResult("Basic.txt", true);
        }

        private void LoadEvents()
        {
            if (this.synchronizationAlertedListener == null)
            {
                this.synchronizationAlertedListener = new MultiplexerGroupSynchronizationAlertedListener(this.TraceView);
            }

            EventManager.AddListener(this.synchronizationAlertedListener);

            if (this.callCreatedListener == null)
            {
                this.callCreatedListener = new MultiplexerGroupCallCreatedListener(this.CallView);
            }

            EventManager.AddListener(this.callCreatedListener);

            if (this.callNotifiedListener == null)
            {
                this.callNotifiedListener = new MultiplexerGroupCallNotifiedListener();
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