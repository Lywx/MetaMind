namespace MetaMind.Unity.Guis.Layers
{
    using System;
    using System.Speech.Synthesis;
    using Concepts.Tests;
    using Engine;
    using Engine.Guis;
    using Engine.Screens;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Modules;

    public class TestLayer : GameLayer
    {
        private readonly TestSession testSession;

        private readonly SpeechSynthesizer testSynthesizer;

        public TestLayer(TestSession testSession, SpeechSynthesizer testSynthesizer, IGameScreen screen, byte transitionAlpha = byte.MaxValue)
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

            this.testSession     = testSession;
            this.testSynthesizer = testSynthesizer;

            this.Modules = new GameControllableEntityCollection<IModule>();
        }

        private GameControllableEntityCollection<IModule> Modules { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            graphics.SpriteBatch.Begin();

            this.Modules.Draw(graphics, time, alpha);

            graphics.SpriteBatch.End();

            base.Draw(graphics, time, alpha);
        }

        public override void LoadContent(IGameInteropService interop)
        {
            var testModule = new TestModule(new TestModuleSettings(), Unity.SessionData.Test, this.testSession, this.testSynthesizer);

            this.Modules.Add(testModule);
            this.Modules.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.Modules.UnloadContent(interop);
            base        .UnloadContent(interop);
        }

        public override void Update(GameTime time)
        {
            this.Modules.Update(time);
            base.        Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Modules.UpdateInput(input, time);
            base.        UpdateInput(input, time);
        }
    }
}