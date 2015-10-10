namespace MetaMind.Session.Guis.Layers
{
    using Engine.Entities;
    using Engine.Screen;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Modules;

    public class SynchronizationLayer : MMLayer
    {
        public SynchronizationLayer(IMMScreen screen)
            : base(screen)
        {
            this.Modules = new MMEntityCollection<IMMMvcEntity>();
        }

        private MMEntityCollection<IMMMvcEntity> Modules { get; set; }

        public override void Draw(GameTime time)
        {
            graphics.SpriteBatch.Begin();

            this.Modules.Draw(time);

            graphics.SpriteBatch.End();

            base.Draw(time);
        }

        public override void LoadContent(IMMEngineInteropService interop)
        {
            var synchronizationModule = new SynchronizationModule(SessionGame.SessionData.Cognition, new SynchronizationSettings());
            this.Modules.Add(synchronizationModule);

            this.Modules.LoadContent(interop);
        }

        public override void UnloadContent(IMMEngineInteropService interop)
        {
            this.Modules.UnloadContent(interop);
        }

        public override void Update(GameTime time)
        {
            this.Modules.Update(time);
        }

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            this.Modules.UpdateInput(input, time);
        }
    }
}
