using System;

namespace MetaMind.Perseverance.Guis.Modules.Synchronization
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public class SynchronizationLabel : Label
    {
        public SynchronizationLabel(Func<Font> textFont, Func<string> text, Func<Vector2> textPosition, Func<Color> textColor, Func<float> textSize, StringHAlign textHAlign, StringVAlign textVAlign, bool textMonospaced)
            : base(textFont, text, textPosition, textColor, textSize, textHAlign, textVAlign, textMonospaced)
        {
        }

        public SynchronizationLabel(Func<Font> textFont, Func<string> text, Func<Vector2> textPosition, Func<Color> textColor, Func<float> textSize)
            : base(textFont, text, textPosition, textColor, textSize)
        {
        }
    }
}
