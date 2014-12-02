// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatParticle.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Particles
{
    using System;

    using C3.Primtive2DXna;

    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public delegate FloatParticle GenerateParticle();

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

        private Vector2 pressure = new Vector2(10, 10);

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
            
            this.deep = deep;
            this.Size = new Vector2(Width, Height) * scale;
            this.Position = position;
        }

        public bool IsOutsideScreen
        {
            get
            {
                return Position.X + this.Size.X < 0 ||
                       Position.Y + this.Size.Y < 0 ||
                       Position.X > GraphicsSettings.Width ||
                       Position.Y > GraphicsSettings.Height;
            }
        }

        public Vector2 Size { get; private set; }

        public static FloatParticle RandomParticle()
        {
            return Generate();
        }

        public void Colorize()
        {
            this.Color = new Color(
                Random.Next(0, 255) / deep,
                Random.Next(0, 255) / deep,
                Random.Next(0, 255) / deep,
                Random.Next(0, 255) / deep);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.FillRectangle(this.Position, this.Size, this.Color, this.Angle);
        }

        public override void Update(GameTime gameTime)
        {
            // smooth disappearance
            if (LastingSeconds < BubbleSeconds)
            {
                this.Scale = Math.Min(LastingSeconds, this.Scale);
            }

            this.Size = new Vector2(Width * this.Scale, Height * this.Scale);

            // random water movements
            this.Acceleration = new Vector2(
                Random.Next((int)-pressure.X, (int)pressure.X),
                Random.Next((int)-pressure.Y, (int)pressure.Y));

            base.Update(gameTime);
        }

        #region Particle Configuration

        public static FloatParticle ParticleFromSide()
        {
            // direction only could be left or right 
            var direction = (FloatDirection)Random.Next(2);
            var deep  = Random.Next(1, 10);
            var scale = deep;

            var acceleration   = new Vector2(direction == FloatDirection.Left ? Random.Next(5, 10) : Random.Next(-10, -5), 0);
            var velocity       = new Vector2(direction == FloatDirection.Left ? Random.Next(30, 50) : Random.Next(-50, -30), 0);
            var lastingSeconds = Random.Next(BubbleSeconds, 2 * BubbleSeconds);

            var color    = new Color(Random.Next(0, 100) / deep, 50 / deep, 50 / deep, 50 / deep);

            // anywhere on the sides of screen
            var x = direction == FloatDirection.Left ? -(Width * scale) : GraphicsSettings.Width;
            var y = Random.Next(GraphicsSettings.Height);
            var position = new Vector2(x, y);

            var particle = new FloatParticle(position, acceleration, velocity, lastingSeconds, color, deep, scale);

            return particle;
        }

        public static FloatParticle ParticleFromBelow()
        {
            var deep  = Random.Next(1, 5);
            var scale = deep;

            var acceleration   = new Vector2(0, Random.Next(-10, -1));
            var velocity       = new Vector2(0, Random.Next(-80, -30));
            var lastingSeconds = Random.Next(BubbleSeconds, 2 * BubbleSeconds);

            var color = new Color(50 / deep, 50 / deep, Random.Next(0, 50) / deep, 50 / deep);

            // anywhere on the sides of screen
            var x = Random.Next(GraphicsSettings.Width);
            var y = GraphicsSettings.Height;
            var position = new Vector2(x, y);

            var particle = new FloatParticle(position, acceleration, velocity, lastingSeconds, color, deep, scale);

            return particle;
        }

        #endregion
    }
}