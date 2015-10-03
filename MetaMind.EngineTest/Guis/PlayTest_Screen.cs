namespace MetaMind.EngineTest.Guis
{
    using Engine;
    using Engine.Entities;
    using Engine.Screen;
    using Engine.Service;

    public class PlayTest_Screen : MMScreen
    {
        private MMEntityCollection<MMInputEntity> tests;

        public override void LoadContent(IMMEngineInteropService interop)
        {
            this.tests = new MMEntityCollection<MMInputEntity>();

            var region = new PlayTest_Region(null);
            var frame  = new PlayTest_Frame(null);

            tests.Add(region);
            tests.Add(frame);

            this.Layers.Add(
                new MMLayer(this)
                {
                    DrawAction = (graphics, time, alpha) =>
                    {
                        graphics.SpriteBatch.Begin();
                        this.tests.Draw(graphics, time, alpha);
                        graphics.SpriteBatch.End();
                    },
                    UpdateAction = time =>
                    {
                        this.tests.Update(time);
                    },
                    UpdateInputAction = (input, time) =>
                    {
                        this.tests.UpdateInput(input, time);
                    }
                });

            base.LoadContent(interop);
        }
    }
}
