namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;
    using Microsoft.Xna.Framework;

    using Primtives2D;
    using Regions;
    using Services;

    public class ViewRegion : Region
    {
        private readonly Func<Rectangle> regionBounds;

        private readonly ViewRegionSettings regionSettings;

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

            this.regionBounds = regionBounds;
            this.regionSettings = regionSettings;
        }

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            Primitives2D.DrawRectangle(
                graphics.SpriteBatch,
                this.Frame.Rectangle.Extend(this.regionSettings.BorderMargin),
                this.regionSettings.HighlightColor.MakeTransparent(alpha),
                2f);

            Primitives2D.FillRectangle(
                graphics.SpriteBatch,
                this.Frame.Rectangle,
                this.regionSettings.HighlightColor.MakeTransparent(alpha));
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            this.Rectangle = this.regionBounds();
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