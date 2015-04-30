namespace MetaMind.EngineTest.Guis
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.ItemView;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.ContinousView;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Services;
    using MetaMind.Engine.Testers;
    using MetaMind.Runtime.Guis.Widgets;

    using Microsoft.Xna.Framework;

    using Program = MetaMind.EngineTest.Program;

    public class TestModule : Module<object>
    {
        private Region testRegion;

        private View testView;

        public TestModule(object settings)
            : base(settings)
        {
            this.Entities = new GameControllableEntityCollection<GameControllableEntity>();

            // Region
            this.testRegion = new Region(new Rectangle(50, 50, 50, 50));
            this.Entities.Add(this.testRegion);

            // View Composition
            this.testView = new ContinuousView1D(
                new PointViewHorizontalSettings(new Point(50, 50)),
                new ViewFactory(
                    (view, viewSettings, itemSettings) =>
                    new PointView1DLogic(
                        view,
                        (PointViewHorizontalSettings)viewSettings,
                        itemSettings,
                        new ViewItemFactory(
                        item =>
                        new ViewItemLogic(
                            item,
                            Program.Container.GetInstance<ExperienceItemLogic>(),
                            new ViewItemViewControl(item),
                            new ViewItemDataModifier(item)),
                        item => new ViewItemVisual(item),
                        item => new Object())),
                    (view, viewSettings, itemSettings) => new ViewVisualControl(view, viewSettings, itemSettings)),
                new ItemSettings());

            this.Entities.Add(this.testView);
        }

        private GameControllableEntityCollection<GameControllableEntity> Entities { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            StateVisualTester.Draw(graphics, this.testView.States, typeof(ViewState), new Point(500, 50), 10, 50);
            Entities.Draw(graphics, time, alpha);
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