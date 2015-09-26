namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Concepts.Tests;
    using Engine;
    using Engine.Components.Graphics.Fonts;
    using Engine.Gui.Controls.Labels;
    using Engine.Gui.Modules;
    using Engine.Service;
    using Engine.Setting.Color;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

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

            this.VisualEntities = new GameEntityCollection<IGameVisualEntity>();
        }

        private GameEntityCollection<IGameVisualEntity> VisualEntities { get; set; }

        public override void LoadContent(IGameInteropService interop)
        {
            var testCompletionFont  = new Func<Font>(() => Font.UiStatistics);
            var testCompletionColor = new Func<Color>(() => this.test.Evaluation.ResultAllPassedRate > TestMonitor.TestWarningRate ? Palette.LightGreen : Palette.LightPink);

            var testRatePrefix = new Label
            {
                TextFont     = testCompletionFont,
                Text         = () => $"{this.test.Evaluation.ResultAllPassedRate.ToString("F0")}",
                AnchorLocation = () => this.TestRateCenterPosition,
                TextColor    = testCompletionColor,
                TextSize     = () => 2.0f,
                TextHAlignment   = HoritonalAlignment.Left,
                TextVAlignment   = VerticalAlignment.Center,
            };

            var testRateSubfix = new Label { 
                TextFont       = testCompletionFont,
                Text           = () => " %",
                AnchorLocation   = () => this.TestRateCenterPosition,
                TextColor      = testCompletionColor,
                TextSize       = () => 1f,
                TextHAlignment     = HoritonalAlignment.Right,
                TextVAlignment     = VerticalAlignment.Center};

            this.VisualEntities.Add(testRatePrefix);
            this.VisualEntities.Add(testRateSubfix);
            this.VisualEntities.LoadContent(interop);


            base.LoadContent(interop);
        }

        private Vector2 TestRateCenterPosition => new Vector2(this.Graphics.Settings.Width / 2 - 160, 90);

        public override void BeginDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.BeginDraw(graphics, time, alpha);

            this.Logic.ControllableEntities.BeginDraw(graphics, time, alpha);
            this      .VisualEntities      .BeginDraw(graphics, time, alpha);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.Draw(graphics, time, alpha);

            this.Logic.ControllableEntities.Draw(graphics, time, alpha);
            this      .VisualEntities      .Draw(graphics, time, alpha);
        }

        public override void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.EndDraw(graphics, time, alpha);

            this      .VisualEntities      .EndDraw(graphics, time, alpha);
            this.Logic.ControllableEntities.EndDraw(graphics, time, alpha);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.VisualEntities.UpdateForwardBuffer();
            this.VisualEntities.Update(time);
            this.VisualEntities.UpdateBackwardBuffer();
        }
    }
}