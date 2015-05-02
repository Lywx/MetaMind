namespace MetaMind.EngineTest.Guis
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Testers;

    using Microsoft.Xna.Framework;

    using NUnit.Framework;

    public class PlayTest_View : Module<object>
    {
        private GameControllableEntityCollection<GameControllableEntity> entities;

        public PlayTest_View(object settings)
            : base(settings)
        {
            this.entities = new GameControllableEntityCollection<GameControllableEntity>();

            // View Composition

            // 


            var pointView = new View(new PointGridSettings(new Point(50, 50)), new PointView2DFactory(), )
            this.entities.Add(pointView);
        }

        [Test]
        public void Test_Factory()
        {
            
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.entities.Draw(graphics, time, alpha);
        }

        public override void Update(GameTime time)
        {
            this.entities.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.entities.UpdateInput(input, time);
        }
    }
}