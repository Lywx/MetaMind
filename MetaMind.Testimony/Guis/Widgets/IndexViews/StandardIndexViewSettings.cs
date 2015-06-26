namespace MetaMind.Testimony.Guis.Widgets.IndexViews
{
    using Engine.Guis.Widgets.Views.Scrolls;
    using Engine.Guis.Widgets.Views.Settings;
    using Microsoft.Xna.Framework;

    public class StandardIndexViewSettings : PointViewVerticalSettings
    {
        public StandardIndexViewSettings(
            Vector2 itemMargin,
            Vector2 viewPosition,
            int viewRowDisplay,
            int viewRowMax,
            ViewDirection viewDirection = ViewDirection.Normal)
            : base(itemMargin, viewPosition, viewRowDisplay, viewRowMax, viewDirection)
        {
            this.Add("ViewVerticalScrollbar", new ViewScrollbarSettings { Width = 15 });
        }
    }
}