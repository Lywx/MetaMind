namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointListControl : PointViewControl1D, IPointListControl
    {
        public PointListControl(IView view, PointViewSettings1D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.Region = new PointViewRegion(view, viewSettings, itemSettings, this.RegionPositioning);
        }

        #region Public Properties

        public PointViewRegion Region { get; private set; }

        #endregion Public Properties

        #region Update Structure

        protected override void UpdateViewFocus()
        {
            // TODO: Moved ?
            this.View[ViewState.View_Has_Focus] = () => this.Region[RegionState.Region_Has_Focus]() || this.View[ViewState.View_Has_Selection]();
        }

        #endregion

        #region Update Input
        
        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateRegionClick(input, time);
            this.UpdateMouseScroll(input);
            this.UpdateKeyboardMotion(input);

            this.UpdateItemInput(input, time);
        }

        public override void Update(GameTime time)
        {
            base       .Update(time);
            this.Region.Update(time);
        }

        protected void UpdateRegionClick(IGameInputService input, GameTime gameTime)
        {
            this.Region.UpdateInput(input, gameTime);
        }

        #endregion Update Input

        #region Configuration

        protected virtual Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            if (this.ViewSettings.Direction == PointViewDirection.Inverse)
            {
                return ExtRectangle.RectangleByCenter(
                        viewSettings.PointStart.X - viewSettings.PointMargin.X * (viewSettings.ColumnNumDisplay / 2),
                        viewSettings.PointStart.Y,
                        viewSettings.PointMargin.X * viewSettings.ColumnNumDisplay,
                        0);
            }

            return ExtRectangle.RectangleByCenter(
                    viewSettings.PointStart.X + viewSettings.PointMargin.X * (viewSettings.ColumnNumDisplay / 2),
                    viewSettings.PointStart.Y,
                    viewSettings.PointMargin.X * viewSettings.ColumnNumDisplay,
                    0);
        }

        #endregion
    }
}