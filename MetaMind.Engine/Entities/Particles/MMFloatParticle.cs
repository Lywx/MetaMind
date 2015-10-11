namespace MetaMind.Engine.Entities.Particles
{
    using System;
    using Microsoft.Xna.Framework;
    using Primtives2D;

    public class MMFloatParticle : MMVisualParticle, IMMRandomParticle
    {
        #region Particle Data

        private readonly int BubbleSeconds = 20;

        private readonly int Height = 2;

        private readonly int Width = 8;

        #endregion Particle Data

        private MMFloatParticle(Vector2 position, Vector2 a, Vector2 v, float life, Color color, int deep, float scale)
            : base(position, a, v, 0f, 0f, 0f, life, color, scale)
        {
            this.Deep = deep;

            this.Size     = new Vector2(this.Width, this.Height) * this.Scale;
            this.Pressure = new Vector2(10, 10);
        }

        private enum FloatDirection
        {
            Left, Right, Up, Down
        }

        public bool IsOutsideScreen => this.Position.X + this.Size.X < 0 ||
                                       this.Position.Y + this.Size.Y < 0 ||
                                       this.Position.X > this.ViewportWidth ||
                                       this.Position.Y > this.ViewportHeight;

        private int Deep { get; set; }

        private Vector2 Pressure { get; set; }

        private Vector2 Size { get; set; }

        public static MMFloatParticle Prototype()
        {
            return new MMFloatParticle(Vector2.Zero, Vector2.Zero, Vector2.Zero, 0f, Color.Transparent, 0, 0f);
        }

        public override void Draw(GameTime time)
        {
            this.SpriteBatch.FillRectangle(this.Position, this.Size, this.Color, this.Angle);
        }

        public override void Update(GameTime time)
        {
            // smooth disappearance
            if (this.Life < this.BubbleSeconds)
            {
                this.Scale = Math.Min(this.Life, this.Scale);
            }

            this.Size = new Vector2(this.Width * this.Scale, this.Height * this.Scale);

            // random water movements
            this.Acceleration = new Vector2(
                this.Random.Next((int)-this.Pressure.X, (int)this.Pressure.X),
                this.Random.Next((int)-this.Pressure.Y, (int)this.Pressure.Y));

            base.Update(time);
        }

        #region IRandomParticle

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public IMMRandomParticle Randomize()
        {
            // direction only could be left or right
            var direction = (FloatDirection)this.Random.Next(2);

            this.Deep = this.Random.Next(1, 10);
            this.Scale = this.Deep;

            this.Acceleration = new Vector2(direction == FloatDirection.Left ? this.Random.Next(5, 10) : this.Random.Next(-10, -5), 0);
            this.Velocity     = new Vector2(direction == FloatDirection.Left ? this.Random.Next(30, 50) : this.Random.Next(-50, -30), 0);

            this.Life = this.Random.Next(this.BubbleSeconds, 2 * this.BubbleSeconds);
            this.Color = new Color(this.Random.Next(0, 100) / this.Deep, 50 / this.Deep, 50 / this.Deep, 50 / this.Deep);

            // anywhere on the sides of screen
            var x = direction == FloatDirection.Left ? -(this.Width * this.Scale) : this.ViewportWidth;
            var y = this.Random.Next(this.ViewportHeight);
            this.Position = new Vector2(x, y);

            return this;
        }

        #endregion IRandomParticle
    }
}