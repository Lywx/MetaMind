namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class ParticleModule : Engine.Guis.Modules.ParticleModule
    {
        public ParticleModule(ParticleModuleSettings settings)
            : base(settings)
        {
            SpawnSpeed = 2;
        }

        public override void UpdateInput(GameTime gameTime)
        {
            if (InputSequenceManager.Keyboard.IsActionPressed(Actions.ForceFlip) &&
                InputSequenceManager.Keyboard.IsActionPressed(Actions.Enter))
            {
                this.Refresh = !this.Refresh;
                this.Plain   = !this.Plain;
            }
        }
    }
}