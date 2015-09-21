namespace MetaMind.EngineTest.Guis
{
    using Engine.Gui.Control.Regions;
    using Engine.Gui.Control.Visuals;
    using Engine.Service;
    using Engine.Test;
    using Engine;
    using Microsoft.Xna.Framework;

    public class PlayTest_Region : GameEntityModule<object>
    {
        private GameEntityCollection<GameControllableEntity> control;
        private GameEntityCollection<GameVisualEntity> visual;
        private RectangleRegion region;

        public PlayTest_Region(object settings)
            : base(settings)
        {
            this.control = new GameEntityCollection<GameControllableEntity>();
            this.visual  = new GameEntityCollection<GameVisualEntity>();

            // Region Control
            this.region = new RectangleRegion(new Rectangle(50, 50, 50, 50));
            this.control.Add(region);

            // Box Visual
            var box = new Box(() => region.Rectangle, () => Color.CornflowerBlue, () => true);
            this.visual.Add(box);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.control.Draw(graphics, time, alpha);
            this.visual .Draw(graphics, time, alpha);

            StateVisualTester.Draw(graphics, typeof(RegionState), this.region.RegionStates, this.region.Location.ToVector2(), 10, 10);
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