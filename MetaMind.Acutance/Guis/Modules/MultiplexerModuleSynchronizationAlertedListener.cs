namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Sessions;

    public class MultiplexerModuleSynchronizationAlertedListener : ListenerBase
    {

        private readonly IView traceView;

        public MultiplexerModuleSynchronizationAlertedListener(IView traceView)
        {
            this.traceView = traceView;

            this.RegisteredEvents.Add((int)AdventureEventType.SyncAlerted);
        }

        public override bool HandleEvent(EventBase @event)
        {
            this.traceView.Control.AddItem();
            this.traceView.Control.Selection.Select(this.traceView.Items.Count - 1);
            return true;
        }
    }
}