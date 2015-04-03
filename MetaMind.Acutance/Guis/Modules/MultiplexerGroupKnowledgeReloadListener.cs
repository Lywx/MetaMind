namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupKnowledgeReloadListener : ListenerBase
    {
        private readonly IView knowledgeView;

        public MultiplexerGroupKnowledgeReloadListener(IView knowledgeView)
        {
            this.knowledgeView = knowledgeView;

            this.RegisteredEvents.Add((int)SessionEventType.ModuleReloaded);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var eventArgs = @event.Data as KnowledgeReloadedEventArgs;
            if (eventArgs != null && 

                // in case of module entry has been disposed
                // so that path is null
                eventArgs.Path != null)
            {
                this.knowledgeView.Control.LoadResult(eventArgs.Path, false, 0);
                this.knowledgeView.Control.LoadBuffer();
            }

            return true;
        }
    }
}