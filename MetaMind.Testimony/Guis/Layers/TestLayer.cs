namespace MetaMind.Testimony.Guis.Layers
{
    using System;
    using Concepts.Tests;
    using Engine;
    using Engine.Guis;
    using Engine.Screens;
    using Engine.Services;
    using Guis.Modules;
    using Microsoft.Xna.Framework;

    public class TestLayer : GameLayer
    {
        private readonly TestSession testSession;

        public TestLayer(TestSession testSession, IGameScreen screen, byte transitionAlpha = byte.MaxValue)
            : base(screen, transitionAlpha)
        {
            if (testSession == null)
            {
                throw new ArgumentNullException("testSession");
            }

            this.testSession = testSession;

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
            var testModule = new TestModule(new TestModuleSettings(), Testimony.SessionData.Test, this.testSession);

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