namespace MetaMind.Unity.Guis.Modules.Summary
{
    using System;
    using Engine;
    using Engine.Service;
    using Microsoft.Xna.Framework;
    using Primtives2D;

    public class SummarySplit : GameVisualEntity
    {
        public SummarySplit(Func<Vector2> start, Func<Vector2> end, Func<Color> color, Func<float> size)
        {
            this.Start = start;
            this.End   = end;
            this.Color = color;
            this.Size  = size;
        }

        public Func<Color> Color { get; private set; }

        public Func<Vector2> Start { get; set; }

        public Func<Vector2> End { get; set; }

        public Func<float> Size { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            var spriteBatch = graphics.SpriteBatch;
            spriteBatch.DrawLine(this.Start(), this.End(), this.Color().MakeTransparent(alpha), this.Size());
        }
    }
}