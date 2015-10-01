namespace MetaMind.Engine.Gui.Modules.Particle
{
    using Entities;
    using Microsoft.Xna.Framework;

    public interface IParticle : IShapelessParticle, IUpdateable, IMMDrawable
    {
        Color Color { get; set; }

        float Scale { get; set; }
    }
}