namespace MetaMind.Engine.Guis.Particles
{
    using System;

    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class ShapelessParticle : GameVisualEntity, IShapelessParticle, IParameterLoader<GraphicsSettings>  
    {
        #region Particle Movements

        public Vector2 Acceleration { get; set; }

        public float Angle { get; set; }

        public float AngularAcceleration { get; set; }

        public float AngularVelocity { get; set; }

        public Vector2 Position { get; set; }

        public float Life { get; set; }

        public Vector2 Velocity { get; set; }

        #endregion Particle Movements

        #region Dependency

        protected Random Random { get; private set; }

        #endregion

        #region Parameters

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.ViewportWidth  = parameter.Width;
            this.ViewportHeight = parameter.Height;
        }

        protected int ViewportHeight { get; set; }

        protected int ViewportWidth { get; set; }
        
        #endregion

        #region Constructors

        public ShapelessParticle()
        {
            // Parameters
            this.LoadParameter(this.EngineGraphics.Settings);

            // Dependency
            this.Random = this.Numerical.Random;
        }

        public ShapelessParticle(Vector2 position, Vector2 a, Vector2 v, float angle, float angluarA, float angluarV, float life)
            : this()
        {
            this.Position     = position;
            this.Acceleration = a;
            this.Velocity     = v;

            this.Angle               = angle;
            this.AngularAcceleration = angluarA;
            this.AngularVelocity     = angluarV;

            this.Life = life;
        }

        #endregion Constructors

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.AngularVelocity += (float)time.ElapsedGameTime.TotalSeconds * this.AngularAcceleration;
            this.Angle           += (float)time.ElapsedGameTime.TotalSeconds * this.AngularVelocity;

            this.Velocity += (float)time.ElapsedGameTime.TotalSeconds * this.Acceleration;
            this.Position += (float)time.ElapsedGameTime.TotalSeconds * this.Velocity;

            this.Life -= (float)time.ElapsedGameTime.TotalSeconds;
        }

        #endregion Update
    }
}