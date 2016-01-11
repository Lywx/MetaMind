namespace MetaMind.EngineTest.Guis
{
    using Engine;
    using Engine.Entities;
    using Engine.Entities.Bases;
    using Engine.Entities.Controls.Images;
    using Engine.Entities.Elements;
    using Engine.Entities.Elements.Rectangles;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Rectangle = Microsoft.Xna.Framework.Rectangle;

    public class PlayTest_Frame : MMMVCEntity<object>
    {
        private MMEntityCollection<MMInputEntity> control;

        private MMEntityCollection<MMVisualEntity> visual;

        private MMDraggableRectangleElement immRectangle;

        public PlayTest_Frame(object settings)
            : base(settings)
        {
            this.control = new MMEntityCollection<MMInputEntity>();
            this.visual  = new MMEntityCollection<MMVisualEntity>();

            // Region Control
            this.immRectangle = new MMDraggableRectangleElement(new Rectangle(50, 50, 50, 50)) {Movable = false};
            this.control.Add(this.immRectangle);

            // Box Visual
            var box = new ImageBox(() => this.immRectangle.Bounds, () => Color.CornflowerBlue, () => true);
            this.visual.Add(box);
        }

        public override void Draw(GameTime time)
        {
            this.control.Draw(time);
            this.visual .Draw(time);

            StateVisualTester.Draw(graphics, typeof(MMInputElementDebugState), this.immRectangle.InputStates, this.immRectangle.Location.ToVector2(), 10, 10);
        }

        public override void Update(GameTime time)
        {
            this.visual .Update(time);
            this.control.Update(time);
        }

        public override void UpdateInput(GameTime time)
        {
            this.control.UpdateInput(time);
        }
    }
}