namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointViewSettings : ViewSettings, IPointViewSettings
    {
        public Vector2 ItemMargin { get; set; }

        public Vector2 ViewPosition { get; set; }

        public ViewDirection ViewDirection { get; set; }

        public PointViewSettings(Vector2 itemMargin, Vector2 viewPosition, ViewDirection viewDirection = ViewDirection.Normal)
        {
            this.ViewPosition = viewPosition;
            this.ItemMargin   = itemMargin;

            this.ViewDirection = viewDirection;
        }
    }
}