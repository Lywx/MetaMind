namespace MetaMind.EngineTest.Guis
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Visual;
    using MetaMind.Engine.Guis.Widgets.Visuals;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PlayTest_Region : Module<object>
    {
        private GameControllableEntityCollection<GameControllableEntity> control;
        private GameVisualEntityCollection<GameVisualEntity> visual;

        public PlayTest_Region(object settings)
            : base(settings)
        {
            this.control = new GameControllableEntityCollection<GameControllableEntity>();
            this.visual  = new GameVisualEntityCollection<GameVisualEntity>();

            // Region Control
            var region = new Region(new Rectangle(50, 50, 50, 50));
            this.control.Add(region);

            // Box Visual
            var box = new Box(() => region.Rectangle, () => Color.CornflowerBlue, () => true);
            this.visual.Add(box);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.control.Draw(graphics, time, alpha);
            this.visual .Draw(graphics, time, alpha);
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