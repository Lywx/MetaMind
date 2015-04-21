// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationViewControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Concepts.Motivations;

    using Microsoft.Xna.Framework;

    public interface IMotivationViewControl
    {
        void AddItem(Motivation entry);

        /// <summary>
        /// Make view reject input when editing task view.
        /// </summary>
        bool AcceptInput { get; }
    }

    public class MotivationViewControl : PointListControl, IMotivationViewControl
    {
        #region Constructors

        public MotivationViewControl(IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        #endregion Constructors

        #region Operations

        public void AddItem(Motivation entry)
        {
            var item = new ViewItemExchangable(View, ViewSettings, ItemSettings, ItemFactory, entry);
            View.Items.Add(item);
        }

        #endregion Operations

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateRegionClick(input, time);
            this.UpdateMouseScroll(input);
            this.UpdateKeyboardMotion(input);

            if (this.AcceptInput)
            {
                if (this.ViewSettings.KeyboardEnabled)
                {
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.MotivationCreateItem))
                    {
                        this.AddItem();

                        // auto select new item
                        this.Selection.Select(this.View.Items.Count - 1);
                    }
                }
            }

            this.UpdateItemInput(input, time);
        }

        #endregion Update

        #region Configuration

        protected override Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            if (this.ViewSettings.Direction == PointViewSettings1D.ScrollDirection.Left)
            {
                return
                    ExtRectangle.Rectangle(
                        viewSettings.PointStart.X - viewSettings.PointMargin.X * (viewSettings.ColumnNumDisplay / 2),
                        viewSettings.PointStart.Y,
                        viewSettings.PointMargin.X * viewSettings.ColumnNumDisplay,
                        viewSettings.BannerSetting.Height);
            }

            return
                ExtRectangle.Rectangle(
                    viewSettings.PointStart.X + viewSettings.PointMargin.X * (viewSettings.ColumnNumDisplay / 2),
                    viewSettings.PointStart.Y,
                    viewSettings.PointMargin.X * viewSettings.ColumnNumDisplay,
                    viewSettings.BannerSetting.Height);
        }

        #endregion Configuration
    }
}