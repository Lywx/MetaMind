namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    public abstract class Group<TGroupSettings> : InputableGameEntity, IGroup<TGroupSettings>
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

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.Graphics.Draw(gameGraphics, gameTime, alpha);
        }

        public override void Update(IGameInput gameInput, GameTime gameTime)
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