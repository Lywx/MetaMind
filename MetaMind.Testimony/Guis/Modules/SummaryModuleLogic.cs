﻿namespace MetaMind.Testimony.Guis.Modules
{
    using System;
    using System.Linq;

    using Engine.Components.Events;
    using Engine.Components.Inputs;
    using Engine.Guis;
    using Engine.Services;
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Sessions;

    using Microsoft.Xna.Framework;
    using Screens;

    public class SummaryModuleLogic : ModuleLogic<SummaryModule, SummarySettings, SummaryModuleLogic>
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
                this.RegisteredEvents.Add((int)SessionEventType.SleepStopped);
            }

            public override bool HandleEvent(IEvent @event)
            {
                var screenManager = this.GameInterop.Screen;

                screenManager.EraseScreenFrom(1);

                // Remove screens on the background screen
                screenManager.AddScreen(new MainScreen());

                return true;
            }
        }
    }
}