namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Engine.Settings;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Views;

    using Microsoft.Xna.Framework;

    public class TraceViewSettings : ViewSettings2D
    {
        public bool  Positive;

        public Color HighlightColor    = ColorPalette.TransparentColor1;

        public Point BorderMargin      = new Point(4, 4);

        public ViewScrollBarSettings ScrollBarSettings = new ViewScrollBarSettings();
    }
}