namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class ParticleModule : Engine.Guis.Modules.ParticleModule
    {
        public ParticleModule(ParticleModuleSettings settings)
            : base(settings)
        {
            this.SpawnSpeed = 2;
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            if (gameInput.Sequence.Keyboard.IsActionPressed(Actions.ForceFlip) &&
                gameInput.Sequence.Keyboard.IsActionPressed(Actions.Enter))
            {
                this.Refresh = !this.Refresh;
                this.Plain   = !this.Plain;
            }
        }
    }
}