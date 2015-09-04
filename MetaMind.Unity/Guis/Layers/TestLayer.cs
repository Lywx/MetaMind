namespace MetaMind.Unity.Guis.Layers
{
    using System;
    using System.Speech.Synthesis;
    using Concepts.Tests;
    using Engine;
    using Engine.Components.Fonts;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Screens;
    using Engine.Services;
    using Engine.Settings.Colors;
    using Microsoft.Xna.Framework;
    using Modules;
    using Screens;

    public class TestLayer : GameLayer
    {
        private readonly TestSession testSession;

        private readonly SpeechSynthesizer testSynthesizer;

        private Label testLabel;

        private ITest test;

        private GameVisualEntityCollection<IGameVisualEntity> drawEntities;

        private GameControllableEntityCollection<IGameControllableEntity> inputEntities;

        #region Constructors

        public TestLayer(
            TestSession testSession,
            SpeechSynthesizer testSynthesizer,
            IGameScreen screen,
            byte transitionAlpha = byte.MaxValue)
            : base(screen, transitionAlpha)
        {
            if (testSession == null)
            {
                throw new ArgumentNullException(nameof(testSession));
            }

            if (testSynthesizer == null)
            {
                throw new ArgumentNullException(nameof(testSynthesizer));
            }

            this.testSession = testSession;
            this.testSynthesizer = testSynthesizer;

            this.inputEntities = new GameControllableEntityCollection<IGameControllableEntity>();
            this.drawEntities = new GameVisualEntityCollection<IGameVisualEntity>();
        }

        #endregion

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            this.test = Unity.SessionData.Test;

            // Controllables
            var testModule = new TestModule(
                new TestModuleSettings(),
                this.test,
                this.testSession,
                this.testSynthesizer);

            this.inputEntities.Add(testModule);
            this.inputEntities.LoadContent(interop);

            // Visuals
            var graphicsSettings = this.Graphics.Settings;

            this.testLabel = new Label
            {
                TextFont     = () => Font.UiRegular,
                Text         = () => "TESTS",
                TextPosition = () => new Vector2((float)graphicsSettings.Width / 2, 80),
                TextColor    = () => Color.White,
                TextSize     = () => 1.0f,
                TextHAlign   = StringHAlign.Center,
                TextVAlign   = StringVAlign.Center,
            };

            this.drawEntities.Add(this.testLabel);
            this.drawEntities.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.inputEntities.UnloadContent(interop);
            this.drawEntities .UnloadContent(interop);
            base              .UnloadContent(interop);
        }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            graphics.SpriteBatch.Begin();

            this.inputEntities.Draw(graphics, time, Math.Min(alpha, this.TransitionAlpha));
            this.drawEntities .Draw(graphics, time, Math.Min(alpha, this.TransitionAlpha)); 

            graphics.SpriteBatch.End();

            base.Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.inputEntities.Update(time);
            this.drawEntities     .Update(time);
            this                     .UpdateEffect(time);
            base                     .Update(time);
        }

        private void UpdateEffect(GameTime time)
        {
            if (this.test.Evaluation.ResultAllPassedRate < TestMonitor.TestWarningRate)
            {
                var brightness = Math.Abs(Math.Cos(time.TotalGameTime.TotalSeconds * 7));

                this.testLabel.Text = () => "TESTS\nWARNING";
                this.testLabel.TextColor = () => Palette.LightPink.MakeTransparent((byte)(255 * brightness));
                this.testLabel.TextLeading = 22;

                BackgroundScreen.Brightness = (float)(1 + brightness);
                BackgroundScreen.Color = new Vector3(1, 0, 0);
            }
            else
            {
                this.testLabel.Text = () => "TESTS";
                this.testLabel.TextColor = () => Color.White;

                BackgroundScreen.Brightness = 1;
                BackgroundScreen.Color = new Vector3(0, 0, 1);
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.inputEntities.UpdateInput(input, time);
            base.                     UpdateInput(input, time);
        }

        #endregion
    }
}