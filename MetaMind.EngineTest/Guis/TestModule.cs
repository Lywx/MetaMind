namespace MetaMind.EngineTest.Guis
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Testers;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class TestModule : Module<object>
    {
        private Region region;

        public TestModule(object settings)
            : base(settings)
        {
            this.Entities = new GameControllableEntityCollection<GameControllableEntity>();
            
            this.region = new Region(new Rectangle(50, 50, 50, 50));
            
            this.Entities.Add(this.region);
        }

        private GameControllableEntityCollection<GameControllableEntity> Entities { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            StateVisualTester.Draw(graphics, this.region.States, typeof(RegionState), new Point(500, 50), 10, 50);

            Primitives2D.FillRectangle(graphics.SpriteBatch, this.region.Rectangle, Color.White);
        }

        public override void Update(GameTime time)
        {
            this.Entities.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Entities.UpdateInput(input, time);
        }
    }
}