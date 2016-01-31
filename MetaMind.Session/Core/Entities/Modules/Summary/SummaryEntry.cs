namespace MetaMind.Session.Guis.Modules.Summary
{
    using System;
    using Engine.Core.Backend.Content.Fonts;
    using Engine.Core.Entity.Common;
    using Engine.Core.Entity.Control.Labels;
    using Engine.Core.Entity.Graphics.Fonts;
    using Microsoft.Xna.Framework;

    public class SummaryEntry : MMVisualEntity
    {
        public SummaryEntry(Func<MMFont> font, Func<string> caption, Func<Vector2> captionPos, Func<string> statistics, Func<Vector2> statisticsPos, Func<Color> color, Func<float> size)
        {
            this.Caption    = new Label(font, caption   , captionPos   , color, size);
            this.Statistics = new Label(font, statistics, statisticsPos, color, size)
                                  {
                                      TextHAlignment = HoritonalAlignment.Left
                                  };
        }

        public Label Caption { get; set; }

        public Label Statistics { get; set; }

        public override void Draw(GameTime time)
        {
            this.Caption   .Draw(time);
            this.Statistics.Draw(time);
        }
    }
}