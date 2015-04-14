namespace MetaMind.Engine.Guis.Particles
{
    using Microsoft.Xna.Framework;

    using IDrawable = MetaMind.Engine.IDrawable;
    using IUpdateable = MetaMind.Engine.IUpdateable;

    public interface IParticle : IShapelessParticle, IUpdateable, IDrawable
    {
        Color Color { get; set; }

        float Scale { get; set; }
    }
}