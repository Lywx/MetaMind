namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Concepts.MotivationEntries;

    using Microsoft.Xna.Framework;

    public class MotivationViewSettings : ListSettings
    {
        public BannerSetting BannerSetting = new BannerSetting();

        public Vector2       TracerMargin;

        //---------------------------------------------------------------------
        public MotivationSpace Space;

        public MotivationViewSettings()
        {
            this.TracerMargin = new Vector2(-new MotivationItemSettings().NameFrameSize.X / 2f, 90);
        }
    }
}