namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Components.Input;
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Engine.Components.Input;
    using Engine.Components.Interop.Event;
    using Engine.Entities;
    using Microsoft.Xna.Framework;
    using Screens;
    using Sessions;

    public class SummaryModuleController : MMMVCEntityController<SummaryModule, SummarySettings, SummaryModuleController>
    {
        public SummaryModuleController(SummaryModule module, IConsciousness consciousness, ISynchronization synchronization)
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
        }

        private IConsciousness Consciousness { get; set; }

        private ISynchronization Synchronization { get; set; }


        public override void LoadContent()
        {
            this.Listeners.Add(new SleepStoppedListener());

            base.LoadContent();
        }

        public override void UpdateInput(GameTime time)
        {
            var keyboard = this.Input.State.Keyboard;

            if (keyboard.IsActionTriggered(KeyboardActions.ConsciousnessAwaken))
            {
                this.Consciousness.Awaken();
            }

            if (keyboard.IsActionTriggered(KeyboardActions.ConsciousnessSleep))
            {
                this.Consciousness.Sleep();
            }
        }

        private class SleepStoppedListener : MMEventListener
        {
            public SleepStoppedListener()
            {
                this.RegisteredEvents.Add((int)SessionEvent.SleepStopped);
            }

            public override bool HandleEvent(IMMEvent @event)
            {
                var screenManager = this.Interop.Screen;

                screenManager.ExitScreenFrom(1);

                // Remove screens on the background screen
                screenManager.AddScreen(new MainScreen());

                return true;
            }
        }
    }
}