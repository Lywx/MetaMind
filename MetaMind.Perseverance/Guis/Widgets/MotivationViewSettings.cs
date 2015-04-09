namespace MetaMind.Perseverance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class MotivationViewSettings : PointListSettings
    {
        public BannerSetting BannerSetting = new BannerSetting();

        public Vector2       TracerMargin;

        public MotivationViewSettings(Point start)
            : base(start)
        {
            this.TracerMargin = new Vector2(-new MotivationItemSettings().NameFrameSize.X / 2f, 75);
        }
    }

    public class MotivationStorageAccess
    {
        private Guid gui;
    }
}