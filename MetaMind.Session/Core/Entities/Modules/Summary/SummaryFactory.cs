namespace MetaMind.Session.Guis.Modules.Summary
{
    using System;
    using Engine.Core.Backend.Content.Fonts;
    using Engine.Core.Entity.Common;
    using Microsoft.Xna.Framework;

    public class SummaryFactory : MMVisualEntity
    {
        public SummaryFactory(SummarySettings settings)
        {
            this.Settings = settings;
        }

        public SummarySettings Settings { get; set; }

        public SummaryEntry CreateEntry(int line, string caption, Func<string> statistics, Color color)
        {
            return this.CreateEntry(() => line, () => caption, statistics, () => color);
        }

        public SummaryEntry CreateEntry(Func<int> line, Func<string> caption, Func<string> statistics, Func<Color> color)
        {
            var font = new Func<MMFont>(() => this.Settings.EntityFont);

            var captionPos    = new Func<Vector2>(() => new Vector2(this.Settings.ViewportWidth / 2f - 300, 150 + line() * this.Settings.LineHeight));
            var statisticsPos = new Func<Vector2>(() => new Vector2(this.Settings.ViewportWidth / 2f + 260, 150 + line() * this.Settings.LineHeight));

            var size = new Func<float>(() => this.Settings.EntitySize);

            return new SummaryEntry(font, caption, captionPos, statistics, statisticsPos, color, size);
        }

        public SummaryEntry CreateEntry(int line, string caption, string statistics, Color color)
        {
            return this.CreateEntry(() => line, () => caption, () => statistics, () => color);
        }

        public SummarySplit CreateSplit(int line, Color color)
        {
            var start = new Func<Vector2>(() => new Vector2(this.Settings.ViewportWidth / 2f - 300, 150 + line * this.Settings.LineHeight + this.Settings.LineHeight / 2));
            var end   = new Func<Vector2>(() => new Vector2(this.Settings.ViewportWidth / 2f + 300, 150 + line * this.Settings.LineHeight + this.Settings.LineHeight / 2));
            var size  = new Func<float>(() => this.Settings.EntitySize);

            return new SummarySplit(start, end, () => color, size);
        }
    }
}