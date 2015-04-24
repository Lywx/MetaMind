namespace MetaMind.Runtime.Guis.Modules.Summary
{
    using System;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Visual;

    using Microsoft.Xna.Framework;

    public class SummaryTitle : Label
    {
        public SummaryTitle(Func<Font> font, Func<string> text, Func<Vector2> pos, Func<Color> color, Func<float> size)
            : base(font, text, pos, color, size)
        {
            this.TextHAlign = StringHAlign.Center;
            this.TextVAlign = StringVAlign.Center;
        }
    }
}