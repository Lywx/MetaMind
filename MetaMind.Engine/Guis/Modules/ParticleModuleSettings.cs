namespace MetaMind.Engine.Guis.Modules
{
    using System;

    public class ParticleModuleSettings : ICloneable
    {
        public int ParticleNum = 1500;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}