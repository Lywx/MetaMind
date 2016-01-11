namespace MetaMind.Engine.Entities.Particles
{
    using Bases;
    using Microsoft.Xna.Framework;

    public interface IMMVisualParticle : IMMParticle, IMMUpdateable, IMMDrawable
    {
        Color Color { get; set; }

        float Scale { get; set; }
    }
}