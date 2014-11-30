﻿namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Linq;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    public class SynchronizationHudSleepStartedEventListener : ListenerBase
    {
        private readonly ISynchronization synchronization;
        private readonly SynchronizationModule synchronizationModule;
        
        public SynchronizationHudSleepStartedEventListener(ISynchronization synchronization, SynchronizationModule synchronizationModule )
        {
            this.synchronization    = synchronization;
            this.synchronizationModule = synchronizationModule;

            this.RegisteredEvents.Add((int)AdventureEventType.SleepStarted);
        }

        public override bool HandleEvent(EventBase @event)
        {
            if (this.synchronization.Enabled)
            {
                this.synchronizationModule.StopSynchronizing();
            }

            this.synchronization.ResetForTomorrow();

            var screenManager = GameEngine.ScreenManager;

            var motivation = screenManager.Screens.First(screen => screen is MotivationScreen);
            if (motivation != null)
            {
                motivation.ExitScreen();
            }

            screenManager.AddScreen(new SummaryScreen());

            return true;
        }
    }
}