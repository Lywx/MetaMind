namespace MetaMind.Unity.Guis.Layers
{
    using System;
    using Concepts.Operations;
    using Engine;
    using Engine.Guis;
    using Engine.Screens;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Modules;

    public class OperationLayer : GameLayer
    {
        private readonly OperationSession operationSession;

        public OperationLayer(OperationSession operationSession, IGameScreen screen, byte transitionAlpha = byte.MaxValue)
            : base(screen, transitionAlpha)
        {
            if (operationSession == null)
            {
                throw new ArgumentNullException("operationSession");
            }

            this.operationSession = operationSession;

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
            this.Modules.Add(
                new OperationModule(
                    this.operationSession,
                    new OperationModuleSettings(),
                    Unity.SessionData.Operation));
            this.Modules.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.Modules.UnloadContent(interop);

            base.UnloadContent(interop);
        }

        public override void Update(GameTime time)
        {
            this.Modules.Update(time);

            base.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Modules.UpdateInput(input, time);

            base.UpdateInput(input, time);
        }
    }
}