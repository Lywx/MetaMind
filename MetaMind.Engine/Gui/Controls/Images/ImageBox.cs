namespace MetaMind.Engine.Gui.Controls.Images
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Service;

    public class ImageBox : Control
    {
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

        #region Constructors and Finalizer

        public ImageBox(
            ControlManager manager,
            Func<Texture2D> imageSelector, 
            Func<Rectangle> boundsSelector,
            Func<Color> colorSelector) : base(manager)
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

        public ImageBox(ControlManager manager, Texture2D image, Rectangle bound, Color color) 
            : base(manager)
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

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
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
                        this.Color(this.MixedAlpha(alpha)),
                        0f);

                    break;
                }

                case ImageMode.Stretched:
                {
                    graphics.Renderer.Draw(
                        this.Image,
                        this.Bounds,
                        this.SourceRectangle,
                        this.Color(this.MixedAlpha(alpha)),
                        0f);

                    break;
                }
            }
        }

        #endregion
    }
}