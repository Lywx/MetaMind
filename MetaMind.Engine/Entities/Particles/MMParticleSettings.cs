namespace MetaMind.Engine.Entities.Particles
{
    using System;

    public class MMParticleSettings : ICloneable
    {
        public int ParticleNum = 1500;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}