namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Components.Input;
    using Engine.Components.Input;
    using Engine.Components.Interop.Event;
    using Engine.Entities;
    using Microsoft.Xna.Framework;
    using Runtime;
    using Runtime.Attention;
    using Screens;
    using Sessions;

    public class SummaryModuleController : MMMVCEntityController<SummaryModule, SummarySettings, SummaryModuleController>
    {
        public SummaryModuleController(SummaryModule module, IConsciousness consciousness, ISynchronizationData synchronizationData)
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
        }

        private IConsciousness Consciousness { get; set; }

        private ISynchronizationData SynchronizationData { get; set; }


        public override void LoadContent()
        {
            this.EntityListeners.Add(new SleepStoppedListener());

            base.LoadContent();
        }

        public override void UpdateInput(GameTime time)
        {
            var keyboard = this.GlobalInput.State.Keyboard;

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
                var screenManager = this.GlobalInterop.Screen;

                screenManager.ExitScreenFrom(1);

                // Remove screens on the background screen
                screenManager.AddScreen(new MainScreen());

                return true;
            }
        }
    }
}