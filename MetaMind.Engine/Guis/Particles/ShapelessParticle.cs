namespace MetaMind.Engine.Guis.Particles
{
    using System;

    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class ShapelessParticle : GameVisualEntity, IShapelessParticle, IConfigurationParameterLoader<GraphicsSettings>  
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

        #region Service

        private static bool isFlyweightServiceLoaded;

        private static bool isFlyweightParameterLoaded;

        protected static Random Random { get; private set; }

        #endregion

        #region Parameters

        public void ParameterLoad(GraphicsSettings parameter)
        {
            ScreenWidth  = parameter.Width;
            ScreenHeight = parameter.Height;
        }

        protected static int ScreenHeight { get; set; }

        protected static int ScreenWidth { get; set; }

        #endregion

        #region Constructors

        public ShapelessParticle(IGameNumerical gameNumerical, IGameGraphics gameGraphics)
        {
            // Service
            if (!isFlyweightServiceLoaded)
            {
                Random = gameNumerical.Random;

                isFlyweightServiceLoaded = true;
            }

            // Parameters
            if (!isFlyweightParameterLoaded)
            {
                this.ParameterLoad(gameGraphics.Settings);

                isFlyweightParameterLoaded = true;
            }
        }

        public ShapelessParticle(Vector2 position, Vector2 a, Vector2 v, float angle, float angluarA, float angluarV, float life)
            : this(GameEngine.Service.GameNumerical, GameEngine.Service.GameGraphics)
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

        public override void Update(GameTime gameTime)
        {
            this.AngularVelocity += (float)gameTime.ElapsedGameTime.TotalSeconds * this.AngularAcceleration;
            this.Angle           += (float)gameTime.ElapsedGameTime.TotalSeconds * this.AngularVelocity;

            this.Velocity += (float)gameTime.ElapsedGameTime.TotalSeconds * this.Acceleration;
            this.Position += (float)gameTime.ElapsedGameTime.TotalSeconds * this.Velocity;

            this.Life -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        #endregion Update
    }
}