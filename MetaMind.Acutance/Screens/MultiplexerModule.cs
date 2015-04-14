namespace MetaMind.Acutance.Screens
{
    using MetaMind.Acutance.Guis.Modules;
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;

    using Microsoft.Xna.Framework;

    public class MultiplexerModule : Module<object>
    {
        private readonly MultiplexerGroup multiplexer;

        private readonly SynchronizationGroup synchronization;

        public MultiplexerModule()
            : base(null)
        {
            this.multiplexer = new MultiplexerGroup(new MultiplexerGroupSettings());
            this.multiplexer.Load(gameFile, gameInput, gameInterop, gameSound);

            this.synchronization = new SynchronizationGroup(new SynchronizationGroupSettings());
            this.synchronization.Load();
        }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.multiplexer    .Draw(gameGraphics, gameTime, alpha);
            this.synchronization.Draw(gameGraphics, gameTime, alpha);
        }

        public override void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            this.multiplexer    .Load(gameFile, gameInput, gameInterop, gameAudio);
            this.synchronization.Load(gameFile, gameInput, gameInterop, gameAudio);
        }

        public override void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            this.multiplexer    .Unload(gameFile, gameInput, gameInterop, gameAudio);
            this.synchronization.Unload(gameFile, gameInput, gameInterop, gameAudio);
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.multiplexer    .UpdateInput(gameInput, gameTime);
            this.synchronization.UpdateInput(gameInput, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.multiplexer    .Update(gameTime);
            this.synchronization.Update(gameTime);
        }
    }
}