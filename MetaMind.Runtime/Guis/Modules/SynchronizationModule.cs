namespace MetaMind.Runtime.Guis.Modules
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Concepts.Cognitions;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Guis.Modules.Synchronization;

    using Microsoft.Xna.Framework;

    namespace Synchronization
    {
        using System.Linq;

        using MetaMind.Engine.Components.Events;
        using MetaMind.Runtime.Screens;
        using MetaMind.Runtime.Sessions;

        public class SynchronizationModuleSleepStartedEventListener : Listener
        {
            private readonly ISynchronization synchronization;

            private readonly SynchronizationModule synchronizationModule;

            public SynchronizationModuleSleepStartedEventListener(
                ISynchronization synchronization,
                SynchronizationModule synchronizationModule)
            {
                this.synchronization = synchronization;
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

    public class SynchronizationModule : Module<SynchronizationSettings>
    {
        private readonly SynchronizationMonitor monitor;

        private IConsciousness Consciousness { get; set; }

        private ISynchronization Synchronization { get; set; }

        #region Constructors

        public SynchronizationModule(IConsciousness consciousness, ISynchronization synchronization, SynchronizationSettings settings)
            : base(settings)
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

            this.Control  = new SynchronizationModuleControl(this);
            this.Graphics = new SynchronizationModuleGraphics(this, this.Consciousness, this.Synchronization);

            this.monitor = new SynchronizationMonitor(this.Synchronization);
        }

        #endregion Constructors

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            this.Listeners.Add(new SynchronizationModuleSynchronizationStartListener(this.Synchronization)); 
            this.Listeners.Add(new SynchronizationModuleSynchronizationStopListener(this.Synchronization, this));
            this.Listeners.Add(new SynchronizationModuleSleepStartedEventListener(this.Synchronization, this));

            base.LoadContent(interop);
        }

        #endregion

        #region Operations

        public void StopSynchronizing()
        {
            this.Synchronization.Stop();
            this.monitor        .Stop();
        }

        #endregion Operations

        #region Update

        public override void Update(GameTime time)
        {
            this.monitor.TryStart();

            // UNDONE: Won't work
            this.monitor.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Awaken))
            {
                // TODO: integrate
                this.Consciousness.Awaken();
                this.Synchronization.ResetToday();
            }

            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Sleep))
            {
                this.Consciousness.Sleep();
            }
        }
        #endregion
    }
}