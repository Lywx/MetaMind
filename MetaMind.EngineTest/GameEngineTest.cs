namespace MetaMind.EngineTest
{
    using Engine;
    using Engine.Service.Debugging.Components;
    using Guis;

    public class GameEngineTest : Game
    {
        public GameEngineTest(GameEngine engine)
            : base(engine)
        {
            this.Engine.Graphics.Settings.FPS = 30;
            
            this.Engine.Components.Add(new FrameRateCounter(this.Engine));
        }

        public override void Initialize()
        {
            base.Initialize();

            this.Interop.Screen.AddScreen(new PlayTest_Screen());
        }
    }
}
