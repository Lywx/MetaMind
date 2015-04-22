namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Concepts.Tasks;

    using Microsoft.Xna.Framework;

    public class SynchronizationModule : Module<SynchronizationModuleSettings>
    {
        public ICognition Cognition { get; set; }
        public ISynchronization Synchronization { get; set; }

        private readonly SynchronizationMonitor monitor;

        private SynchronizationModuleSleepStartedEventListener    sleepStartedEventListener;

        private SynchronizationModuleSynchronizationStartListener synchronizationStartListener;

        private SynchronizationModuleSynchronizationStopListener  synchronizationStopListener;

        #region Constructors

        public SynchronizationModule(Cognition cognition, SynchronizationModuleSettings settings)
            : base(settings)
        {
            this.Cognition = cognition;
            this.Synchronization = cognition.Synchronization;

            // best close the mouse listener
            // which may casue severe mouse performance issues
            this.monitor = new SynchronizationMonitor(GameEngine.Service.Interop.Engine, this.Synchronization);
        }

        #endregion Constructors
        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            if (this.synchronizationStartListener == null || 
                this.synchronizationStopListener  == null || 
                this.sleepStartedEventListener    == null)
            {
                this.synchronizationStartListener = new SynchronizationModuleSynchronizationStartListener(this.Synchronization, this);

                this.synchronizationStopListener  = new SynchronizationModuleSynchronizationStopListener(this.Synchronization, this);
                this.sleepStartedEventListener    = new SynchronizationModuleSleepStartedEventListener(this.Synchronization, this);
            }

            interop.Event.AddListener(this.synchronizationStartListener);
            interop.Event.AddListener(this.synchronizationStopListener);
            interop.Event.AddListener(this.sleepStartedEventListener);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            if (this.synchronizationStartListener != null)
            {
                interop.Event.RemoveListener(this.synchronizationStartListener);
            }

            if (this.synchronizationStopListener != null)
            {
                interop.Event.RemoveListener(this.synchronizationStopListener);
            }

            if (this.sleepStartedEventListener != null)
            {
                interop.Event.RemoveListener(this.sleepStartedEventListener);
            }

            this.synchronizationStartListener = null;
            this.synchronizationStopListener  = null;
            this.sleepStartedEventListener    = null;
        }

        #endregion

        #region Operations

        public void StartSynchronizing(Task target)
        {
            this.Synchronization.TryStart(target);
        }

        public void StopSynchronizing()
        {
            this.Synchronization.Stop();
            this.monitor        .Stop();
        }

        #endregion Operations

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.ForceAwake))
            {
                var awake = this.Cognition.Consciousness as ConsciousnessAwake;
                if (awake != null)
                {
                    awake.AwakeNow();
                }

                this.Synchronization.ResetToday();
            }

            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.ForceReset))
            {
                this.Synchronization.ResetTomorrow();
            }

            if (input.State.Keyboard.IsActionTriggered(KeyboardActions.ForceReverse))
            {
                this.Synchronization.TryAbort();
            }
        }

        public override void Update(GameTime time)
        {
            this.monitor.TryStart();
        }

        #endregion
    }
}