namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Concepts.Tasks;
    using MetaMind.Perseverance.Guis.Modules.Synchronization;

    using Microsoft.Xna.Framework;

    public class SynchronizationModule : Module<SynchronizationModuleSettings>
    {
        public IConsciousness Consciousness { get; set; }

        public ISynchronization Synchronization { get; set; }


        private readonly SynchronizationMonitor monitor;

        private SynchronizationModuleSleepStartedEventListener    sleepStartedEventListener;

        private SynchronizationModuleSynchronizationStartListener synchronizationStartListener;

        private SynchronizationModuleSynchronizationStopListener  synchronizationStopListener;

        #region Constructors

        public SynchronizationModule(IConsciousness consciousness, ISynchronization synchronization, SynchronizationModuleSettings settings)
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

            this.Consciousness       = consciousness;
            this.Synchronization = synchronization;

            this.Control = new SynchronizationModuleControl(this);
            this.Graphics = new SynchronizationModuleGraphics(this, this.Synchronization);

            // best close the mouse listener
            // which may casue severe mouse performance issues
            this.monitor = new SynchronizationMonitor(this.Synchronization);
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

        public override void Update(GameTime time)
        {
            this.monitor.TryStart();
        }

        #endregion
    }
}