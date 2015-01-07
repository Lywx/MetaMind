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
            var createdEventArgs = @event.Data as CommandCreatedEventArgs;
            if (createdEventArgs != null)
            {
                this.commandView.Control.AddItem(createdEventArgs.Name, createdEventArgs.Path);
            }

            return true;
        }
    }
}