namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Sessions;

    public class MotivationModuleGameStartedListener : ListenerBase
    {
        private readonly IView startup;

        public MotivationModuleGameStartedListener(IView startup)
        {
            this.startup = startup;

            this.RegisteredEvents.Add((int)SessionEventType.GameStarted);
        }

        public override bool HandleEvent(EventBase @event)
        {
            // auto-select after startup
            this.startup.Control.Selection.Select(0);

            return true;
        }
    }
}