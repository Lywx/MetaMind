namespace MetaMind.Engine.Gui.Control.Button
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Glyph
    {
        #region Constructors and Finalizer

        public Glyph(Texture2D texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            this.Texture = texture;
        }

        public Glyph(Texture2D texture, Rectangle rectangle) : this(texture)
        {
            this.Rectangle = rectangle;
        }

        #endregion

        public Texture2D Texture { get; set; }

        public GlyphMode Mode { get; set; } = GlyphMode.Stretched;

        public Color Color { get; set; } = Color.White;

        public Point Offset { get; set; } = Point.Zero;

        public Rectangle Rectangle { get; set; } = Rectangle.Empty;
    }
}
