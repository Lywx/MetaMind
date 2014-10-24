using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Particles
{
    public interface IParticle
    {
        Color Color { get; set; }
        float Size { get; set; }

        float Angle { get; set; }
        float AngularAcceleration { get; set; }
        float AngularVelocity { get; set; }
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 Acceleration { get; set; }
        float LastingSeconds { get; set; }

        void Draw(GameTime gameTime);

        void Update(GameTime gameTime);
    }

    public abstract class Particle : ShapelessParticle, IParticle
    {
        #region Graphical Data

        public Color Color { get; set; }

        public float Size { get; set; }

        #endregion Graphical Data

        protected Particle( Vector2 a, Vector2 v, float angle, float angularA, float angularV, float lastingSeconds, Color color, float size ) :
            base( a, v, angle, angularA, angularV, lastingSeconds )
        {
            Color = color;
            Size = size;
        }

        public abstract void Draw( GameTime gameTime );
    }
}