namespace MetaMind.Acutance.Guis.Modules
{
    using System.Speech.Synthesis;

    using MetaMind.Acutance.Concepts;
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
            var notifiedEventArgs = @event.Data as CallNotifiedEventArgs;
            if (notifiedEventArgs != null)
            {
                var notifiedCall = notifiedEventArgs.NotifiedCall;
                var notifiedItem = this.callView.Items.Find(item => ReferenceEquals(item.ItemData, notifiedCall));

                int id = notifiedItem.ItemControl.Id;

                this.callView.Control.Selection.Select(id);
                this.callView.Control.Scroll   .Zoom(id);

                // asynchronous speaking
                Acutance.Synthesizer.SpeakAsync(notifiedCall.Name);
            }

            this.traceView    .Control.Selection.Clear();
            this.traceView    .Control.Region   .Clear();

            this.knowledgeView.Control.Selection.Clear();
            this.knowledgeView.Control.Region   .Clear();

            return true;
        }
    }
}