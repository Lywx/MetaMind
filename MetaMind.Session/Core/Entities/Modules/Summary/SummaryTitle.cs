namespace MetaMind.Session.Guis.Modules.Summary
{
    using System;
    using Engine.Core.Backend.Content.Fonts;
    using Engine.Core.Entity.Control.Labels;
    using Engine.Core.Entity.Graphics.Fonts;
    using Microsoft.Xna.Framework;

    public class SummaryTitle : Label
    {
        public SummaryTitle(Func<MMFont> font, Func<string> text, Func<Vector2> pos, Func<Color> color, Func<float> size)
            : base(font, text, pos, color, size)
        {
            this.TextHAlignment = HoritonalAlignment.Center;
            this.TextVAlignment = VerticalAlignment.Center;
        }
    }
}