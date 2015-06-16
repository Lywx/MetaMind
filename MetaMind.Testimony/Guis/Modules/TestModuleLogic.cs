namespace MetaMind.Testimony.Guis.Modules
{
    using System;
    using Microsoft.Xna.Framework;

    using Engine.Components.Inputs;
    using Engine.Guis;
    using Engine.Services;
    using Concepts.Synchronizations;
    using Concepts.Tests;
    using Scripting;

    public class TestModuleLogic : ModuleLogic<TestModule, TestModuleSettings, TestModuleLogic>
    {
        private ScriptSearcher scriptSearcher;

        private ScriptRunner scriptRunner;

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
            this.scriptSearcher = new ScriptSearcher();
            this.scriptRunner   = new ScriptRunner(this.scriptSearcher, this.testSession.FsiSession);
            this.scriptRunner.Search();

            this.synchronizationSession = new SynchronizationSession();
            this.synchronizationSession.StartSynchronization();

            base.LoadContent(interop);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            var keyboard = input.State.Keyboard;

            if (keyboard.IsActionTriggered(KeyboardActions.TestPause))
            {
                this.synchronizationSession.ToggleSynchronization();
                this.testSession.           ToggleNotification();
            }

            if (keyboard.IsActionTriggered(KeyboardActions.TestRerun))
            {
                this.test.Reset();
                this.scriptRunner.Rerun();
            }

            base.UpdateInput(input, time);
        }
    }
}