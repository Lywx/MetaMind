namespace MetaMind.Acutance.Screens
{
    using MetaMind.Acutance.Guis.Modules;
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    using IGameInteropService = MetaMind.Engine.IGameInteropService;

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

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.multiplexer    .Draw(graphics, time, alpha);
            this.synchronization.Draw(graphics, time, alpha);
        }

        public override void Load(IGameInteropService interop, IGameInputService input, Engine.Services.IGameInteropService interop, IGameAudioService audio)
        {
            this.multiplexer    .Load(gameFile, input, interop, audio);
            this.synchronization.Load(gameFile, input, interop, audio);
        }

        public override void Unload(IGameInteropService interop, IGameInputService input, Engine.Services.IGameInteropService interop, IGameAudioService audio)
        {
            this.multiplexer    .Unload(gameFile, input, interop, audio);
            this.synchronization.Unload(gameFile, input, interop, audio);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.multiplexer    .UpdateInput(input, time);
            this.synchronization.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.multiplexer    .Update(time);
            this.synchronization.Update(time);
        }
    }
}