namespace MetaMind.Session.Guis.Layers
{
    using System;
    using System.Speech.Synthesis;
    using Concepts.Tests;
    using Engine.Components.Content.Fonts;
    using Engine.Components.Graphics;
    using Engine.Entities;
    using Engine.Entities.Controls.Labels;
    using Engine.Entities.Graphics.Fonts;
    using Engine.Screens;
    using Engine.Services;
    using Engine.Settings;
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

        private MMEntityCollection<IMMInputEntity> inputEntities;

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

            this.inputEntities = new MMEntityCollection<IMMInputEntity>();
            this.drawEntities = new MMEntityCollection<IMMVisualEntity>();
        }

        #endregion

        #region Load and Unload

        public override void LoadContent()
        {
            this.test = SessionGame.SessionData.Test;

            // Controllables
            var testModule = new TestModule(
                new TesTMVCSettings(),
                this.test,
                this.testSession,
                this.testSynthesizer);

            this.inputEntities.Add(testModule);
            this.inputEntities.LoadContent();

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
            this.drawEntities.LoadContent();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            this.inputEntities.UnloadContent();
            this.drawEntities .UnloadContent();
            base              .UnloadContent();
        }

        #endregion

        #region Draw

        public override void Draw(GameTime time)
        {
            ((MMVisualEntity)this).Graphics.Renderer.Begin();

            this.inputEntities.Draw(time);
            this.drawEntities .Draw(time); 

            ((MMVisualEntity)this).Graphics.Renderer.End();

            base.Draw(time);
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
                this.testLabel.TextColor = () => MMPalette.LightPink.MakeTransparent((byte)(255 * brightness));
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

        public override void UpdateInput(GameTime time)
        {
            this.inputEntities.UpdateInput(time);
            base.                     UpdateInput(time);
        }

        #endregion
    }
}