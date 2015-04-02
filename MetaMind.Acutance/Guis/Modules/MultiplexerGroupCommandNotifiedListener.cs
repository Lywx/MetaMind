namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupCommandNotifiedListener : ListenerBase
    {
        private readonly IView commandView;
        private readonly IView moduleView;
        private readonly IView knowledgeView;

        public MultiplexerGroupCommandNotifiedListener(IView commandView, IView moduleView, IView knowledgeView)
        {
            this.commandView   = commandView;
            this.knowledgeView = knowledgeView;
            this.moduleView    = moduleView;

            this.RegisteredEvents.Add((int)SessionEventType.CommandNotified);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var eventArgs = @event.Data as CommandNotifiedEventArgs;
            if (eventArgs != null)
            {
                var notifiedCommand = eventArgs.NotifiedCommand;

                this.commandView.Control.AddItem(notifiedCommand);
                this.commandView.Control.SortItems(PointViewSortMode.Name);

                // possibly unnecessary
                var notifiedItem = this.commandView.Items.Find(item => ReferenceEquals(item.ItemData, notifiedCommand));

                int id = notifiedItem.ItemControl.Id;

                this.commandView.Control.Selection.Select(id);
                this.commandView.Control.Scroll   .Zoom(id);

                // asynchronous speaking
                Acutance.Synthesizer.SpeakAsync(notifiedCommand.Name);
            }

            this.knowledgeView.Control.Selection.Clear();
            this.knowledgeView.Control.Region   .Clear();
            this.moduleView   .Control.Selection.Clear();
            this.moduleView   .Control.Region   .Clear();

            return true;
        }
    }
}