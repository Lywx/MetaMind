namespace MetaMind.Engine.Guis.Widgets.Views.Settings
{
    using Microsoft.Xna.Framework;

    public class PointViewSettings : ViewSettings
    {
        public Vector2 Margin;

        public Vector2 Position;

        public ViewDirection Direction;

        public PointViewSettings(
            Vector2 position,
            Vector2 margin,
            ViewDirection direction = ViewDirection.Normal)
        {
            this.Position = position;
            this.Margin = margin;

            this.Direction = direction;
        }
    }
}