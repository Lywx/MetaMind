namespace MetaMind.EngineTest
{
    using MetaMind.Engine;
    using MetaMind.EngineTest.Guis;

    public class Test : Game
    {
        public Test(GameEngine engine)
            : base(engine)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            this.Interop.Screen.AddScreen(new TestScreen());
        }
    }
}
