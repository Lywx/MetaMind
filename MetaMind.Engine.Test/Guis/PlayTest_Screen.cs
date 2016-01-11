namespace MetaMind.EngineTest.Guis
{
    using Engine;
    using Engine.Entities;
    using Engine.Entities.Bases;
    using Engine.Entities.Screens;
    using Engine.Services;

    public class PlayTest_Screen : MMScreen
    {
        private MMEntityCollection<MMInputEntity> tests;

        public override void LoadContent()
        {
            this.tests = new MMEntityCollection<MMInputEntity>();

            var region = new PlayTest_Region(null);
            var frame  = new PlayTest_Frame(null);

            this.tests.Add(region);
            this.tests.Add(frame);

            this.Layers.Add(
                new MMLayer(this)
                {
                    DrawAction = (graphics, time, alpha) =>
                    {
                        graphics.SpriteBatch.Begin();
                        this.tests.Draw(time);
                        graphics.SpriteBatch.End();
                    },
                    UpdateAction = time =>
                    {
                        this.tests.Update(time);
                    },
                    UpdateInputAction = (input, time) =>
                    {
                        this.tests.UpdateInput(time);
                    }
                });

            base.LoadContent();
        }
    }
}
