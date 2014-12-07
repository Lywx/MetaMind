namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Modules;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class ParticleModule : Engine.Guis.Modules.ParticleModule
    {
        public ParticleModule(ParticleModuleSettings settings)
            : base(settings)
        {
            SpawnSpeed = 2;
        }

        public override void UpdateInput(GameTime gameTime)
        {
            if (InputSequenceManager.Keyboard.IsActionPressed(Actions.SLeft) &&
                InputSequenceManager.Keyboard.IsActionPressed(Actions.SRight))
            {
                this.Refresh = true;
                this.Plain   = true;
            }

            if (InputSequenceManager.Keyboard.IsActionPressed(Actions.SUp) &&
                InputSequenceManager.Keyboard.IsActionPressed(Actions.SDown))
            {
                this.Refresh = false;
                this.Plain   = false;
            }
        }
    }
}