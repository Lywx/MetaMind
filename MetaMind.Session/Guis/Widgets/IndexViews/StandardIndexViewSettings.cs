namespace MetaMind.Session.Guis.Widgets.IndexViews
{
    using Engine.Gui.Controls.Views.Scrolls;
    using Engine.Gui.Controls.Views.Settings;
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