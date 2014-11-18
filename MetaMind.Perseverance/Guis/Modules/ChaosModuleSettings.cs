namespace MetaMind.Perseverance.Guis.Modules
{
    using System;

    public class ChaosModuleSettings : ICloneable
    {
        public int ParticleNum = 1500;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}