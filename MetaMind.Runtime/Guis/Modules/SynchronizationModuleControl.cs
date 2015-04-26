namespace MetaMind.Runtime.Guis.Modules
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Concepts.Cognitions;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Events;
    using MetaMind.Runtime.Screens;
    using MetaMind.Runtime.Sessions;

    using Microsoft.Xna.Framework;

    public class SynchronizationModuleControl : ModuleControl<SynchronizationModule, SynchronizationSettings, SynchronizationModuleControl>
    {
        public SynchronizationModuleControl(SynchronizationModule module, IConsciousness consciousness, ISynchronization synchronization)
            : base(module)
        {
            if (consciousness == null)
            {
                throw new ArgumentNullException("consciousness");
            }

            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            this.Consciousness   = consciousness;
            this.Synchronization = synchronization;

            this.Monitor = new SynchronizationMonitor(this.GameInterop.Engine, this.Synchronization);
        }

        private SynchronizationMonitor Monitor { get; set; }

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
            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Awaken))
            {
                // This trigger synchronization listener to reset today
                this.Consciousness.Awaken();
            }

            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Sleep))
            {
                this.Consciousness.Sleep();
            }
        }

        #region Operations

        public void Stop()
        {
            this.Synchronization.Stop();
            this.Monitor        .Stop();
        }

        public void Exit()
        {
            this.Monitor.Exit();
        }

        #endregion Operations

        #region Update

        public override void Update(GameTime time)
        {
            // Automatic monitoring
            this.Monitor.TryStart();

            this.Monitor.Update(time);
        }
        #endregion

        private class SleepStartedListener : Listener
        {
            private readonly ISynchronization synchronization;

            private readonly SynchronizationModuleControl synchronizationControl;

            public SleepStartedListener(ISynchronization synchronization, SynchronizationModuleControl synchronizationControl)
            {
                this.synchronization        = synchronization;
                this.synchronizationControl = synchronizationControl;

                this.RegisteredEvents.Add((int)SessionEventType.SleepStarted);
            }

            public override bool HandleEvent(IEvent @event)
            {
                // Stop synchronization
                if (this.synchronization.Enabled)
                {
                    this.synchronizationControl.Stop();
                }

                // Reset statistics
                this.synchronization.ResetTomorrow();

                // Quit monitoring 
                this.synchronizationControl.Exit();

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

        private class SleepStoppedListener : Listener
        {
            private readonly ISynchronization synchronization;

            public SleepStoppedListener(ISynchronization synchronization)
            {
                this.synchronization = synchronization;

                this.RegisteredEvents.Add((int)SessionEventType.SleepStopped);
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

                this.RegisteredEvents.Add((int)SessionEventType.SyncStarted);
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

            private readonly SynchronizationModuleControl synchronizationControl;

            public SynchronizationStoppedListener(ISynchronization synchronization, SynchronizationModuleControl synchronizationControl)
            {
                this.synchronization        = synchronization;
                this.synchronizationControl = synchronizationControl;

                this.RegisteredEvents.Add((int)SessionEventType.SyncStopped);
            }

            public override bool HandleEvent(IEvent @event)
            {
                if (!this.synchronization.Enabled)
                {
                    return true;
                }

                this.synchronizationControl.Stop();

                return true;
            }
        }
    }
}