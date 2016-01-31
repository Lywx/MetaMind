namespace MetaMind.Session.Guis.Layers
{
    using Engine.Core.Entity;
    using Engine.Core.Entity.Common;
    using Engine.Core.Entity.Screens;
    using Microsoft.Xna.Framework;
    using Modules;

    public class SynchronizationLayer : MMLayer
    {
        public SynchronizationLayer(IMMScreen screen)
            : base(screen)
        {
            this.Modules = new MMEntityCollection<IMMMVCEntity>();
        }

        private MMEntityCollection<IMMMVCEntity> Modules { get; set; }

        public override void Draw(GameTime time)
        {
            this.GlobalGraphicsRenderer.Begin();

            this.Modules.Draw(time);

            this.GlobalGraphicsRenderer.End();

            base.Draw(time);
        }

        public override void LoadContent()
        {
            var synchronizationModule = new SynchronizationModule(MMSessionGame.SessionData.Cognition, new SynchronizationSettings());
            this.Modules.Add(synchronizationModule);

            this.Modules.LoadContent();
        }

        public override void UnloadContent()
        {
            this.Modules.UnloadContent();
        }

        public override void Update(GameTime time)
        {
            this.Modules.Update(time);
        }

        public override void UpdateInput(GameTime time)
        {
            this.Modules.UpdateInput(time);
        }
    }
}
