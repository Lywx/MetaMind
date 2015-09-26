namespace MetaMind.Engine.Gui.Modules.Particle
{
    using Microsoft.Xna.Framework;

    public abstract class Particle : ShapelessParticle, IParticle
    {
        protected Particle(Vector2 position, Vector2 a, Vector2 v, float angle, float angularA, float angularV, float life, Color color, float scale)
            : base(position, a, v, angle, angularA, angularV, life)
        {
            this.Color = color;
            this.Scale = scale;
        }

        public Color Color { get; set; }

        public float Scale { get; set; }
    }
}