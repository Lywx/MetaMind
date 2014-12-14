namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MultiplexerGroupCallCreatedListener : ListenerBase
    {
        private readonly IView callView;

        public MultiplexerGroupCallCreatedListener(IView callView)
        {
            this.callView = callView;

            this.RegisteredEvents.Add((int)AdventureEventType.CallCreated);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var createdEventArgs = @event.Data as CallCreatedEventArgs;
            if (createdEventArgs != null)
            {
                this.callView.Control.AddItem(createdEventArgs.Name, createdEventArgs.Path, createdEventArgs.Minutes);
            }

            return true;
        }
    }
}