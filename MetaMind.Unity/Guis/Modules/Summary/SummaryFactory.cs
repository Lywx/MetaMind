namespace MetaMind.Unity.Guis.Modules.Summary
{
    using System;
    using Engine;
    using Engine.Components.Fonts;
    using Microsoft.Xna.Framework;

    public class SummaryFactory : GameVisualEntity
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
            var font = new Func<Font>(() => this.Settings.EntityFont);

            var captionPos    = new Func<Vector2>(() => new Vector2(this.Settings.ScreenWidth / 2f - 300, 150 + line() * this.Settings.LineHeight));
            var statisticsPos = new Func<Vector2>(() => new Vector2(this.Settings.ScreenWidth / 2f + 260, 150 + line() * this.Settings.LineHeight));

            var size = new Func<float>(() => this.Settings.EntitySize);

            return new SummaryEntry(font, caption, captionPos, statistics, statisticsPos, color, size);
        }

        public SummaryEntry CreateEntry(int line, string caption, string statistics, Color color)
        {
            return this.CreateEntry(() => line, () => caption, () => statistics, () => color);
        }

        public SummarySplit CreateSplit(int line, Color color)
        {
            var start = new Func<Vector2>(() => new Vector2(this.Settings.ScreenWidth / 2f - 300, 150 + line * this.Settings.LineHeight + this.Settings.LineHeight / 2));
            var end   = new Func<Vector2>(() => new Vector2(this.Settings.ScreenWidth / 2f + 300, 150 + line * this.Settings.LineHeight + this.Settings.LineHeight / 2));
            var size  = new Func<float>(() => this.Settings.EntitySize);

            return new SummarySplit(start, end, () => color, size);
        }
    }
}