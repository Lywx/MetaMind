namespace MetaMind.Acutance.Events
{
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupCommandCreatedListener : ListenerBase
    {
        private readonly IView commandView;

        public MultiplexerGroupCommandCreatedListener(IView commandView)
        {
            this.commandView = commandView;

            this.RegisteredEvents.Add((int)SessionEventType.CommandCreated);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var eventArgs = @event.Data as CommandCreatedEventArgs;
            if (eventArgs != null)
            {
                this.commandView.Control.AddItem(eventArgs.Name, eventArgs.Path);
            }

            return true;
        }
    }
}