namespace MetaMind.Engine.Entities.Particles
{
    using System;

    public interface IMMRandomParticle : ICloneable
    {
        IMMRandomParticle Randomize();
    }
}