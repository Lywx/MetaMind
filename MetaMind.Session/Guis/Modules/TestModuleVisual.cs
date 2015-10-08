namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Concepts.Tests;
    using Engine.Entities;
    using Engine.Gui.Controls.Labels;
    using Engine.Gui.Graphics.Fonts;
    using Engine.Services;
    using Engine.Settings.Color;
    using Microsoft.Xna.Framework;

    public class TesTMvcVisual : MMMvcEntityVisual<TestModule, TesTMvcSettings, TesTMvcLogic>
    {
        private readonly ITest test;

        public TesTMvcVisual(TestModule module, ITest test) : base(module)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            this.test = test;

            this.VisualEntities = new MMEntityCollection<IMMVisualEntity>();
        }

        private MMEntityCollection<IMMVisualEntity> VisualEntities { get; set; }

        public override void LoadContent(IMMEngineInteropService interop)
        {
            var testCompletionFont  = new Func<Font>(() => Font.UiStatistics);
            var testCompletionColor = new Func<Color>(() => this.test.Evaluation.ResultAllPassedRate > TestMonitor.TestWarningRate ? MMPalette.LightGreen : MMPalette.LightPink);

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

        public override void BeginDraw(IMMEngineGraphicsService graphics, GameTime time)
        {
            base.BeginDraw(graphics, time);

            this.Logic.ControllableEntities.BeginDraw(graphics, time);
            this      .VisualEntities      .BeginDraw(graphics, time);
        }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time)
        {
            base.Draw(graphics, time);

            this.Logic.ControllableEntities.Draw(graphics, time);
            this      .VisualEntities      .Draw(graphics, time);
        }

        public override void EndDraw(IMMEngineGraphicsService graphics, GameTime time)
        {
            base.EndDraw(graphics, time);

            this      .VisualEntities      .EndDraw(graphics, time);
            this.Logic.ControllableEntities.EndDraw(graphics, time);
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