namespace MetaMind.EngineTest
{
    using Engine;
    using Engine.Services.Debug.Components;
    using Guis;

    public class MMEngineTest : MMGame
    {
        public MMEngineTest(MMEngine engine)
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
