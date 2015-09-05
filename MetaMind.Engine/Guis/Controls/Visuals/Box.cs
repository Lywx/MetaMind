namespace MetaMind.Engine.Guis.Controls.Visuals
{
    using System;
    using Engine;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Primtives2D;
    using Services;

    public class Box : GameVisualEntity
    {
        public Box(Func<Rectangle> bounds, Func<Color> color, Func<bool> colorFilled)
        {
            this.Bounds = bounds;
            this.Color       = color;
            this.ColorFilled = colorFilled;
        }

        public Func<Rectangle> Bounds { get; set; }

        public Func<Color> Color { get; set; }

        public Func<bool> ColorFilled { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            var draw = this.ColorFilled()
                           ? Primitives2D.FillRectangle
                           : new Action<SpriteBatch, Rectangle, Color>(Primitives2D.DrawRectangle);

            draw(graphics.SpriteBatch, this.Bounds(), this.Color().MakeTransparent(alpha));
        }
    }
}