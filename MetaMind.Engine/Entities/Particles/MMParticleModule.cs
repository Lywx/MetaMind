namespace MetaMind.Engine.Entities.Particles
{
    using System.Collections.Generic;
    using Components.Graphics;
    using Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Particle controller relies on implementation of FloatParticle.
    /// </summary>
    public class MMParticleModule : MMMVCEntity<MMParticleSettings>
    {
        #region Particle Data

        protected List<MMFloatParticle> Particles { get; private set; }

        #endregion

        #region Particle Spawner Data
        public int SpawnRate { get; set; }

        protected MMParticleSpawner<MMFloatParticle> Spawner { get; set; }

        #endregion

        public MMParticleModule(MMParticleSettings settings)
            : base(settings)
        {
            this.Spawner = new MMParticleSpawner<MMFloatParticle>(MMFloatParticle.Prototype());

            this.SpawnRate  = 1;

            this.Particles = new List<MMFloatParticle>();
        }

        public override void Draw(GameTime time)
        {
            for (var i = 0; i < this.Particles.Count; ++i)
            {
                if (i == this.Particles.Count / 2)
                {
                    // Half additive and half solid
                    this.GlobalGraphicsRenderer.End();
                    this.GlobalGraphicsRenderer.Begin(BlendState.Additive);
                }

                this.Particles[i].Draw(time);
            }
        }

        public override void LoadContent()
        {
        }

        public override void UnloadContent()
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
                    this.Particles.Add((MMFloatParticle)this.Spawner.Spawn());
                }
            }
        }
    }
}