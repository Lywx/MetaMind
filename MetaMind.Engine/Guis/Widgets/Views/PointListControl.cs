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
            if (this.Region.IsEnabled(RegionState.Region_Has_Focus))
            {
                this.View.Enable(ViewState.View_Has_Focus);
            }
            else if (this.View.IsEnabled(ViewState.View_Has_Selection))
            {
                this.View.Enable(ViewState.View_Has_Focus);
            }
            else
            {
                this.View.Disable(ViewState.View_Has_Focus);
            }
        }

        #endregion

        #region Update Input

        /// <summary>
        /// Make view reject input when editing task view.
        /// </summary>
        public override bool AcceptInput
        {
            get
            {
                return base.AcceptInput && !this.View.Items.Exists(item => item.ItemControl.ItemTaskControl.TaskTracer != null
                                ? item.ItemControl.ItemTaskControl.TaskTracer.View.Control.Locked
                                : false);
            }
        }

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
            if (this.ViewSettings.Direction == PointViewSettings1D.ScrollDirection.Left)
            {
                return ExtRectangle.Rectangle(
                        viewSettings.PointStart.X - viewSettings.PointMargin.X * (viewSettings.ColumnNumDisplay / 2),
                        viewSettings.PointStart.Y,
                        viewSettings.PointMargin.X * viewSettings.ColumnNumDisplay,
                        0);
            }

            return ExtRectangle.Rectangle(
                    viewSettings.PointStart.X + viewSettings.PointMargin.X * (viewSettings.ColumnNumDisplay / 2),
                    viewSettings.PointStart.Y,
                    viewSettings.PointMargin.X * viewSettings.ColumnNumDisplay,
                    0);
        }

        #endregion
    }
}