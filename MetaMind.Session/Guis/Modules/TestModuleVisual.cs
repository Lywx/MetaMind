namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Engine.Components.Content.Fonts;
    using Engine.Entities;
    using Engine.Entities.Bases;
    using Engine.Entities.Controls.Labels;
    using Engine.Entities.Graphics.Fonts;
    using Engine.Settings;
    using Microsoft.Xna.Framework;
    using Tests;

    public class TesTMVCRenderer : MMMVCEntityRenderer<TestModule, TesTMVCSettings, TesTMVCController>
    {
        private readonly ITest test;

        public TesTMVCRenderer(TestModule module, ITest test) : base(module)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            this.test = test;

            this.VisualEntities = new MMEntityCollection<IMMVisualEntity>();
        }

        private MMEntityCollection<IMMVisualEntity> VisualEntities { get; set; }

        public override void LoadContent()
        {
            var testCompletionFont  = new Func<MMFont>(() => MMFont.UiStatistics);
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
            this.VisualEntities.LoadContent();


            base.LoadContent();
        }

        private Vector2 TestRateCenterPosition => new Vector2(this.EngineGraphics.Settings.Width / 2 - 160, 90);

        public override void BeginDraw(GameTime time)
        {
            base.BeginDraw(time);

            this.Controller.ControllableEntities.BeginDraw(time);
            this      .VisualEntities      .BeginDraw(time);
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);

            this.Controller.ControllableEntities.Draw(time);
            this      .VisualEntities      .Draw(time);
        }

        public override void EndDraw(GameTime time)
        {
            base.EndDraw(time);

            this      .VisualEntities      .EndDraw(time);
            this.Controller.ControllableEntities.EndDraw(time);
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