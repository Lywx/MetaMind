namespace MetaMind.Engine.Entities.Controls.Images
{
    using System;
    using Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ImageBox : MMRenderComponent
    {
        #region Constructors and Finalizer

        public ImageBox(
            Func<Texture2D> imageSelector, 
            Func<Rectangle> boundsSelector,
            Func<Color> colorSelector)
        {
            if (imageSelector == null)
            {
                throw new ArgumentNullException(nameof(imageSelector));
            }

            if (boundsSelector == null)
            {
                throw new ArgumentNullException(nameof(boundsSelector));
            }

            if (colorSelector == null)
            {
                throw new ArgumentNullException(nameof(colorSelector));
            }

            this.imageSelector = imageSelector;
            this.boundsSelector = boundsSelector;
            this.colorSelector = colorSelector;
        }

        public ImageBox(MMControlManager manager, Texture2D image, Rectangle bound, Color color) 
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }
            
            this.image  = image;
            this.Bounds = bound;
            this.color  = color;
        }

        #endregion

        #region Geometry Data

        private Rectangle sourceRectangle = new Rectangle(int.MinValue, int.MinValue, 0, 0);

        public Rectangle SourceRectangle
        {
            get { return this.sourceRectangle; }
            set
            {
                var l = value.Left;
                var t = value.Top;
                var w = value.Width;
                var h = value.Height;

                if (l < 0)
                {
                    l = 0;
                }

                if (t < 0)
                {
                    t = 0;
                }

                if (w > this.Image.Width)
                {
                    w = this.Image.Width;
                }

                if (h > this.Image.Height)
                {
                    h = this.Image.Height;
                }

                if (l + w > this.Image.Width)
                {
                    w = this.Image.Width - l;
                }

                if (t + h > this.Image.Height)
                {
                    h = this.Image.Height - t;
                }

                this.sourceRectangle = new Rectangle(l, t, w, h);
            }
        }

        private Func<Rectangle> boundsSelector;

        public sealed override Rectangle Bounds => this.boundsSelector?.Invoke() ?? base.Bounds;

        #endregion

        #region Render Data

        private Texture2D image;

        private Func<Texture2D> imageSelector;

        public Texture2D Image => this.imageSelector?.Invoke() ?? this.image;

        private Color color;

        private Func<Color> colorSelector;

        public Color Color(byte alpha) => this.colorSelector?.Invoke().MakeTransparent(alpha)
                        ?? this.color.MakeTransparent(alpha); 

        public ImageMode ImageMode { get; set; } = ImageMode.Normal;

        #endregion

        #region Draw

        public override void Draw(GameTime time)
        {
            switch (this.ImageMode)
            {
                case ImageMode.Normal:
                {
                    graphics.Renderer.Draw(
                        this.Image,
                        this.X,
                        this.Y,
                        this.SourceRectangle,
                        this.Color(this.MixedMinOpacity(alpha)),
                        0f);

                    break;
                }

                case ImageMode.Stretched:
                {
                    graphics.Renderer.Draw(
                        this.Image,
                        this.Bounds,
                        this.SourceRectangle,
                        this.Color(this.MixedMinOpacity(alpha)),
                        0f);

                    break;
                }
            }
        }

        #endregion
    }
}