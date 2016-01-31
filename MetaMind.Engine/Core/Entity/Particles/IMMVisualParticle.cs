namespace MetaMind.Engine.Core.Entity.Particles
{
    using Entity.Common;
    using Microsoft.Xna.Framework;

    public interface IMMVisualParticle : IMMParticle, IMMUpdateable, IMMDrawable
    {
        Color Color { get; set; }

        float Scale { get; set; }
    }
}