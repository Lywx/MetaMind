namespace MetaMind.Engine.Guis.Widgets.Visuals
{
    using System;

    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;

    public class LabelSettings : ICloneable
    {
        public float Size;

        public Color Color;

        public Font Font;

        public Func<Vector2> Position;

        public Func<string> Text;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}