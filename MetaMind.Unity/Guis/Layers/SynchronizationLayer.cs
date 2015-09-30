namespace MetaMind.Unity.Guis.Layers
{
    using Engine;
    using Engine.Gui.Modules;
    using Engine.Screen;
    using Engine.Service;
    using Microsoft.Xna.Framework;
    using Modules;

    public class SynchronizationLayer : MMLayer
    {
        public SynchronizationLayer(IGameScreen screen)
            : base(screen)
        {
            this.Modules = new MMEntityCollection<IMMMvcEntity>();
        }

        private MMEntityCollection<IMMMvcEntity> Modules { get; set; }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            graphics.SpriteBatch.Begin();

            this.Modules.Draw(graphics, time, alpha);

            graphics.SpriteBatch.End();

            base.Draw(graphics, time, alpha);
        }

        public override void LoadContent(IMMEngineInteropService interop)
        {
            var synchronizationModule = new SynchronizationModule(Unity.SessionData.Cognition, new SynchronizationSettings());
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
