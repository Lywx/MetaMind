namespace MetaMind.Engine.Entities.Particles
{
    using Entities;
    using Microsoft.Xna.Framework;

    public interface IMMVisualParticle : IMMParticle, IUpdateable, IMMDrawable
    {
        Color Color { get; set; }

        float Scale { get; set; }
    }
}