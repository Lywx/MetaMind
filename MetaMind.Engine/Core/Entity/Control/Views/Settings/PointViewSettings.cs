namespace MetaMind.Engine.Core.Entity.Control.Views.Settings
{
    using Microsoft.Xna.Framework;

    // TODO: This is my implementation of layout. I felt that it is not well designed.
    public class PointViewSettings : ViewSettings, IPointViewSettings
    {
        public PointViewSettings(Vector2 itemMargin, Vector2 viewPosition, ViewDirection viewDirection = ViewDirection.Normal)
        {
            this.ItemMargin   = itemMargin;

            this.ViewPosition = viewPosition;
            this.ViewDirection = viewDirection;
        }

        public Vector2 ItemMargin { get; set; }

        public Vector2 ViewPosition { get; set; }

        public ViewDirection ViewDirection { get; set; }
    }
}