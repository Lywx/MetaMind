using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Particles
{
    public interface IParticle
    {
        Vector2 Acceleration { get; set; }

        float Angle { get; set; }

        float AngularAcceleration { get; set; }

        float AngularVelocity { get; set; }

        Color Color { get; set; }

        float LastingSeconds { get; set; }

        Vector2 Position { get; set; }

        float Scale { get; set; }

        Vector2 Velocity { get; set; }

        void Draw(GameTime gameTime);

        void Update(GameTime gameTime);
    }

    public abstract class Particle : ShapelessParticle, IParticle
    {
        protected Particle(Vector2 a, Vector2 v, float angle, float angularA, float angularV, float lastingSeconds, Color color, float scale)
            : base(a, v, angle, angularA, angularV, lastingSeconds)
        {
            Color = color;
            this.Scale = scale;
        }

        public Color Color { get; set; }

        public float Scale { get; set; }

        public abstract void Draw(GameTime gameTime);
    }
}