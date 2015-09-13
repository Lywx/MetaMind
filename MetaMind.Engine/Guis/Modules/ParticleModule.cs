namespace MetaMind.Engine.Guis.Modules
{
    using System.Collections.Generic;
    using Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Particles;

    /// <summary>
    /// Particle controller relies on implementation of FloatParticle.
    /// </summary>
    public class ParticleModule : GameEntityModule<ParticleSettings>
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

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
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

                this.Particles[i].Draw(graphics, time, alpha);
            }
        }

        public override void LoadContent(IGameInteropService interop)
        {
        }

        public override void UnloadContent(IGameInteropService interop)
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