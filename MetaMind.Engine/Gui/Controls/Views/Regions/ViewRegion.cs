namespace MetaMind.Engine.Gui.Controls.Views.Regions
{
    using System;
    using Controls.Regions;
    using Microsoft.Xna.Framework;
    using Service;

    public class ViewRegion : RectangleRegion
    {
        public ViewRegion(Func<Rectangle> regionBounds, ViewRegionSettings regionSettings)
            : base(regionBounds())
        {
            if (regionBounds == null)
            {
                throw new ArgumentNullException(nameof(regionBounds));
            }

            if (regionSettings == null)
            {
                throw new ArgumentNullException(nameof(regionSettings));
            }

            this.RegionBounds   = regionBounds;
            this.RegionSettings = regionSettings;
        }

        public Func<Rectangle> RegionBounds { get; private set; }

        public ViewRegionSettings RegionSettings { get; private set; }

        public ViewRegionVisual RegionVisual { get; set; }

        #region Draw

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.RegionVisual?.Draw(graphics, time, alpha);
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.Rectangle.Bounds = this.RegionBounds();
        }

        #endregion

        #region Operations

        public void Defocus()
        {
            this.RegionMachine.Fire(RegionMachineTrigger.FocusOutside);
        }

        #endregion
    }
}