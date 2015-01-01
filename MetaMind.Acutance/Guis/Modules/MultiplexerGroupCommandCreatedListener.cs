namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupCommandCreatedListener : ListenerBase
    {
        private readonly IView commandView;

        public MultiplexerGroupCommandCreatedListener(IView commandView)
        {
            this.commandView = commandView;

            this.RegisteredEvents.Add((int)AdventureEventType.CommandCreated);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var createdEventArgs = @event.Data as CommandCreatedEventArgs;
            if (createdEventArgs != null)
            {
                this.commandView.Control.AddItem(createdEventArgs.Name, createdEventArgs.Path, createdEventArgs.Minutes);
            }

            return true;
        }
    }
}