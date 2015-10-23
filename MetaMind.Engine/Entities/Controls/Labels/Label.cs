﻿namespace MetaMind.Engine.Entities.Controls.Labels
{
    using System;
    using Components.Content.Fonts;
    using Graphics;
    using Graphics.Fonts;
    using Microsoft.Xna.Framework;

    public class Label : MMRendererComponent, ICloneable
    {
        #region Constructors and Finalizer

        /// <summary>
        /// Label is the main text display control.
        /// </summary>
        public Label(
            Func<Font> textFont,
            Func<string> text,
            Func<Vector2> anchorLocation,
            Func<Color> textColor,
            Func<float> textSize,
            HoritonalAlignment textHAlignment,
            VerticalAlignment textVAlignment,
            int textLeading = 0,
            bool textMonospaced = false)
            : this(textFont, text, anchorLocation, textColor, textSize)
        {
            this.TextHAlignment = textHAlignment;
            this.TextVAlignment = textVAlignment;
            this.TextLeading    = textLeading;
            this.TextMonospaced = textMonospaced;
        }

        public Label(
            Func<Font> textFont,
            Func<string> text,
            Func<Vector2> anchorLocation,
            Func<Color> textColor,
            Func<float> textSize)
            : this()
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (anchorLocation == null)
            {
                throw new ArgumentNullException(nameof(anchorLocation));
            }

            if (textSize == null)
            {
                throw new ArgumentNullException(nameof(textSize));
            }

            if (textFont == null)
            {
                throw new ArgumentNullException(nameof(textFont));
            }

            this.Text           = text;
            this.TextSize       = textSize;
            this.TextColor      = textColor;
            this.TextFont       = textFont;
            this.AnchorLocation = anchorLocation;
        }

        public Label()
        {
            this.Name = "Label";

            this.TextMeasureSelector = () => this.TextMonospaced
                ? (Func<Vector2>)
                  (() => this.TextFont().MeasureMonospacedString(this.Text(), this.TextSize()))
                : (() => this.TextFont().MeasureString(this.Text(), this.TextSize()));

            this.DrawActionSelector = () => this.TextMonospaced
                ? (Action<Font, string, Vector2, Color, float, HoritonalAlignment, VerticalAlignment, int>)
                  ((font, str, position, color, scale, halignment, valignment, leading) => this.Graphics.Renderer.DrawMonospacedString(font, str, position, color, scale, halignment, valignment, leading))
                : ((font, str, position, color, scale, halignment, valignment, leading) => this.Graphics.Renderer.DrawString          (font, str, position, color, scale, halignment, valignment, leading));
        }

        #endregion

        #region Text Data

        /// <remarks>
        ///     Consistent with Control.Location initialization.
        /// </remarks>
        public Func<Vector2> AnchorLocation { get; set; } = () => new Vector2(int.MinValue, int.MinValue);

        public Func<string> Text { get; set; } = () => "";

        public Func<Color> TextColor { get; set; } = () => Color.Transparent;

        public Func<float> TextSize { get; set; } = () => 0f;

        public Func<Font> TextFont { get; set; } = () => null;

        public HoritonalAlignment TextHAlignment { get; set; } = HoritonalAlignment.Right;

        public VerticalAlignment TextVAlignment { get; set; } = VerticalAlignment.Bottom;

        public int TextLeading { get; set; } = 0;

        public bool TextMonospaced { get; set; } = false;

        protected Func<Vector2> TextMeasure => this.TextMeasureSelector();

        protected Func<Func<Vector2>> TextMeasureSelector { get; }

        #endregion

        #region Control Data

        public Rectangle Rectangle
        {
            get
            {
                var anchorLocation = this.AnchorLocation();
                var location = new Vector2();
                var size = this.TextMeasure();

                switch (this.TextHAlignment)
                {
                    case HoritonalAlignment.Left:
                    {
                        location.X = anchorLocation.X - size.X;

                        break;
                    }

                    case HoritonalAlignment.Center:
                    {
                        location.X = anchorLocation.X - size.X / 2;

                        break;
                    }

                    case HoritonalAlignment.Right:
                    {
                        location.X = anchorLocation.X;

                        break;
                    }

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (this.TextVAlignment)
                {
                    case VerticalAlignment.Top:
                    {
                        location.Y = anchorLocation.Y - size.Y;
                        break;
                    }
                    case VerticalAlignment.Center:
                    {
                        location.Y = anchorLocation.Y - size.Y / 2;
                        break;
                    }
                    case VerticalAlignment.Bottom:
                    {
                        location.Y = anchorLocation.Y;
                        break;
                    }

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return new Rectangle(location.ToPoint(), size.ToPoint());
            }

            set { throw new InvalidOperationException(); }
        }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion

        #region Draw

        protected Action<Font, string, Vector2, Color, float, HoritonalAlignment, VerticalAlignment, int> DrawAction => this.DrawActionSelector();

        protected Func<Action<Font, string, Vector2, Color, float, HoritonalAlignment, VerticalAlignment, int>> DrawActionSelector { get; }

        public override void Draw(GameTime time)
        {
            this.DrawAction?.Invoke(
                this.TextFont(),
                this.Text(),
                this.AnchorLocation(),
                this.TextColor().MakeTransparent(this.MixedMinOpacity(alpha)),
                this.TextSize(),
                this.TextHAlignment,
                this.TextVAlignment,
                this.TextLeading);
        }

        #endregion

        #region IClonable

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}