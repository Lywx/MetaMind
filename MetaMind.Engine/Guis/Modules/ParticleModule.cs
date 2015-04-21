namespace MetaMind.Engine.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Particles;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Particle controller relies on implementation of FloatParticle.
    /// </summary>
    public class ParticleModule : Module<ParticleModuleSettings>
    {
        #region Particle Data

        protected List<FloatParticle> Particles { get; private set; }

        #endregion

        #region Particle Spawner Data
        protected ParticleSpawner<FloatParticle> Spawner { get; set; }

        protected int SpawnSpeed { get; set; }

        protected int SpawnRate { get; set; }

        #endregion

        public ParticleModule(ParticleModuleSettings settings)
            : base(settings)
        {
            this.Spawner = new ParticleSpawner<FloatParticle>(FloatParticle.Prototype());

            this.SpawnRate  = 1;
            this.SpawnSpeed = 1;

            this.Particles = new List<FloatParticle>();
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            for (var i = 0; i < this.Particles.Count; ++i)
            {
                if (i == this.Particles.Count / 2)
                {
                    // half additive and half solid
                    var spriteBatch = graphics.Screen.SpriteBatch;

                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
                }

                this.Particles[i].Draw(graphics, time, alpha);
            }
        }

        public override void Load(IGameFile gameFile, IGameInputService input, IGameInteropService interop, IGameAudioService audio)
        {
        }

        public override void Unload(IGameFile gameFile, IGameInputService input, IGameInteropService interop, IGameAudioService audio)
        {
        }

        public override void Update(GameTime gameTime)
        {
            for (var i = 0; i < this.Particles.Count; ++i)
            {
                this.Particles[i].Update(gameTime);

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