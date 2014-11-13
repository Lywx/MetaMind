namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Banners
{
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class BannerSetting
    {
        public int Width  = GraphicsSettings.Width;
        public int Height = 103;
        public int Thin   = 4;
        public int Thick  = 2;

        public Color Color = ColorPalette.TransparentColor1;
    }
}