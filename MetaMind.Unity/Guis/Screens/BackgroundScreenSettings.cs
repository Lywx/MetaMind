namespace MetaMind.Unity.Guis.Screens
{
    using Engine;
    using Microsoft.Xna.Framework;

    public class BackgroundScreenSettings : GameSettings
    {
        public Color Color { get; set; } = Color.Black;

        public float Brightness { get; set; } = 1f;
    }

    public static class BackgroundScreenSettingsExtension
    {
        public static Color GetColor(this BackgroundScreenSettings settings)
        {
            return settings.Color.MakeDark(settings.Brightness);
        }
    }
}