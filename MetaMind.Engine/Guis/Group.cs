namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public abstract class Group<TGroupSettings> : GameControllableEntity, IGroup<TGroupSettings>
    {
        public TGroupSettings Settings { get; protected set; }

        public IGroupControl  Control  { get; protected set; }

        public IGroupGraphics Graphics { get; protected set; }

        #region Constructors

        protected Group(TGroupSettings settings)
        {
            this.Settings = settings;
        }

        #endregion

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Graphics.Draw(graphics, time, alpha);
        }

        public override void UpdateInput(IGameInputService input, GameTime gameTime)
        {
            this.Control .Update(gameTime);
            this.Graphics.Update(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.Control .Update(gameTime);
            this.Graphics.Update(gameTime);
        }
    }
}