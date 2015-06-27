namespace MetaMind.Testimony.Guis.Modules
{
    using System;
    using Microsoft.Xna.Framework;

    using Engine.Components.Inputs;
    using Engine.Guis;
    using Engine.Services;
    using Concepts.Synchronizations;
    using Concepts.Tests;

    public class TestModuleLogic : ModuleLogic<TestModule, TestModuleSettings, TestModuleLogic>
    {
        private SynchronizationSession synchronizationSession;

        private readonly ITest test;

        private readonly TestSession testSession;

        public TestModuleLogic(TestModule module, ITest test, TestSession testSession) 
            : base(module)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            if (testSession == null)
            {
                throw new ArgumentNullException("testSession");
            }

            this.test        = test;
            this.testSession = testSession;
        }

        public override void LoadContent(IGameInteropService interop)
        {
            this.synchronizationSession = new SynchronizationSession();
            this.synchronizationSession.StartSynchronization();

            base.LoadContent(interop);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            var keyboard = input.State.Keyboard;

            if (keyboard.IsActionTriggered(KeyboardActions.SynchronizationPause))
            {
                this.synchronizationSession.ToggleSynchronization();
                this.testSession.           ToggleNotification();
            }

            base.UpdateInput(input, time);
        }
    }
}