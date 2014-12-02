namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    using MetaMind.Perseverance.Guis.Particles;

    public class ChaosModuleSettings : ICloneable
    {
        public Random           Random;
        public GenerateParticle Generate;

        public int ParticleNum = 1500;

        public int Width;

        public int Height;

        public ChaosModuleSettings(Random random, GenerateParticle generate, int width, int height)
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