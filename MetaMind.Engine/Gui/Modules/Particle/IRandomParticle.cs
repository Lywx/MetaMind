namespace MetaMind.Engine.Gui.Modules.Particle
{
    using System;

    public interface IRandomParticle : ICloneable
    {
        IRandomParticle Randomize();
    }
}