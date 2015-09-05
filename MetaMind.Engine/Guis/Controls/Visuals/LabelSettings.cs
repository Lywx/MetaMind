namespace MetaMind.Engine.Guis.Widgets.Visuals
{
    using System;
    using Components.Fonts;
    using Microsoft.Xna.Framework;

    public class LabelSettings : ICloneable
    {
        public Color TextColor;

        public Font TextFont;

        public StringHAlign TextHAlign;

        public StringVAlign TextVAlign;

        public bool TextMonospaced;

        public float TextSize;

        public Func<string> Text = () => "";

        public Func<Vector2> TextPosition;

        public int TextLeading;

        public LabelSettings()
        {
            this.TextHAlign = StringHAlign.Right;
            this.TextVAlign = StringVAlign.Center;

            this.TextMonospaced = false;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}