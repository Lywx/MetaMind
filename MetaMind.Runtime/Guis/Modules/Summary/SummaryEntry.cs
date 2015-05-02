namespace MetaMind.Runtime.Guis.Modules.Summary
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Visual;
    using MetaMind.Engine.Guis.Widgets.Visuals;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class SummaryEntry : GameVisualEntity
    {
        public SummaryEntry(Func<Font> font, Func<string> caption, Func<Vector2> captionPos, Func<string> statistics, Func<Vector2> statisticsPos, Func<Color> color, Func<float> size)
        {
            this.Caption    = new Label(font, caption   , captionPos   , color, size);
            this.Statistics = new Label(font, statistics, statisticsPos, color, size)
                                  {
                                      TextHAlign = StringHAlign.Left
                                  };
        }

        public Label Caption { get; set; }

        public Label Statistics { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Caption   .Draw(graphics, time, alpha);
            this.Statistics.Draw(graphics, time, alpha);
        }
    }
}