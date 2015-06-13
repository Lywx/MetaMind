namespace MetaMind.Testimony.Guis.Modules
{
    using Microsoft.Xna.Framework;

    using Engine.Guis;
    using Engine.Services;

    public class TestModuleVisual : ModuleVisual<TestModule, TestModuleSettings, TestModuleLogic>
    {
        public TestModuleVisual(TestModule module) : base(module)
        {
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Module.Entities.Draw(graphics, time, alpha);
        }
    }
}