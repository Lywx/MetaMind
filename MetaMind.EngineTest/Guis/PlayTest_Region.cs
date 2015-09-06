namespace MetaMind.EngineTest.Guis
{
    using Engine.Guis.Controls.Regions;
    using Engine.Guis.Controls.Visuals;
    using Engine.Testers;
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PlayTest_Region : GameEntityModule<object>
    {
        private GameControllableEntityCollection<GameControllableEntity> control;
        private GameVisualEntityCollection<GameVisualEntity> visual;
        private Region region;

        public PlayTest_Region(object settings)
            : base(settings)
        {
            this.control = new GameControllableEntityCollection<GameControllableEntity>();
            this.visual  = new GameVisualEntityCollection<GameVisualEntity>();

            // Region Control
            this.region = new Region(new Rectangle(50, 50, 50, 50));
            this.control.Add(region);

            // Box Visual
            var box = new Box(() => region.Rectangle, () => Color.CornflowerBlue, () => true);
            this.visual.Add(box);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.control.Draw(graphics, time, alpha);
            this.visual .Draw(graphics, time, alpha);

            StateVisualTester.Draw(graphics, typeof(RegionState), this.region.States, this.region.Location.ToVector2(), 10, 10);
        }

        public override void Update(GameTime time)
        {
            this.visual .Update(time);
            this.control.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.control.UpdateInput(input, time);
        }
    }
}