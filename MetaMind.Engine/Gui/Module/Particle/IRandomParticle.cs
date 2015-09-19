namespace MetaMind.Engine.Gui.Module.Particle
{
    using System;

    public interface IRandomParticle : ICloneable
    {
        IRandomParticle Randomize();
    }
}