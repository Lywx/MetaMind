namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupKnowledgeRetrievedListener : Listener
    {
        private readonly IView knowledgeView;

        public MultiplexerGroupKnowledgeRetrievedListener(IView knowledgeView)
        {
            this.knowledgeView = knowledgeView;

            this.RegisteredEvents.Add((int)SessionEventType.KnowledgeRetrieved);
        }

        public override bool HandleEvent(IEvent e)
        {
            var eventArgs = e.EventData as KnowledgeRetrievedEventArgs;
            if (eventArgs != null && 

                // in case of module entry has been disposed
                // so that path is null
                eventArgs.Path != null)
            {
                this.knowledgeView.Control.LoadResult(eventArgs.Path, false, eventArgs.Offset);
            }

            return true;
        }
    }
}