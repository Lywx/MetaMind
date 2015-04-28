namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupCommandNotifiedListener : Listener
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

        public override bool HandleEvent(IEvent e)
        {
            var eventArgs = e.EventData as CommandNotifiedEventArgs;
            if (eventArgs != null)
            {
                var notifiedCommand = eventArgs.NotifiedCommandCommand;

                this.commandView.Logic.AddItem(notifiedCommand);
                this.commandView.Logic.SortItems(PointViewSortMode.Name);

                // possibly unnecessary
                var notifiedItem = this.commandView.Items.Find(item => ReferenceEquals(item.ItemData, notifiedCommand));

                int id = notifiedItem.ItemLogic.Id;

                this.commandView.Logic.Selection.Select(id);
                this.commandView.Logic.Scroll   .Zoom(id);

                // asynchronous speaking
                Acutance.Synthesizer.SpeakAsync(notifiedCommand.Name);
            }

            this.knowledgeView.Logic.Selection.Clear();
            this.knowledgeView.Logic.Region   .Clear();
            this.moduleView   .Logic.Selection.Clear();
            this.moduleView   .Logic.Region   .Clear();

            return true;
        }
    }
}