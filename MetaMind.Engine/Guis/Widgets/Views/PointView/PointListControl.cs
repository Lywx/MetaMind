namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointListControl : PointView1DLogicControl, IPointListControl
    {
        public PointListControl(IView view, PointListSettings viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.Region = new ViewRegion(this.RegionBounds);

            this.View[ViewState.View_Has_Focus] = () => this.Region[RegionState.Region_Has_Focus]() || this.View[ViewState.View_Has_Selection]();
        }

        #region Public Properties

        public ViewRegion Region { get; private set; }

        #endregion Public Properties
        
        #region Update Input
        
        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfRegion(input, time);
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);

            this.UpdateInputOfItems(input, time);
        }

        public override void Update(GameTime time)
        {
            base       .Update(time);
            this.Region.Update(time);
        }

        protected void UpdateInputOfRegion(IGameInputService input, GameTime gameTime)
        {
            this.Region.UpdateInput(input, gameTime);
        }

        #endregion Update Input

        #region Configuration

        protected virtual Rectangle RegionBounds()
        {
            if (this.ViewSettings.Direction == PointView1DDirection.Inverse)
            {
                return ExtRectangle.RectangleByCenter(
                        this.ViewSettings.PointStart.X - this.ViewSettings.PointMargin.X * (this.ViewSettings.ColumnNumDisplay / 2),
                        this.ViewSettings.PointStart.Y,
                        this.ViewSettings.PointMargin.X * this.ViewSettings.ColumnNumDisplay,
                        0);
            }

            return ExtRectangle.RectangleByCenter(
                    this.ViewSettings.PointStart.X + this.ViewSettings.PointMargin.X * (this.ViewSettings.ColumnNumDisplay / 2),
                    this.ViewSettings.PointStart.Y,
                    this.ViewSettings.PointMargin.X * this.ViewSettings.ColumnNumDisplay,
                    0);
        }

        #endregion
    }
}