namespace MetaMind.EngineTest
{
    using Engine;
    using Engine.Debugging;
    using Engine.Screens;
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
