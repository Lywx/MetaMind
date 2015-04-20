// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatParticle.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Particles
{
    using Microsoft.Xna.Framework;
    using Primtives2D;
    using System;

    public class FloatParticle : Particle, IRandomParticle
    {
        #region Particle Data

        private int bubbleSeconds = 20;

        private int deep;

        private int height = 2;

        private Vector2 pressure;

        private Vector2 size;

        private int width = 8;

        #endregion Particle Data

        public FloatParticle(Vector2 position, Vector2 a, Vector2 v, float life, Color color, int deep, float scale)
            : base(position, a, v, 0f, 0f, 0f, life, color, scale)
        {
            this.deep = deep;

            this.size = new Vector2(this.width, this.height) * scale;
            this.pressure = new Vector2(10, 10);
        }

        protected enum FloatDirection
        {
            Left, Right, Up, Down
        }

        public bool IsOutsideScreen
        {
            get
            {
                return this.Position.X + this.size.X < 0 ||
                       this.Position.Y + this.size.Y < 0 ||
                       this.Position.X > ScreenWidth ||
                       this.Position.Y > ScreenHeight;
            }
        }

        public static FloatParticle Prototype()
        {
            return new FloatParticle(Vector2.Zero, Vector2.Zero, Vector2.Zero, 0f, Color.Transparent, 0, 0f);
        }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            gameGraphics.Screen.SpriteBatch.FillRectangle(this.Position, this.size, this.Color, this.Angle);
        }

        public override void Update(GameTime gameTime)
        {
            // smooth disappearance
            if (this.Life < this.bubbleSeconds)
            {
                this.Scale = Math.Min(this.Life, this.Scale);
            }

            this.size = new Vector2(this.width * this.Scale, this.height * this.Scale);

            // random water movements
            this.Acceleration = new Vector2(
                Random.Next((int)-this.pressure.X, (int)this.pressure.X),
                Random.Next((int)-this.pressure.Y, (int)this.pressure.Y));

            base.Update(gameTime);
        }

        #region Particle Configuration

        public FloatParticle ParticleFromBelow(int factor)
        {
            var deep  = Random.Next(1, 5);
            var scale = deep;

            var acceleration   = new Vector2(0, Random.Next(-10, -1));
            var velocity       = new Vector2(0, Random.Next(-80 * factor, -30 * factor));
            var lastingSeconds = Random.Next(this.bubbleSeconds, 2 * this.bubbleSeconds);

            var color = new Color(50 / deep, 50 / deep, Random.Next(0, 50) / deep, 50 / deep);

            // anywhere on the sides of screen
            var x = Random.Next(ScreenWidth);
            var y = ScreenHeight;
            var position = new Vector2(x, y);

            var particle = new FloatParticle(position, acceleration, velocity, lastingSeconds, color, deep, scale);

            return particle;
        }

        public FloatParticle ParticleFromSide(int factor)
        {
            // direction only could be left or right
            var direction = (FloatDirection)Random.Next(2);
            var deep  = Random.Next(1, 10);
            var scale = deep;

            var acceleration   = new Vector2(direction == FloatDirection.Left ? Random.Next(5, 10) : Random.Next(-10, -5), 0);
            var velocity       = new Vector2(direction == FloatDirection.Left ? Random.Next(30 * factor, 50 * factor) : Random.Next(-50 * factor, -30 * factor), 0);
            var lastingSeconds = Random.Next(this.bubbleSeconds, 2 * this.bubbleSeconds);

            var color    = new Color(Random.Next(0, 100) / deep, 50 / deep, 50 / deep, 50 / deep);

            // anywhere on the sides of screen
            var x = direction == FloatDirection.Left ? -(this.width * scale) : ScreenWidth;
            var y = Random.Next(ScreenHeight);
            var position = new Vector2(x, y);

            var particle = new FloatParticle(position, acceleration, velocity, lastingSeconds, color, deep, scale);

            return particle;
        }

        #endregion Particle Configuration

        #region IRandomParticle

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public IRandomParticle Randomize()
        {
            this.bubbleSeconds = 20;
            this.height = 2;
            this.width = 8;

            return this;
        }

        #endregion IRandomParticle
    }
}