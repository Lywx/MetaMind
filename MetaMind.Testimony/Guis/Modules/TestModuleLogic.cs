namespace MetaMind.Testimony.Guis.Modules
{
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

        private TestSession testSession;

        public TestModuleLogic(TestModule module) 
            : base(module)
        {
        }

        public override void LoadContent(IGameInteropService interop)
        {
            this.scriptSearcher = new ScriptSearcher();
            this.scriptRunner   = new ScriptRunner(this.scriptSearcher);
            this.scriptRunner.Search();

            this.synchronizationSession = new SynchronizationSession();
            this.synchronizationSession.StartSynchronization();

            this.testSession = new TestSession();
            Test.TestSession = this.testSession;

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
                this.Module.Test.Reset();
                this.scriptRunner.Rerun();
            }

            base.UpdateInput(input, time);
        }
    }
}