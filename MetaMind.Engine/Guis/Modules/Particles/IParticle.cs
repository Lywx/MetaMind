namespace MetaMind.Engine.Guis.Modules.Particles
{
    using Microsoft.Xna.Framework;
    using IDrawable = Engine.IDrawable;

    public interface IParticle : IShapelessParticle, IUpdateable, IDrawable
    {
        Color Color { get; set; }

        float Scale { get; set; }
    }
}