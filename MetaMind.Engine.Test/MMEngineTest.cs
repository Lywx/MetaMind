namespace MetaMind.EngineTest
{
    using Engine;
    using Engine.Core;
    using Engine.Core.Services.Debug.Components;
    using Guis;

    public class MMEngineTest : MMGame
    {
        public MMEngineTest(MMEngine engine)
            : base(engine)
        {
            this.Engine.Graphics.Settings.Fps = 30;
            
            this.Engine.Components.Add(new FrameRateCounter(this.Engine));
        }

        public override void Initialize()
        {
            base.Initialize();

            this.GlobalInterop.Screen.AddScreen(new PlayTest_Screen());
        }
    }
}
