namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;

    using Microsoft.Xna.Framework;

    public interface IListControl : IPointViewControl
    {
        PointViewRegion Region { get; }

        bool AcceptInput { get; }

        bool Active { get; }

        void AddItem();

        void MoveLeft();

        void MoveRight();

        void SuperMoveLeft();

        void SuperMoveRight();
    }

    public class ListControl : PointViewControl1D, IListControl
    {
        public ListControl(IView view, PointViewSettings1D viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
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
                return base.AcceptInput && !this.View.Items.Exists(
                    item => item.ItemControl.ItemTaskControl.TaskTracer != null
                                ? item.ItemControl.ItemTaskControl.TaskTracer.View.Control.Locked
                                : false);
            }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.UpdateRegionClick(gameTime);
            this.UpdateMouseScroll();
            this.UpdateKeyboardMotion();

            this.UpdateItemInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            base       .UpdateStructure(gameTime);
            this.Region.UpdateStructure(gameTime);
        }

        protected void UpdateRegionClick(GameTime gameTime)
        {
            this.Region.UpdateInput(gameTime);
        }

        #endregion Update Input

        #region Configuration

        protected virtual Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            if (this.ViewSettings.Direction == PointViewSettings1D.ScrollDirection.Left)
            {
                return
                    RectangleExt.Rectangle(
                        viewSettings.StartPoint.X - viewSettings.RootMargin.X * (viewSettings.ColumnNumDisplay / 2),
                        viewSettings.StartPoint.Y,
                        viewSettings.RootMargin.X * viewSettings.ColumnNumDisplay,
                        0);
            }

            return
                RectangleExt.Rectangle(
                    viewSettings.StartPoint.X + viewSettings.RootMargin.X * (viewSettings.ColumnNumDisplay / 2),
                    viewSettings.StartPoint.Y,
                    viewSettings.RootMargin.X * viewSettings.ColumnNumDisplay,
                    0);
        }


        #endregion
    }
}