namespace MetaMind.Engine.Guis.Particles
{
    using System;

    public interface IRandomParticle : ICloneable
    {
        IRandomParticle Randomize();
    }
}