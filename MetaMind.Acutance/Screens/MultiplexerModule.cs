namespace MetaMind.Acutance.Screens
{
    using MetaMind.Acutance.Guis.Modules;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class MultiplexerModule : Module<object>
    {
        private readonly MultiplexerGroup           multiplexer;

        private readonly SynchronizationGroup       synchronization;
        private readonly SynchronizationGroupClient synchronizationClient;


        public MultiplexerModule()
            : base(null)
        {
            this.multiplexer = new MultiplexerGroup(new MultiplexerGroupSettings());
            this.multiplexer.Load();

            this.synchronizationClient = new SynchronizationGroupClient();
            this.synchronizationClient.TraceView     = this.multiplexer.PTraceView;
            this.synchronizationClient.KnowledgeView = this.multiplexer.KnowledgeView;
             
            this.synchronization = new SynchronizationGroup(this.synchronizationClient, new SynchronizationGroupSettings());
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

        public override void UpdateInput(GameTime gameTime)
        {
            this.multiplexer    .UpdateInput(gameTime);
            this.synchronization.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.multiplexer    .UpdateStructure(gameTime);
            this.synchronization.UpdateStructure(gameTime);
        }
    }
}