namespace MetaMind.Session.Guis.Screens
{
    using Engine.Core.Settings;
    using Microsoft.Xna.Framework;

    public class BackgroundScreenSettings : MMSettings
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