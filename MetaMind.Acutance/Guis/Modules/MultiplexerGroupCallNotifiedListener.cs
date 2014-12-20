namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupCallNotifiedListener : ListenerBase
    {
        private readonly IView traceView;
        private readonly IView callView;
        private readonly IView knowledgeView;

        public MultiplexerGroupCallNotifiedListener(IView traceView, IView callView, IView knowledgeView)
        {
            this.callView      = callView;
            this.traceView     = traceView;
            this.knowledgeView = knowledgeView;

            this.RegisteredEvents.Add((int)AdventureEventType.CallNotified);
        }

        public override bool HandleEvent(EventBase @event)
        {
            this.traceView    .Control.Selection.Clear();
            this.traceView    .Control.Region   .Clear();

            this.callView     .Control.Selection.Select(0);

            this.knowledgeView.Control.Selection.Clear();
            this.knowledgeView.Control.Region   .Clear();

            GameEngine.AudioManager.PlayMusic("Illusion Mirror");

            return true;
        }
    }
}