namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Concepts.Tests;
    using Engine;
    using Engine.Components.Fonts;
    using Engine.Guis;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Services;
    using Engine.Settings.Colors;
    using Microsoft.Xna.Framework;

    public class TestModuleVisual : GameEntityModuleVisual<TestModule, TestModuleSettings, TestModuleLogic>
    {
        private readonly ITest test;

        public TestModuleVisual(TestModule module, ITest test) : base(module)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            this.test = test;

            this.VisualEntities = new GameVisualEntityCollection<IGameVisualEntity>();
        }

        private GameVisualEntityCollection<IGameVisualEntity> VisualEntities { get; set; }

        public override void LoadContent(IGameInteropService interop)
        {
            var testCompletionFont  = new Func<Font>(() => Font.UiStatistics);
            var testCompletionColor = new Func<Color>(() => this.test.Evaluation.ResultAllPassedRate > TestMonitor.TestWarningRate ? Palette.LightGreen : Palette.LightPink);

            var testRatePrefix = new Label
            {
                TextFont     = testCompletionFont,
                Text         = () => $"{this.test.Evaluation.ResultAllPassedRate.ToString("F0")}",
                TextPosition = () => this.TestRateCenterPosition,
                TextColor    = testCompletionColor,
                TextSize     = () => 2.0f,
                TextHAlign   = StringHAlign.Left,
                TextVAlign   = StringVAlign.Center,
            };

            var testRateSubfix = new Label { 
                TextFont       = testCompletionFont,
                Text           = () => " %",
                TextPosition   = () => this.TestRateCenterPosition,
                TextColor      = testCompletionColor,
                TextSize       = () => 1f,
                TextHAlign     = StringHAlign.Right,
                TextVAlign     = StringVAlign.Center};

            this.VisualEntities.Add(testRatePrefix);
            this.VisualEntities.Add(testRateSubfix);
            this.VisualEntities.LoadContent(interop);


            base.LoadContent(interop);
        }

        private Vector2 TestRateCenterPosition => new Vector2(this.EngineGraphics.Settings.Width / 2 - 160, 90);

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Logic.ControllableEntities.Draw(graphics, time, alpha);
            this      .VisualEntities      .Draw(graphics, time, alpha);
            base                           .Draw(graphics, time, alpha);
        }

        public override void Update(GameTime time)
        {
            this.VisualEntities.UpdateForwardBuffer();
            this.VisualEntities.Update(time);
            this.VisualEntities.UpdateBackwardBuffer();

            base.Update(time);
        }
    }
}