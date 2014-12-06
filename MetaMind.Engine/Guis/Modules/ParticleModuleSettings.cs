namespace MetaMind.Engine.Guis.Modules
{
    using System;

    using MetaMind.Engine.Guis.Particles;

    public class ParticleModuleSettings : ICloneable
    {
        public Random           Random;
        public GenerateParticle Generate;

        public int ParticleNum = 1500;

        public int Width;

        public int Height;

        public ParticleModuleSettings(Random random, GenerateParticle generate, int width, int height)
        {
            this.Random   = random;
            this.Generate = generate;

            this.Width    = width;
            this.Height   = height;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}