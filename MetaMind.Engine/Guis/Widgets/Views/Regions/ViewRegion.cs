namespace MetaMind.Engine.Guis.Widgets.Views.Regions
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;
    using Widgets.Regions;

    public class ViewRegion : Region
    {
        public ViewRegion(Func<Rectangle> regionBounds, ViewRegionSettings regionSettings)
            : base(regionBounds())
        {
            if (regionBounds == null)
            {
                throw new ArgumentNullException("regionBounds");
            }

            if (regionSettings == null)
            {
                throw new ArgumentNullException("regionSettings");
            }

            this.RegionBounds   = regionBounds;
            this.RegionSettings = regionSettings;
        }

        public Func<Rectangle> RegionBounds { get; private set; }

        public ViewRegionSettings RegionSettings { get; private set; }

        public ViewRegionVisual RegionVisual { get; set; }

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.RegionVisual != null)
            {
                this.RegionVisual.Draw(graphics, time, alpha);
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.Rectangle = this.RegionBounds();
        }

        #endregion

        #region Operations

        public void Blur()
        {
            this.StateMachine.Fire(Trigger.PressedOutside);
        }

        #endregion
    }
}