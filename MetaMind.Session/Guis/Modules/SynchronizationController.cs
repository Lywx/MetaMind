namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Components.Input;
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Engine.Components.Input;
    using Engine.Components.Interop.Event;
    using Engine.Entities;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Screens;
    using Session.Sessions;

    public class SynchronizationController : MMMVCEntityController<SynchronizationModule, SynchronizationSettings, SynchronizationController>
    {
        public SynchronizationController(SynchronizationModule module, IConsciousness consciousness, ISynchronization synchronization)
            : base(module)
        {
            if (consciousness == null)
            {
                throw new ArgumentNullException(nameof(consciousness));
            }

            if (synchronization == null)
            {
                throw new ArgumentNullException(nameof(synchronization));
            }

            this.Consciousness   = consciousness;
            this.Synchronization = synchronization;

            this.SynchronizationMonitor = new SynchronizationMonitor(this.Interop.Engine, this.Synchronization);
        }

        private SynchronizationMonitor SynchronizationMonitor { get; set; }

        private IConsciousness Consciousness { get; set; }

        private ISynchronization Synchronization { get; set; }

        public override void LoadContent()
        {
            this.Listeners.Add(new SleepStartedListener(this.Synchronization, this));
            this.Listeners.Add(new SleepStoppedListener(this.Synchronization));

            this.Listeners.Add(new SynchronizationStartedListener(this.Synchronization));
            this.Listeners.Add(new SynchronizationStoppedListener(this.Synchronization, this));

            base.LoadContent();
        }

        public override void UpdateInput(GameTime time)
        {
            var keyboard = input.State.Keyboard;

            // Consciousness
            if (keyboard.IsActionTriggered(KeyboardActions.ConsciousnessAwaken))
            {
                // This trigger synchronization listener to reset today
                this.Consciousness.Awaken();
            }

            if (keyboard.IsActionTriggered(KeyboardActions.ConsciousnessSleep))
            {
                this.Consciousness.Sleep();
            }

            // Reverse Synchronization 
            if (keyboard.IsActionTriggered(KeyboardActions.SynchronizationReverse))
            {
                this.Synchronization.TryAbort();
            }
        }

        #region Operations

        public void Stop()
        {
            this.Synchronization       .Stop();
            this.SynchronizationMonitor.Stop();
        }

        public void Exit()
        {
            this.SynchronizationMonitor.Exit();
        }

        #endregion Operations

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            // Automatic monitoring
            this.SynchronizationMonitor.TryStart();
        }

        #endregion

        private class SleepStartedListener : MMEventListener
        {
            private readonly ISynchronization synchronization;

            private readonly SynchronizationController synchronizationController;

            public SleepStartedListener(ISynchronization synchronization, SynchronizationController synchronizationController)
            {
                this.synchronization        = synchronization;
                this.synchronizationController = synchronizationController;

                this.RegisteredEvents.Add((int)SessionEvent.SleepStarted);
            }

            public override bool HandleEvent(IMMEvent @event)
            {
                // Stop synchronization
                if (this.synchronization.Enabled)
                {
                    this.synchronizationController.Stop();
                }

                // Reset statistics
                this.synchronization.ResetTomorrow();

                // Quit monitoring 
                this.synchronizationController.Exit();

                var screenManager = this.Interop.Screen;

                // Remove screens on the background screen
                screenManager.ExitScreenFrom(1); 
                screenManager.AddScreen(new SummaryScreen());

                return true;
            }
        }

        private class SleepStoppedListener : MMEventListener
        {
            private readonly ISynchronization synchronization;

            public SleepStoppedListener(ISynchronization synchronization)
            {
                this.synchronization = synchronization;

                this.RegisteredEvents.Add((int)SessionEvent.SleepStopped);
            }

            public override bool HandleEvent(IMMEvent e)
            {
                this.synchronization.ResetToday();

                return true;
            }
        }

        private class SynchronizationStartedListener : MMEventListener
        {
            public SynchronizationStartedListener(ISynchronization synchronization)
            {
                if (synchronization == null)
                {
                    throw new ArgumentNullException("synchronization");
                }

                this.Synchronization = synchronization;

                this.RegisteredEvents.Add((int)SessionEvent.SyncStarted);
            }

            private ISynchronization Synchronization { get; set; }

            public override bool HandleEvent(IMMEvent e)
            {
                var synchronizationStartedEventArgs = (SynchronizationStartedEventArgs)e.Data;
                var data = synchronizationStartedEventArgs.Data;

                // uncomment this to enforce fixed entry start/stop
                //// if (Synchronization.IsEnabled) return true;

                this.Synchronization.TryStart(data);

                return true;
            }
        }

        private class SynchronizationStoppedListener : MMEventListener
        {
            private readonly ISynchronization synchronization;

            private readonly SynchronizationController synchronizationController;

            public SynchronizationStoppedListener(ISynchronization synchronization, SynchronizationController synchronizationController)
            {
                this.synchronization      = synchronization;
                this.synchronizationController = synchronizationController;

                this.RegisteredEvents.Add((int)SessionEvent.SyncStopped);
            }

            public override bool HandleEvent(IMMEvent @event)
            {
                if (!this.synchronization.Enabled)
                {
                    return true;
                }

                this.synchronizationController.Stop();

                return true;
            }
        }
    }
}