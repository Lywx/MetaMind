namespace MetaMind.Unity.Guis.Modules.Summary
{
    using System;
    using Engine;
    using Engine.Components.Content.Fonts;
    using Engine.Components.Graphics.Fonts;
    using Engine.Entities;
    using Engine.Gui.Controls.Labels;
    using Engine.Service;
    using Microsoft.Xna.Framework;

    public class SummaryEntry : MMVisualEntity
    {
        public SummaryEntry(Func<Font> font, Func<string> caption, Func<Vector2> captionPos, Func<string> statistics, Func<Vector2> statisticsPos, Func<Color> color, Func<float> size)
        {
            this.Caption    = new Label(font, caption   , captionPos   , color, size);
            this.Statistics = new Label(font, statistics, statisticsPos, color, size)
                                  {
                                      TextHAlignment = HoritonalAlignment.Left
                                  };
        }

        public Label Caption { get; set; }

        public Label Statistics { get; set; }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Caption   .Draw(graphics, time, alpha);
            this.Statistics.Draw(graphics, time, alpha);
        }
    }
}