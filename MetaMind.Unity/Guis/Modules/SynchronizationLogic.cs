namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Engine;
    using Engine.Components.Input;
    using Engine.Components.Interop.Event;
    using Engine.Gui.Modules;
    using Engine.Service;
    using Events;
    using Microsoft.Xna.Framework;
    using Screens;
    using Sessions;

    public class SynchronizationLogic : GameMvcEntityLogic<SynchronizationModule, SynchronizationSettings, SynchronizationLogic>
    {
        public SynchronizationLogic(SynchronizationModule module, IConsciousness consciousness, ISynchronization synchronization)
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

        public override void LoadContent(IGameInteropService interop)
        {
            this.Listeners.Add(new SleepStartedListener(this.Synchronization, this));
            this.Listeners.Add(new SleepStoppedListener(this.Synchronization));

            this.Listeners.Add(new SynchronizationStartedListener(this.Synchronization));
            this.Listeners.Add(new SynchronizationStoppedListener(this.Synchronization, this));

            base.LoadContent(interop);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
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

        private class SleepStartedListener : Listener
        {
            private readonly ISynchronization synchronization;

            private readonly SynchronizationLogic synchronizationLogic;

            public SleepStartedListener(ISynchronization synchronization, SynchronizationLogic synchronizationLogic)
            {
                this.synchronization        = synchronization;
                this.synchronizationLogic = synchronizationLogic;

                this.RegisteredEvents.Add((int)SessionEvent.SleepStarted);
            }

            public override bool HandleEvent(IEvent @event)
            {
                // Stop synchronization
                if (this.synchronization.Enabled)
                {
                    this.synchronizationLogic.Stop();
                }

                // Reset statistics
                this.synchronization.ResetTomorrow();

                // Quit monitoring 
                this.synchronizationLogic.Exit();

                var screenManager = this.Interop.Screen;

                // Remove screens on the background screen
                screenManager.EraseScreenFrom(1); 
                screenManager.AddScreen(new SummaryScreen());

                return true;
            }
        }

        private class SleepStoppedListener : Listener
        {
            private readonly ISynchronization synchronization;

            public SleepStoppedListener(ISynchronization synchronization)
            {
                this.synchronization = synchronization;

                this.RegisteredEvents.Add((int)SessionEvent.SleepStopped);
            }

            public override bool HandleEvent(IEvent e)
            {
                this.synchronization.ResetToday();

                return true;
            }
        }

        private class SynchronizationStartedListener : Listener
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

            public override bool HandleEvent(IEvent e)
            {
                var synchronizationStartedEventArgs = (SynchronizationStartedEventArgs)e.EventData;
                var data = synchronizationStartedEventArgs.Data;

                // uncomment this to enforce fixed entry start/stop
                //// if (Synchronization.IsEnabled) return true;

                this.Synchronization.TryStart(data);

                return true;
            }
        }

        private class SynchronizationStoppedListener : Listener
        {
            private readonly ISynchronization synchronization;

            private readonly SynchronizationLogic synchronizationLogic;

            public SynchronizationStoppedListener(ISynchronization synchronization, SynchronizationLogic synchronizationLogic)
            {
                this.synchronization      = synchronization;
                this.synchronizationLogic = synchronizationLogic;

                this.RegisteredEvents.Add((int)SessionEvent.SyncStopped);
            }

            public override bool HandleEvent(IEvent @event)
            {
                if (!this.synchronization.Enabled)
                {
                    return true;
                }

                this.synchronizationLogic.Stop();

                return true;
            }
        }
    }
}