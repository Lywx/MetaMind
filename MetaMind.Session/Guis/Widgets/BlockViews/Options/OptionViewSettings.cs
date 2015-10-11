namespace MetaMind.Session.Guis.Widgets.BlockViews.Options
{
    using Engine.Entities.Controls.Views.Scrolls;
    using Engine.Entities.Controls.Views.Settings;
    using Microsoft.Xna.Framework;

    public class OptionViewSettings : PointViewVerticalSettings
    {
        public OptionViewSettings(
            Vector2 itemMargin,
            Vector2       viewPosition,
            int           viewRowDisplay,
            int           viewRowMax,
            ViewDirection viewDirection = ViewDirection.Normal)
            : base(itemMargin, viewPosition, viewRowDisplay, viewRowMax, viewDirection)
        {
            this.Add("ViewVerticalScrollbar", new ViewScrollbarSettings { Width = 15 });
        }
    }
}