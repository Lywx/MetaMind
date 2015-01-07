namespace MetaMind.Acutance.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class ParticleModule : Engine.Guis.Modules.ParticleModule
    {
        private bool accelerating;

        public ParticleModule(ParticleModuleSettings settings)
            : base(settings)
        {
        }

        public override void UpdateInput(GameTime gameTime)
        {
            if (InputSequenceManager.Keyboard.IsActionPressed(Actions.FastLeft) &&
                InputSequenceManager.Keyboard.IsActionPressed(Actions.FastRight))
            {
                this.accelerating = true;

                this.Refresh = true;
                this.Plain   = true;

                this.InitialSpeed = 10;
                this.SpawnSpeed   = 10;

                this.Settings.ParticleNum = 2000;

                GraphicsSettings.Fps = 60;
            }
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            base.UpdateStructure(gameTime);

            if (this.accelerating)
            {
                foreach (var particle in this.Particles)
                {
                    particle.Velocity += new Vector2(0, -10);
                }
            }
        }
    }
}