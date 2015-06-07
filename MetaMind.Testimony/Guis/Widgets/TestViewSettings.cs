namespace MetaMind.Testimony.Guis.Widgets
{
    using Engine.Guis.Widgets.Views.Scrolls;
    using Engine.Guis.Widgets.Views.Settings;
    using Microsoft.Xna.Framework;

    public class TestViewSettings : PointViewVerticalSettings
    {
        public TestViewSettings(
            Vector2 itemMargin,
            Vector2 viewPosition,
            int viewRowDisplay,
            int viewRowMax,
            ViewDirection viewDirection = ViewDirection.Normal)
            : base(itemMargin, viewPosition, viewRowDisplay, viewRowMax, viewDirection)
        {
            this.Add("ViewVerticalScrollbar", new ViewScrollbarSettings());
        }
    }
}