namespace MetaMind.Testimony.Guis.Modules
{
    using System;
    using System.Linq;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Testimony.Concepts.Cognitions;
    using MetaMind.Testimony.Concepts.Synchronizations;
    using MetaMind.Testimony.Screens;
    using MetaMind.Testimony.Sessions;

    using Microsoft.Xna.Framework;

    public class SummaryModuleLogicControl : ModuleLogicControl<SummaryModule, SummarySettings, SummaryModuleLogicControl>
    {
        public SummaryModuleLogicControl(SummaryModule module, IConsciousness consciousness, ISynchronization synchronization)
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

            if (keyboard.IsActionTriggered(KeyboardActions.Awaken))
            {
                this.Consciousness.Awaken();
            }

            if (keyboard.IsActionTriggered(KeyboardActions.Sleep))
            {
                this.Consciousness.Sleep();
            }
        }

        private class SleepStoppedListener : Listener
        {
            public SleepStoppedListener()
            {
                this.RegisteredEvents.Add((int)SessionEventType.SleepStopped);
            }

            public override bool HandleEvent(IEvent @event)
            {
                var screenManager = this.GameInterop.Screen;

                var summary = screenManager.Screens.First(screen => screen is SummaryScreen);
                if (summary != null)
                {
                    summary.Exit();
                }

                screenManager.AddScreen(new MotivationScreen());

                return true;
            }
        }
    }
}