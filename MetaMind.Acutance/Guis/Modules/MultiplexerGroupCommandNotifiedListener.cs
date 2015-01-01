namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupCommandNotifiedListener : ListenerBase
    {
        private readonly IView commandView;
        private readonly IView knowledgeView;

        public MultiplexerGroupCommandNotifiedListener(IView commandView, IView knowledgeView)
        {
            this.commandView      = commandView;
            this.knowledgeView = knowledgeView;

            this.RegisteredEvents.Add((int)AdventureEventType.CommandNotified);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var notifiedEventArgs = @event.Data as CommandNotifiedEventArgs;
            if (notifiedEventArgs != null)
            {
                var notifiedCommand = notifiedEventArgs.NotifiedCommand;
                var notifiedItem = this.commandView.Items.Find(item => ReferenceEquals(item.ItemData, notifiedCommand));

                int id = notifiedItem.ItemControl.Id;

                this.commandView.Control.Selection.Select(id);
                this.commandView.Control.Scroll   .Zoom(id);

                // asynchronous speaking
                Acutance.Synthesizer.SpeakAsync(notifiedCommand.Name);
            }

            this.knowledgeView.Control.Selection.Clear();
            this.knowledgeView.Control.Region   .Clear();

            return true;
        }
    }
}