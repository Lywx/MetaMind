namespace MetaMind.Testimony.Guis.Widgets
{
    using Engine.Guis.Widgets.Views.Settings;
    using Microsoft.Xna.Framework;

    public class TestViewSettings : PointView2DSettings
    {
        public TestViewSettings(
            Vector2 position,
            Vector2 margin,
            int columnNumDisplay,
            int columnNumMax,
            int rowNumDisplay,
            int rowNumMax)
            : base(position, margin, columnNumDisplay, columnNumMax, rowNumDisplay, rowNumMax)
        {
        }
    }
}