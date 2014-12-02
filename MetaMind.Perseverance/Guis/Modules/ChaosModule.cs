namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Perseverance.Guis.Particles;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ChaosModule : Module<ChaosModuleSettings>
    {
        private List<FloatParticle> particles = new List<FloatParticle>();

        private bool refresh;

        public ChaosModule(ChaosModuleSettings settings)
            : base(settings)
        {
            FloatParticle.Random   = settings.Random;
            FloatParticle.Generate = settings.Generate;
            FloatParticle.Width    = settings.Width;
            FloatParticle.Height   = settings.Height;
        }

        public override void Load()
        {
        }

        public override void Unload()
        {
        }

        public void Draw(GameTime gameTime)
        {
            for (var i = 0; i < this.particles.Count; ++i)
            {
                if (i == this.particles.Count / 2)
                {
                    if (!this.refresh)
                    {
                        // half additive and half solid
                        ScreenManager.SpriteBatch.End();
                        ScreenManager.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
                    }
                }

                this.particles[i].Draw(gameTime);
            }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            if (InputSequenceManager.Keyboard.IsActionPressed(Actions.SLeft) &&
                InputSequenceManager.Keyboard.IsActionPressed(Actions.SRight))
            {
                this.refresh = true;
            }

            if (InputSequenceManager.Keyboard.IsActionPressed(Actions.SUp) &&
                InputSequenceManager.Keyboard.IsActionPressed(Actions.SDown))
            {
                this.refresh = false;
            }
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            for (var i = 0; i < this.particles.Count; ++i)
            {
                if (this.refresh)
                {
                    this.particles[i].Colorize();
                }

                this.particles[i].Update(gameTime);

                if (this.particles[i].LastingSeconds < 0 ||
                    this.particles[i].IsOutsideScreen)
                {
                    this.particles.Remove(this.particles[i]);
                }
            }

            if (this.particles.Count < this.Settings.ParticleNum)
            {
                this.particles.Add(FloatParticle.RandomParticle());
            }
        }
    }
}