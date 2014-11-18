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

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class FloatParticle : Particle
    {
        private const int BubbleSeconds = 20;
        private const int Height        = 2;
        private const int Width         = 8;

        private static readonly Random Random = Perseverance.Adventure.Random;

        private Point pressure = new Point(10, 10);
        private int   deep;

        private FloatParticle(int deep, Direction direction, Vector2 a, Vector2 v, float lastingSeconds, Color color, float scale)
            : base(a, v, 0f, 0f, 0f, lastingSeconds, color, scale)
        {
            this.deep = deep;

            // anywhere on the sides of screen
            var width = (int)(Width * this.Scale);
            this.Position = PointExt.ToVector2(new Point(
                    direction == Direction.Left ? -width : GraphicsSettings.Width,
                    Random.Next(GraphicsSettings.Height)));
        }

        private enum Direction
        {
            Left,

            Right,
        }

        public bool IsOutsideScreen
        {
            get
            {
                return Position.X + Width * this.Scale < 0 ||
                       Position.Y + Height * this.Scale < 0 ||
                       Position.X > GraphicsSettings.Width ||
                       Position.Y > GraphicsSettings.Height;
            }
        }

        public static FloatParticle RandParticle()
        {
            var direction = (Direction)Random.Next(2);
            var deep      = Random.Next(1, 10);
            var size      = deep;

            Vector2 acceleration;
            Vector2 velocity;

            switch (direction)
            {
                case Direction.Left:
                    {
                        acceleration = new Vector2(Random.Next(5, 10), 0);
                        velocity = new Vector2(Random.Next(30, 50), 0);
                    }

                    break;

                case Direction.Right:
                    {
                        acceleration = new Vector2(Random.Next(-10, -5), 0);
                        velocity = new Vector2(Random.Next(-50, -30), 0);
                    }

                    break;

                default:
                    {
                        acceleration = Vector2.Zero;
                        velocity = Vector2.Zero;
                    }

                    break;
            }

            var remainingSeconds = Random.Next(BubbleSeconds, 2 * BubbleSeconds);
            var color            = new Color(Random.Next(0, 100) / deep, 50 / deep, 50 / deep, 50 / deep);
            var particle         = new FloatParticle(deep, direction, acceleration, velocity, remainingSeconds, color, size);

            return particle;
        }

        public override void Draw(GameTime gameTime)
        {
            var size = new Vector2(Width * this.Scale, Height * this.Scale);
            ScreenManager.SpriteBatch.FillRectangle(Position, size, Color, Angle);
        }

        public override void Update(GameTime gameTime)
        {
            // smooth disappearance
            if (LastingSeconds < BubbleSeconds)
            {
                this.Scale = Math.Min(LastingSeconds, this.Scale);
            }

            // random water movements
            Acceleration = new Vector2(
                Random.Next(-pressure.X, pressure.X),
                Random.Next(-pressure.Y, pressure.Y));

            base.Update(gameTime);
        }

        public void Colorize()
        {
            Color = new Color(
                Random.Next(0, 255) / deep,
                Random.Next(0, 255) / deep,
                Random.Next(0, 255) / deep,
                Random.Next(0, 255) / deep);
        }
    }
}