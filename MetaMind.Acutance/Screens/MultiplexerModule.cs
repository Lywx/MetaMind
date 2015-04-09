namespace MetaMind.Acutance.Screens
{
    using MetaMind.Acutance.Guis.Modules;
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;

    using Microsoft.Xna.Framework;

    public class MultiplexerModule : Module<object>
    {
        private readonly MultiplexerGroup           multiplexer;

        private readonly SynchronizationGroup       synchronization;

        public MultiplexerModule()
            : base(null)
        {
            this.multiplexer = new MultiplexerGroup(new MultiplexerGroupSettings());
            this.multiplexer.Load();

            this.synchronization = new SynchronizationGroup(new SynchronizationGroupSettings());
            this.synchronization.Load();
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.multiplexer    .Draw(gameTime, alpha);
            this.synchronization.Draw(gameTime, alpha);
        }

        public override void Load()
        {
            this.multiplexer    .Load();
            this.synchronization.Load();
        }

        public override void Unload()
        {
            this.multiplexer    .Unload();
            this.synchronization.Unload();
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.multiplexer    .UpdateInput(gameInput, gameTime);
            this.synchronization.UpdateInput(gameInput, gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.multiplexer    .UpdateStructure(gameTime);
            this.synchronization.UpdateStructure(gameTime);
        }
    }
}