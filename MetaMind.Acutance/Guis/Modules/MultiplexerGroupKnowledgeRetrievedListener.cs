namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupKnowledgeRetrievedListener : ListenerBase
    {
        private readonly IView knowledgeView;

        public MultiplexerGroupKnowledgeRetrievedListener(IView knowledgeView)
        {
            this.knowledgeView = knowledgeView;

            this.RegisteredEvents.Add((int)AdventureEventType.KnowledgeRetrieved);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var callSelectedArgs = @event.Data as KnowledgeRetrievedEventArgs;
            if (callSelectedArgs != null)
            {
                this.knowledgeView.Control.LoadResult(callSelectedArgs.Path, false);
            }

            return true;
        }
    }
}