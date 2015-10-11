namespace MetaMind.Engine.Entities.Controls.Views.Regions
{
    using System;
    using Entities;
    using Microsoft.Xna.Framework;
    using Primtives2D;

    // TODO: Make region contrl
    public class ViewRegionVisual : MMVisualEntity
    {
        public ViewRegionVisual(ViewRegion region)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }

            this.Region = region;
        }

        public ViewRegion Region { get; private set; }

        public ViewRegionSettings RegionSettings => this.Region.RegionSettings;

        public override void Draw(GameTime time)
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