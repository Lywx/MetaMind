namespace MetaMind.Engine.Guis.Particles
{
    using Microsoft.Xna.Framework;

    public interface IShapelessParticle : IUpdateable
    {
        float Angle { get; set; }

        float AngularAcceleration { get; set; }

        float AngularVelocity { get; set; }

        Vector2 Position { get; set; }

        Vector2 Acceleration { get; set; }

        Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets second of particle lifespan.
        /// </summary>
        float Life { get; set; }
    }
}