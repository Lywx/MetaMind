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
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}