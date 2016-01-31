namespace MetaMind.Engine.Core.Entity.Control.Images
{
    using System;
    using Entity.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Primtives2D;

    public class ColorBox : MMRendererComponent
    {
        public ColorBox(
            Func<Rectangle> boundsSelector,
            Func<Color> colorSelector)
        {
            if (boundsSelector == null)
            {
                throw new ArgumentNullException(nameof(boundsSelector));
            }

            if (colorSelector == null)
            {
                throw new ArgumentNullException(nameof(colorSelector));
            }

            this.BoundsSelector = boundsSelector;
            this.ColorSelector = colorSelector;
        }

        public Func<Rectangle> BoundsSelector { get; set; }

        public Func<Color> ColorSelector { get; set; }

        public bool ColorFilled { get; set; } = true;

        public override void Draw(GameTime time)
        {
            var draw = this.ColorFilled
                           ? Primitives2D.FillRectangle
                           : new Action<SpriteBatch, Rectangle, Color>(Primitives2D.DrawRectangle);

            draw(
                this.GlobalGraphicsDeviceController.SpriteBatch,
                this.BoundsSelector(),
                this.ColorSelector().MakeTransparent(this.Opacity.Blend));
        }
    }
}