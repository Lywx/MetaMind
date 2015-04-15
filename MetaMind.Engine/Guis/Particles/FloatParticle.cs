// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatParticle.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Particles
{
    using System;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class FloatParticle : Particle
    {
        public const int BubbleSeconds = 20;

        public static int Height = 2;

        public static int Width = 8;

        private readonly int deep;

        public FloatParticle(Vector2 position, Vector2 a, Vector2 v, float life, Color color, int deep, float scale)
            : base(position, a, v, 0f, 0f, 0f, life, color, scale)
        {
            this.deep = deep;

            this.Size = new Vector2(Width, Height) * scale;
            this.Pressure = new Vector2(10, 10);
        }

        protected enum FloatDirection
        {
            Left, Right, Up, Down
        }

        public bool IsOutsideScreen
        {
            get
            {
                return this.Position.X + this.Size.X < 0 ||
                       this.Position.Y + this.Size.Y < 0 ||
                       this.Position.X > GameEngine.GraphicsSettings.Width ||
                       this.Position.Y > GameEngine.GraphicsSettings.Height;
            }
        }

        public Vector2 Pressure { get; set; }

        public Vector2 Size { get; private set; }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            var spriteBatch = gameGraphics.Screens.SpriteBatch;
            spriteBatch.FillRectangle(this.Position, this.Size, this.Color, this.Angle);
        }

        public override void Update(GameTime gameTime)
        {
            // smooth disappearance
            if (this.Life < BubbleSeconds)
            {
                this.Scale = Math.Min(this.Life, this.Scale);
            }

            this.Size = new Vector2(Width * this.Scale, Height * this.Scale);

            // random water movements
            var random = GameEngineService.Random;
            this.Acceleration = new Vector2(
                random.Next((int)-this.Pressure.X, (int)this.Pressure.X),
                random.Next((int)-this.Pressure.Y, (int)this.Pressure.Y));

            base.Update(gameTime);
        }

        #region Particle Configuration

        public FloatParticle ParticleFromBelow(int factor)
        {
            var random = GameEngineService.Random;

            var deep  = random.Next(1, 5);
            var scale = deep;

            var acceleration   = new Vector2(0, random.Next(-10, -1));
            var velocity       = new Vector2(0, random.Next(-80 * factor, -30 * factor));
            var lastingSeconds = random.Next(BubbleSeconds, 2 * BubbleSeconds);

            var color = new Color(50 / deep, 50 / deep, random.Next(0, 50) / deep, 50 / deep);

            // anywhere on the sides of screen
            var x = random.Next(GameEngine.GraphicsSettings.Width);
            var y = GameEngine.GraphicsSettings.Height;
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
            var lastingSeconds = Random.Next(BubbleSeconds, 2 * BubbleSeconds);

            var color    = new Color(Random.Next(0, 100) / deep, 50 / deep, 50 / deep, 50 / deep);

            // anywhere on the sides of screen
            var x = direction == FloatDirection.Left ? -(Width * scale) : GameEngine.GraphicsSettings.Width;
            var y = Random.Next(GameEngine.GraphicsSettings.Height);
            var position = new Vector2(x, y);

            var particle = new FloatParticle(position, acceleration, velocity, lastingSeconds, color, deep, scale);

            return particle;
        }
        #endregion Particle Configuration
    }
}