namespace MetaMind.Engine.Guis.Widgets.Views.Regions
{
    using System;
    using Microsoft.Xna.Framework;
    using Primtives2D;
    using Services;

    public class ViewRegionVisual : GameVisualEntity
    {
        public ViewRegionVisual(ViewRegion region)
        {
            if (region == null)
            {
                throw new ArgumentNullException("region");
            }

            this.Region = region;
        }

        public ViewRegion Region { get; private set; }

        public ViewRegionSettings RegionSettings
        {
            get { return this.Region.RegionSettings; }
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            Primitives2D.DrawRectangle(
                graphics.SpriteBatch,
                this.Region.Frame.Rectangle.Extend(this.RegionSettings.RegionMargin),
                this.RegionSettings.RegionColor.MakeTransparent(alpha),
                this.RegionSettings.RegionThick);

            Primitives2D.FillRectangle(
                graphics.SpriteBatch,
                this.Region.Frame.Rectangle,
                this.RegionSettings.RegionColor.MakeTransparent(alpha));
        }
    }
}