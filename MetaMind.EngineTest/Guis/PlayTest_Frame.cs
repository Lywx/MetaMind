namespace MetaMind.EngineTest.Guis
{
    using Engine;
    using Engine.Entities;
    using Engine.Gui.Controls.Images;
    using Engine.Gui.Elements;
    using Engine.Gui.Elements.Rectangles;
    using Engine.Gui.Modules;
    using Engine.Service;
    using Microsoft.Xna.Framework;

    public class PlayTest_Frame : MMMvcEntity<object>
    {
        private MMEntityCollection<MMInputableEntity> control;

        private MMEntityCollection<MMVisualEntity> visual;

        private DraggableRectangle rectangle;

        public PlayTest_Frame(object settings)
            : base(settings)
        {
            this.control = new MMEntityCollection<MMInputableEntity>();
            this.visual  = new MMEntityCollection<MMVisualEntity>();

            // Region Control
            this.rectangle = new DraggableRectangle(new Rectangle(50, 50, 50, 50)) {Movable = false};
            this.control.Add(this.rectangle);

            // Box Visual
            var box = new ImageBox(() => this.rectangle.Bounds, () => Color.CornflowerBlue, () => true);
            this.visual.Add(box);
        }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.control.Draw(graphics, time, alpha);
            this.visual .Draw(graphics, time, alpha);

            StateVisualTester.Draw(graphics, typeof(ElementState), this.rectangle.FrameStates, this.rectangle.Location.ToVector2(), 10, 10);
        }

        public override void Update(GameTime time)
        {
            this.visual .Update(time);
            this.control.Update(time);
        }

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            this.control.UpdateInput(input, time);
        }
    }
}