namespace MetaMind.Unity.Guis.Layers
{
    using System;
    using System.Speech.Synthesis;
    using Concepts.Tests;
    using Engine;
    using Engine.Components.Content.Fonts;
    using Engine.Components.Graphics.Fonts;
    using Engine.Entities;
    using Engine.Gui.Controls.Labels;
    using Engine.Screen;
    using Engine.Service;
    using Engine.Settings.Color;
    using Microsoft.Xna.Framework;
    using Modules;
    using Screens;

    public class TestLayer : MMLayer
    {
        private readonly TestSession testSession;

        private readonly SpeechSynthesizer testSynthesizer;

        private Label testLabel;

        private ITest test;

        private MMEntityCollection<IMMVisualEntity> drawEntities;

        private MMEntityCollection<IMMInputableEntity> inputEntities;

        #region Constructors

        public TestLayer(
            TestSession testSession,
            SpeechSynthesizer testSynthesizer,
            IMMScreen screen)
            : base(screen)
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

            this.inputEntities = new MMEntityCollection<IMMInputableEntity>();
            this.drawEntities = new MMEntityCollection<IMMVisualEntity>();
        }

        #endregion

        #region Load and Unload

        public override void LoadContent(IMMEngineInteropService interop)
        {
            this.test = Unity.SessionData.Test;

            // Controllables
            var testModule = new TestModule(
                new TesTMvcSettings(),
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
                AnchorLocation = () => new Vector2((float)graphicsSettings.Width / 2, 80),
                TextColor    = () => Color.White,
                TextSize     = () => 1.0f,
                TextHAlignment   = HoritonalAlignment.Center,
                TextVAlignment   = VerticalAlignment.Center,
            };

            this.drawEntities.Add(this.testLabel);
            this.drawEntities.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IMMEngineInteropService interop)
        {
            this.inputEntities.UnloadContent(interop);
            this.drawEntities .UnloadContent(interop);
            base              .UnloadContent(interop);
        }

        #endregion

        #region Draw

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.SpriteBatch.Begin();

            this.inputEntities.Draw(graphics, time, Math.Min(alpha, this.Opacity));
            this.drawEntities .Draw(graphics, time, Math.Min(alpha, this.Opacity)); 

            this.SpriteBatch.End();

            base.Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.inputEntities.Update(time);
            this.drawEntities .Update(time);
            this              .UpdateEffect(time);
            base              .Update(time);
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

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            this.inputEntities.UpdateInput(input, time);
            base.                     UpdateInput(input, time);
        }

        #endregion
    }
}