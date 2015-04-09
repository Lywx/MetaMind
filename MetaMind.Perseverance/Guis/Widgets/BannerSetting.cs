namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Settings;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    public class BannerSetting
    {
        public int Width  = GameEngine.GraphicsSettings.Width;
        public int Height = 103;
        public int Thin   = 4;
        public int Thick  = 2;

        public Color Color = Palette.LightBlue;
    }
}