namespace MetaMind.Engine.Guis.Modules
{
    using System;

    public class ParticleSettings : ICloneable
    {
        public int ParticleNum = 1500;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}