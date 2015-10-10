namespace MetaMind.Engine.Gui.Modules
{
    using System.Collections.Generic;
    using Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Particle;
    using Services;

    /// <summary>
    /// Particle controller relies on implementation of FloatParticle.
    /// </summary>
    public class ParticleModule : MMMvcEntity<ParticleSettings>
    {
        #region Particle Data

        protected List<FloatParticle> Particles { get; private set; }

        #endregion

        #region Particle Spawner Data
        public int SpawnRate { get; set; }

        protected ParticleSpawner<FloatParticle> Spawner { get; set; }

        #endregion

        public ParticleModule(ParticleSettings settings)
            : base(settings)
        {
            this.Spawner = new ParticleSpawner<FloatParticle>(FloatParticle.Prototype());

            this.SpawnRate  = 1;

            this.Particles = new List<FloatParticle>();
        }

        public override void Draw(GameTime time)
        {
            for (var i = 0; i < this.Particles.Count; ++i)
            {
                if (i == this.Particles.Count / 2)
                {
                    // half additive and half solid
                    var spriteBatch = graphics.SpriteBatch;

                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
                }

                this.Particles[i].Draw(time);
            }
        }

        public override void LoadContent(IMMEngineInteropService interop)
        {
        }

        public override void UnloadContent(IMMEngineInteropService interop)
        {
        }

        public override void Update(GameTime time)
        {
            for (var i = 0; i < this.Particles.Count; ++i)
            {
                this.Particles[i].Update(time);

                if (this.Particles[i].Life < 0 ||
                    this.Particles[i].IsOutsideScreen)
                {
                    this.Particles.Remove(this.Particles[i]);
                }
            }

            if (this.Particles.Count < this.Settings.ParticleNum)
            {
                for (var i = 0; i < this.SpawnRate; ++i)
                {
                    // Temporary solution for FloatParticle implementation
                    this.Particles.Add((FloatParticle)this.Spawner.Spawn());
                }
            }
        }
    }
}