namespace MetaMind.Engine.Guis.Widgets.Items.Settings
{
    using System;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Visuals;

    using Microsoft.Xna.Framework;

    public class ItemSettings : WidgetSettings, ICloneable
    {
        public ItemSettings()
        {
            this.Add("RootFrame", new FrameSettings { Size = new Point(25, 25), Margin = new Point(2, 2) });

            this.Add("IdFrame", new FrameSettings { Size = new Point(25, 25), Margin = new Point(2, 2) });
            this.Add("IdLabel", new LabelSettings { Size = 0.7f, Color = Color.White, Font = Font.UiStatistics });
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}