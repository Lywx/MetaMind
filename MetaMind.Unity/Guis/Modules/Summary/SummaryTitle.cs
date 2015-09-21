namespace MetaMind.Unity.Guis.Modules.Summary
{
    using System;
    using Engine.Component.Graphics.Font;
    using Engine.Gui.Control.Visuals;
    using Microsoft.Xna.Framework;

    public class SummaryTitle : Label
    {
        public SummaryTitle(Func<Font> font, Func<string> text, Func<Vector2> pos, Func<Color> color, Func<float> size)
            : base(font, text, pos, color, size)
        {
            this.TextHAlignment = HoritonalAlignment.Center;
            this.TextVAlignment = VerticalAlignment.Center;
        }
    }
}