using MetaMind.Engine;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Particles
{
    public interface IAbstractParticle
    {
        float Angle { get; set; }
        float AngularAcceleration { get; set; }
        float AngularVelocity { get; set; }
        Vector2 Position { get; set; }
        Vector2 Acceleration { get; set; }
        Vector2 Velocity { get; set; }
        float LastingSeconds { get; set; }

        void Update(GameTime gameTime);
    }

    public class ShapelessParticle : EngineObject, IAbstractParticle
    {
        #region Particle Movements

        public Vector2 Acceleration { get; set; }

        public float Angle { get; set; }

        public float AngularAcceleration { get; set; }

        public float AngularVelocity { get; set; }

        public Vector2 Position { get; set; }

        public float LastingSeconds { get; set; }

        public Vector2 Velocity { get; set; }

        #endregion Particle Movements

        #region Constructors

        public ShapelessParticle(Vector2 a, Vector2 v, float angle, float angluarA, float angluarV, float lastingSeconds)
        {
            Position = Vector2.Zero;
            Acceleration = a;
            Velocity = v;

            Angle = angle;
            AngularAcceleration = angluarA;
            AngularVelocity = angluarV;

            LastingSeconds = lastingSeconds;
        }

        #endregion Constructors

        #region Update

        public virtual void Update(GameTime gameTime)
        {
            AngularVelocity += (float)gameTime.ElapsedGameTime.TotalSeconds * AngularAcceleration;
            Angle += (float)gameTime.ElapsedGameTime.TotalSeconds * AngularVelocity;

            Velocity += (float)gameTime.ElapsedGameTime.TotalSeconds * Acceleration;
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Velocity;

            LastingSeconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        #endregion Update
    }
}