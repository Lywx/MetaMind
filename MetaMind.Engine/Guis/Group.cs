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

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Control .Update(time);
            this.Graphics.Update(time);
        }

        public override void Update(GameTime time)
        {
            this.Control .Update(time);
            this.Graphics.Update(time);
        }
    }
}