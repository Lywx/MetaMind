namespace MetaMind.Testimony.Guis.Layers
{
    using Engine;
    using Engine.Guis;
    using Engine.Screens;
    using Engine.Services;
    using Microsoft.Xna.Framework;

    public class OperationLayer : GameLayer
    {
        public OperationLayer(IGameScreen screen, byte alpha = byte.MaxValue)
            : base(screen, alpha)
        {
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
            this.Modules.Add();
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