namespace MetaMind.Engine.Core.Entity.Particles
{
    using System;

    public interface IMMRandomParticle : ICloneable
    {
        IMMRandomParticle Randomize();
    }
}