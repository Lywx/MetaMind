namespace MetaMind.Unity.Guis.Modules.Summary
{
    using System;
    using Engine.Components.Fonts;
    using Engine.Guis.Widgets.Visuals;
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