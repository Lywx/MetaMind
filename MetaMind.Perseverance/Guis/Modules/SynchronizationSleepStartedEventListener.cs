namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Linq;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    public class SynchronizationModuleSleepStartedEventListener : Listener
    {
        private readonly ISynchronization synchronization;
        private readonly SynchronizationModule synchronizationModule;
        
        public SynchronizationModuleSleepStartedEventListener(ISynchronization synchronization, SynchronizationModule synchronizationModule )
        {
            this.synchronization       = synchronization;
            this.synchronizationModule = synchronizationModule;

            this.RegisteredEvents.Add((int)SessionEventType.SleepStarted);
        }

        public override bool HandleEvent(Event @event)
        {
            if (this.synchronization.Enabled)
            {
                this.synchronizationModule.StopSynchronizing();
            }

            this.synchronization.ResetTomorrow();

            var screenManager = GameEngine.ScreenManager;

            var motivation = screenManager.Screens.First(screen => screen is MotivationScreen);
            if (motivation != null)
            {
                motivation.Exit();
            }

            screenManager.AddScreen(new SummaryScreen());

            return true;
        }
    }
}