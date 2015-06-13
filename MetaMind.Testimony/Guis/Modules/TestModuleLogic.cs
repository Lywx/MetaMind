namespace MetaMind.Testimony.Guis.Modules
{
    using Microsoft.Xna.Framework;

    using Engine.Components.Inputs;
    using Engine.Guis;
    using Engine.Services;
    using Concepts.Synchronizations;
    using Scripting;

    public class TestModuleLogic : ModuleLogic<TestModule, TestModuleSettings, TestModuleLogic>
    {
        private ScriptSearcher scriptSearcher;

        private ScriptRunner scriptRunner;

        private SynchronizationController synchronizationController;

        public TestModuleLogic(TestModule module) 
            : base(module)
        {
        }

        public override void LoadContent(IGameInteropService interop)
        {
            this.scriptSearcher = new ScriptSearcher();
            this.scriptRunner   = new ScriptRunner(this.scriptSearcher);
            this.scriptRunner.Search();

            this.synchronizationController = new SynchronizationController();
            this.synchronizationController.StartSynchronization();

            base.LoadContent(interop);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            var keyboard = input.State.Keyboard;

            if (keyboard.IsActionTriggered(KeyboardActions.TestPause))
            {
                this.synchronizationController.ToggleSynchronization();
            }

            if (keyboard.IsActionTriggered(KeyboardActions.TestRerun))
            {
                this.scriptRunner.Rerun();
            }

            base.UpdateInput(input, time);
        }
    }
}