// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationViewControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Concepts.MotivationEntries;

    using Microsoft.Xna.Framework;

    public interface IMotivationViewControl
    {
        void AddItem(MotivationEntry entry);

        /// <summary>
        /// Make view reject input when editing task view.
        /// </summary>
        bool AcceptInput { get; }
    }

    public class MotivationViewControl : ListControl, IMotivationViewControl
    {
        #region Constructors

        public MotivationViewControl(IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        #endregion Constructors

        #region Operations

        public void AddItem(MotivationEntry entry)
        {
            var item = new ViewItemExchangable(View, ViewSettings, ItemSettings, ItemFactory, entry);
            View.Items.Add(item);
        }

        #endregion Operations

        #region Update

        public override void UpdateInput(GameTime gameTime)
        {
            this.UpdateRegionClick(gameTime);
            this.UpdateMouseScroll();
            this.UpdateKeyboardMotion();

            if (this.AcceptInput)
            {
                if (this.ViewSettings.KeyboardEnabled)
                {
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.MotivationCreateItem))
                    {
                        this.AddItem();

                        // auto select new item
                        this.Selection.Select(this.View.Items.Count - 1);
                    }
                }
            }

            this.UpdateItemInput(gameTime);
        }

        #endregion Update

        #region Configuration

        protected override Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            if (this.ViewSettings.Direction == PointViewSettings1D.ScrollDirection.Left)
            {
                return
                    RectangleExt.Rectangle(
                        viewSettings.StartPoint.X - viewSettings.RootMargin.X * (viewSettings.ColumnNumDisplay / 2),
                        viewSettings.StartPoint.Y,
                        viewSettings.RootMargin.X * viewSettings.ColumnNumDisplay,
                        viewSettings.BannerSetting.Height);
            }

            return
                RectangleExt.Rectangle(
                    viewSettings.StartPoint.X + viewSettings.RootMargin.X * (viewSettings.ColumnNumDisplay / 2),
                    viewSettings.StartPoint.Y,
                    viewSettings.RootMargin.X * viewSettings.ColumnNumDisplay,
                    viewSettings.BannerSetting.Height);
        }

        #endregion Configuration
    }
}