namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupKnowledgeRetrievedListener : ListenerBase
    {
        private readonly IView knowledgeView;

        public MultiplexerGroupKnowledgeRetrievedListener(IView knowledgeView)
        {
            this.knowledgeView = knowledgeView;

            this.RegisteredEvents.Add((int)SessionEventType.KnowledgeRetrieved);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var eventArgs = @event.Data as KnowledgeRetrievedEventArgs;
            if (eventArgs != null)
            {
                this.knowledgeView.Control.LoadResult(eventArgs.Path, false, eventArgs.Offset);
            }

            return true;
        }
    }
}