namespace MetaMind.EngineTest.Guis
{
    using Engine;
    using Engine.Guis;
    using Engine.Guis.Elements;
    using Engine.Guis.Widgets.Regions;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Services;
    using Engine.Testers;
    using Microsoft.Xna.Framework;

    public class PlayTest_Frame : Module<object>
    {
        private GameControllableEntityCollection<GameControllableEntity> control;
        private GameVisualEntityCollection<GameVisualEntity> visual;
        private DraggableFrame frame;

        public PlayTest_Frame(object settings)
            : base(settings)
        {
            this.control = new GameControllableEntityCollection<GameControllableEntity>();
            this.visual  = new GameVisualEntityCollection<GameVisualEntity>();

            // Region Control
            this.frame = new DraggableFrame(new Rectangle(50, 50, 50, 50));
            this.control.Add(this.frame);

            // Box Visual
            var box = new Box(() => this.frame.Rectangle, () => Color.CornflowerBlue, () => true);
            this.visual.Add(box);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.control.Draw(graphics, time, alpha);
            this.visual .Draw(graphics, time, alpha);

            StateVisualTester.Draw(graphics, typeof(FrameState), this.frame.States, this.frame.Location.ToVector2(), 10, 10);
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