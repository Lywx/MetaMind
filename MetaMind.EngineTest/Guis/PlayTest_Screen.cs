namespace MetaMind.EngineTest.Guis
{
    using MetaMind.Engine;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PlayTest_Screen : GameScreen
    {
        private GameControllableEntityCollection<GameControllableEntity> tests;

        public override void LoadContent(IGameInteropService interop)
        {
            this.tests = new GameControllableEntityCollection<GameControllableEntity>();

            var region = new PlayTest_Region(null);
            var frame = new PlayTest_Frame(null);
            //tests.Add(region);
            tests.Add(frame);

            base.LoadContent(interop);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.tests.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.tests.Update(time);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            var spriteBatch = graphics.SpriteBatch;

            spriteBatch.Begin();
            
            this.tests.Draw(graphics, time, this.TransitionAlpha);
            
            spriteBatch.End();
        }
    }
}
