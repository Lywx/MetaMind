﻿namespace MetaMind.Runtime.Guis.Modules
{
    using System.Linq;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Screens;
    using MetaMind.Runtime.Sessions;

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

        public override bool HandleEvent(IEvent @event)
        {
            if (this.synchronization.Enabled)
            {
                this.synchronizationModule.StopSynchronizing();
            }

            this.synchronization.ResetTomorrow();

            var screenManager = this.GameInterop.Screen;

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