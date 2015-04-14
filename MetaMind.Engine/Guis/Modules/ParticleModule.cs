namespace MetaMind.Engine.Guis.Modules
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Particles;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ParticleModule : Module<ParticleModuleSettings>
    {
        public ParticleModule(ParticleModuleSettings settings)
            : base(settings)
        {
            this.Particles = new List<FloatParticle>();

            this.SpawnSpeed   = 1;
            this.InitialSpeed = 1;

            FloatParticle.Generate = settings.Generate;
            FloatParticle.Random   = settings.Random;
            FloatParticle.Width    = settings.ParticleWidth;
            FloatParticle.Height   = settings.ParticleHeight;
        }

        protected int InitialSpeed { get; set; }

        protected List<FloatParticle> Particles { get; private set; }

        protected bool Plain { get; set; }

        protected bool Refresh { get; set; }

        protected int SpawnSpeed { get; set; }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            for (var i = 0; i < this.Particles.Count; ++i)
            {
                if (i == this.Particles.Count / 2)
                {
                    if (!this.Plain)
                    {
                        // half additive and half solid
                        var spriteBatch = gameGraphics.Screen.SpriteBatch;

                        spriteBatch.End();
                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
                    }
                }

                this.Particles[i].Draw(gameGraphics, gameTime, alpha);
            }
        }

        public override void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
        }

        public override void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
        }

        public override void Update(GameTime gameTime)
        {
            for (var i = 0; i < this.Particles.Count; ++i)
            {
                if (this.Refresh)
                {
                    this.Particles[i].Colorize();
                }

                this.Particles[i].Update(gameTime);

                if (this.Particles[i].Life < 0 ||
                    this.Particles[i].IsOutsideScreen)
                {
                    this.Particles.Remove(this.Particles[i]);
                }
            }

            if (this.Particles.Count < this.Settings.ParticleNum)
            {
                for (var i = 0; i < this.SpawnSpeed; ++i)
                {
                    this.Particles.Add(FloatParticle.RandomParticle(this.InitialSpeed));
                }
            }
        }
    }
}