﻿namespace MetaMind.Testimony.Guis.Layers
{
    using Engine;
    using Engine.Guis;
    using Engine.Screens;
    using Engine.Services;
    using Guis.Modules;
    using Microsoft.Xna.Framework;

    public class SynchronizationLayer : GameLayer
    {
        public SynchronizationLayer(IGameScreen screen, byte alpha = byte.MaxValue)
            : base(screen, alpha)
        {
            this.Modules = new GameControllableEntityCollection<IModule>();
        }

        private GameControllableEntityCollection<IModule> Modules { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            base.Draw(graphics, time, alpha);

            graphics.SpriteBatch.Begin();

            this.Modules.Draw(graphics, time, alpha);

            graphics.SpriteBatch.End();
        }

        public override void LoadContent(IGameInteropService interop)
        {
            var synchronizationModule = new SynchronizationModule(Testimony.SessionData.Cognition, new SynchronizationSettings());
            this.Modules.Add(synchronizationModule);

            this.Modules.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.Modules.UnloadContent(interop);
        }

        public override void Update(GameTime time)
        {
            this.Modules.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Modules.UpdateInput(input, time);
        }
    }
}
