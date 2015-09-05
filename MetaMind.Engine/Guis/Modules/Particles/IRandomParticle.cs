namespace MetaMind.Engine.Guis.Modules.Particles
{
    using System;

    public interface IRandomParticle : ICloneable
    {
        IRandomParticle Randomize();
    }
}