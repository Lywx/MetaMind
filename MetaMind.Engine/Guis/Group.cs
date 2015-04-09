namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    public abstract class Group<TGroupSettings> : ManualInputGameElement
    {
        public TGroupSettings Settings { get; protected set; }

        public IGroupControl  Control  { get; protected set; }

        public IGroupGraphics Graphics { get; protected set; }

        protected Group(TGroupSettings settings)
        {
            this.Settings = settings;
        }

        public override void HandleInput()
        {
            base        .HandleInput();
            this.Control.HandleInput();
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.Graphics.Draw(gameTime);
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.Control .UpdateInput(gameTime);
            this.Graphics.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.Control .UpdateStructure(gameTime);
            this.Graphics.UpdateStructure(gameTime);
        }
    }
}