namespace MetaMind.Engine.Guis.Modules
{
    using System;

    public class ParticleModuleSettings : ICloneable
    {
        public int ParticleNum = 1500;

        public int ParticleWidth;

        public int ParticleHeight;

        public ParticleModuleSettings(int particleWidth, int particleHeight, int particleNum)
        {
            this.ParticleWidth  = particleWidth;
            this.ParticleHeight = particleHeight;
            this.ParticleNum    = particleNum;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}