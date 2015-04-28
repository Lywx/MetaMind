namespace MetaMind.EngineTest.Guis
{
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class TestScreen : GameScreen
    {
        private TestModule test;

        public override void LoadContent(IGameInteropService interop)
        {
            this.test = new TestModule(null);

            base.LoadContent(interop);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.test.UpdateInput(input, time);
        }

        public override void Update(GameTime gameTime)
        {
            this.test.Update(gameTime);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            var spriteBatch = graphics.SpriteBatch;

            spriteBatch.Begin();
            
            this.test.Draw(graphics, time, this.TransitionAlpha);
            
            spriteBatch.End();
        }
    }
}
