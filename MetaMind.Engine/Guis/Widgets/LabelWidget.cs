namespace MetaMind.Engine.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class LabelWidget : GameVisualEntity
    {
        public Func<string> Text { get; set; }

        public Func<Vector2> TextPosition { get; set; }

        public Func<Color> TextColor { get; set; }

        public Func<float> TextSide { get; set; }

        public Func<Font> TextFont { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            var font = graphics.String;

            font.DrawStringCenteredHV(
                this.TextFont(),
                this.Text(),
                this.TextPosition(),
                this.TextColor(),
                this.TextSide());
        }
    }
}