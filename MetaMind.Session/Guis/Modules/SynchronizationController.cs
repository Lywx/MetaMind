namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Components.Input;
    using Engine.Components.Input;
    using Engine.Components.Interop.Event;
    using Engine.Entities;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Runtime;
    using Runtime.Attention;
    using Screens;
    using Session.Sessions;

    public class SynchronizationController : MMMVCEntityController<SynchronizationModule, SynchronizationSettings, SynchronizationController>
    {
        public SynchronizationController(SynchronizationModule module, IConsciousness consciousness, ISynchronizationData synchronizationData)
            : base(module)
        {
            if (consciousness == null)
            {
                throw new ArgumentNullException(nameof(consciousness));
            }

            if (synchronizationData == null)
            {
                throw new ArgumentNullException(nameof(synchronizationData));
            }

            this.Consciousness   = consciousness;
            this.SynchronizationData = synchronizationData;

            this.SynchronizationMonitor = new SynchronizationMonitor(this.GlobalInterop.Engine, this.SynchronizationData);
        }

        private SynchronizationMonitor SynchronizationMonitor { get; set; }

        private IConsciousness Consciousness { get; set; }

        private ISynchronizationData SynchronizationData { get; set; }

        public override void LoadContent()
        {
            this.EntityListeners.Add(new SleepStartedListener(this.SynchronizationData, this));
            this.EntityListeners.Add(new SleepStoppedListener(this.SynchronizationData));

            this.EntityListeners.Add(new SynchronizationStartedListener(this.SynchronizationData));
            this.EntityListeners.Add(new SynchronizationStoppedListener(this.SynchronizationData, this));

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
                this.SynchronizationData.TryAbort();
            }
        }

        #region Operations

        public void Stop()
        {
            this.SynchronizationData       .Stop();
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
            private readonly ISynchronizationData synchronizationData;

            private readonly SynchronizationController synchronizationController;

            public SleepStartedListener(ISynchronizationData synchronizationData, SynchronizationController synchronizationController)
            {
                this.synchronizationData        = synchronizationData;
                this.synchronizationController = synchronizationController;

                this.RegisteredEvents.Add((int)SessionEvent.SleepStarted);
            }

            public override bool HandleEvent(IMMEvent @event)
            {
                // Stop synchronization
                if (this.synchronizationData.Enabled)
                {
                    this.synchronizationController.Stop();
                }

                // Reset statistics
                this.synchronizationData.ResetTomorrow();

                // Quit monitoring 
                this.synchronizationController.Exit();

                var screenManager = this.GlobalInterop.Screen;

                // Remove screens on the background screen
                screenManager.ExitScreenFrom(1); 
                screenManager.AddScreen(new SummaryScreen());

                return true;
            }
        }

        private class SleepStoppedListener : MMEventListener
        {
            private readonly ISynchronizationData synchronizationData;

            public SleepStoppedListener(ISynchronizationData synchronizationData)
            {
                this.synchronizationData = synchronizationData;

                this.RegisteredEvents.Add((int)SessionEvent.SleepStopped);
            }

            public override bool HandleEvent(IMMEvent e)
            {
                this.synchronizationData.ResetToday();

                return true;
            }
        }

        private class SynchronizationStartedListener : MMEventListener
        {
            public SynchronizationStartedListener(ISynchronizationData synchronizationData)
            {
                if (synchronizationData == null)
                {
                    throw new ArgumentNullException("synchronizationData");
                }

                this.SynchronizationData = synchronizationData;

                this.RegisteredEvents.Add((int)SessionEvent.SyncStarted);
            }

            private ISynchronizationData SynchronizationData { get; set; }

            public override bool HandleEvent(IMMEvent e)
            {
                var synchronizationStartedEventArgs = (SynchronizationStartedEventArgs)e.Data;
                var data = synchronizationStartedEventArgs.Data;

                // uncomment this to enforce fixed entry start/stop
                //// if (Synchronization.IsEnabled) return true;

                this.SynchronizationData.TryBegin(data);

                return true;
            }
        }

        private class SynchronizationStoppedListener : MMEventListener
        {
            private readonly ISynchronizationData synchronizationData;

            private readonly SynchronizationController synchronizationController;

            public SynchronizationStoppedListener(ISynchronizationData synchronizationData, SynchronizationController synchronizationController)
            {
                this.synchronizationData      = synchronizationData;
                this.synchronizationController = synchronizationController;

                this.RegisteredEvents.Add((int)SessionEvent.SyncStopped);
            }

            public override bool HandleEvent(IMMEvent @event)
            {
                if (!this.synchronizationData.Enabled)
                {
                    return true;
                }

                this.synchronizationController.Stop();

                return true;
            }
        }
    }
}