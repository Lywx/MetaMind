namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Engine;
    using Engine.Components.Events;
    using Engine.Components.Inputs;
    using Engine.Guis;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Screens;
    using Sessions;

    public class SummaryModuleLogic : GameEntityModuleLogic<SummaryModule, SummarySettings, SummaryModuleLogic>
    {
        public SummaryModuleLogic(SummaryModule module, IConsciousness consciousness, ISynchronization synchronization)
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
        }

        private IConsciousness Consciousness { get; set; }

        private ISynchronization Synchronization { get; set; }


        public override void LoadContent(IGameInteropService interop)
        {
            this.Listeners.Add(new SleepStoppedListener());

            base.LoadContent(interop);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            var keyboard = input.State.Keyboard;

            if (keyboard.IsActionTriggered(KeyboardActions.ConsciousnessAwaken))
            {
                this.Consciousness.Awaken();
            }

            if (keyboard.IsActionTriggered(KeyboardActions.ConsciousnessSleep))
            {
                this.Consciousness.Sleep();
            }
        }

        private class SleepStoppedListener : Listener
        {
            public SleepStoppedListener()
            {
                this.RegisteredEvents.Add((int)SessionEvent.SleepStopped);
            }

            public override bool HandleEvent(IEvent @event)
            {
                var screenManager = this.Interop.Screen;

                screenManager.EraseScreenFrom(1);

                // Remove screens on the background screen
                screenManager.AddScreen(new MainScreen());

                return true;
            }
        }
    }
}