// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatParticle.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Particles
{
    using System;

    using MetaMind.Engine.Components.Graphics;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public delegate FloatParticle GenerateParticle(int factor);

    public enum FloatDirection
    {
        Left, Right, Up, Down
    }

    public class FloatParticle : Particle
    {
        public const int BubbleSeconds = 20;

        public static int Height = 2;
        public static int Width = 8;

        public static GenerateParticle Generate;
        public static Random           Random;

        private readonly int deep;

        public FloatParticle(Vector2 position, Vector2 a, Vector2 v, float lastingSeconds, Color color, int deep, float scale)
            : base(a, v, 0f, 0f, 0f, lastingSeconds, color, scale)
        {
            if (Generate == null)
            {
                throw new InvalidOperationException("Generate is not set.");
            }

            if (Random == null)
            {
                throw new InvalidOperationException("Random is not set.");
            }
            
            this.deep     = deep;
            this.Position = position;
            
            this.Size     = new Vector2(Width, Height) * scale;
            this.Pressure = new Vector2(10, 10);
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

        public Vector2 Size { get; private set; }

        public Vector2 Pressure { get; set; }

        public static FloatParticle RandomParticle(int factor)
        {
            return Generate(factor);
        }

        public void Colorize()
        {
            this.Color = new Color(
                Random.Next(0, 255) / this.deep,
                Random.Next(0, 255) / this.deep,
                Random.Next(0, 255) / this.deep,
                Random.Next(0, 255) / this.deep);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.FillRectangle(this.Position, this.Size, this.Color, this.Angle);
        }

        public override void Update(GameTime gameTime)
        {
            // smooth disappearance
            if (this.LastingSeconds < BubbleSeconds)
            {
                this.Scale = Math.Min(this.LastingSeconds, this.Scale);
            }

            this.Size = new Vector2(Width * this.Scale, Height * this.Scale);

            // random water movements
            this.Acceleration = new Vector2(
                Random.Next((int)-this.Pressure.X, (int)this.Pressure.X),
                Random.Next((int)-this.Pressure.Y, (int)this.Pressure.Y));

            base.Update(gameTime);
        }

        #region Particle Configuration

        public static FloatParticle ParticleFromSide(int factor)
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

        public static FloatParticle ParticleFromBelow(int factor)
        {
            var deep  = Random.Next(1, 5);
            var scale = deep;

            var acceleration   = new Vector2(0, Random.Next(-10, -1));
            var velocity       = new Vector2(0, Random.Next(-80 * factor, -30 * factor));
            var lastingSeconds = Random.Next(BubbleSeconds, 2 * BubbleSeconds);

            var color = new Color(50 / deep, 50 / deep, Random.Next(0, 50) / deep, 50 / deep);

            // anywhere on the sides of screen
            var x = Random.Next(GameEngine.GraphicsSettings.Width);
            var y = GameEngine.GraphicsSettings.Height;
            var position = new Vector2(x, y);

            var particle = new FloatParticle(position, acceleration, velocity, lastingSeconds, color, deep, scale);

            return particle;
        }

        #endregion
    }
}