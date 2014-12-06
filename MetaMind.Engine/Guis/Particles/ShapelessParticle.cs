namespace MetaMind.Engine.Guis.Particles
{
    using Microsoft.Xna.Framework;

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
            this.Position = Vector2.Zero;
            this.Acceleration = a;
            this.Velocity = v;

            this.Angle = angle;
            this.AngularAcceleration = angluarA;
            this.AngularVelocity = angluarV;

            this.LastingSeconds = lastingSeconds;
        }

        #endregion Constructors

        #region Update

        public virtual void Update(GameTime gameTime)
        {
            this.AngularVelocity += (float)gameTime.ElapsedGameTime.TotalSeconds * this.AngularAcceleration;
            this.Angle += (float)gameTime.ElapsedGameTime.TotalSeconds * this.AngularVelocity;

            this.Velocity += (float)gameTime.ElapsedGameTime.TotalSeconds * this.Acceleration;
            this.Position += (float)gameTime.ElapsedGameTime.TotalSeconds * this.Velocity;

            this.LastingSeconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        #endregion Update
    }
}