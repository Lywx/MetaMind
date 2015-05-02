namespace MetaMind.EngineTest
{
    using MetaMind.Engine;
    using MetaMind.EngineTest.Guis;

    public class GameEngineTest : Game
    {
        public GameEngineTest(GameEngine engine)
            : base(engine)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            this.Interop.Screen.AddScreen(new PlayTest_Screen());
        }
    }
}
