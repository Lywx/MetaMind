// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationViewControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Regions;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Concepts.MotivationEntries;
    using MetaMind.Perseverance.Guis.Widgets.Motivations.Banners;
    using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
    using MetaMind.Perseverance.Guis.Widgets.Tasks;

    using Microsoft.Xna.Framework;

    public class MotivationViewControl : ViewControl1D
    {
        #region Constructors and Destructors

        public MotivationViewControl(IView view, MotivationViewSettings viewSettings, MotivationItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.Region      = new ViewRegion(view, viewSettings, itemSettings, this.RegionPositioning);
            this.ItemFactory = new MotivationItemFactory();
        }

        #endregion

        #region Public Properties

        public MotivationItemFactory ItemFactory { get; protected set; }

        public ViewRegion Region { get; private set; }

        #endregion

        #region Operations


        public void AddItem(MotivationEntry entry)
        {
            this.View.Items.Add(
                new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry));
        }

  
        public void AddItem()
        {
            this.View.Items.Add(
                new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory));
        }

        #endregion

        #region Update

        /// <summary>
        /// Make view reject input when editing task view.
        /// </summary>
        public override bool AcceptInput
        {
            get
            {
                return base.AcceptInput
                       && !View.Items.Exists(
                           item =>
                           item.ItemControl.ItemTaskControl.TaskTracer != null
                               ? item.ItemControl.ItemTaskControl.TaskTracer.View.Control.Locked
                               : false);
            }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            // mouse
            // -----------------------------------------------------------------
            // region
            this.Region.UpdateInput(gameTime);

            if (this.AcceptInput)
            {
                // mouse
                // ------------------------------------------------------------------
                if (InputSequenceManager.Mouse.IsWheelScrolledUp)
                {
                    this.Scroll.MoveLeft();
                }

                if (InputSequenceManager.Mouse.IsWheelScrolledDown)
                {
                    this.Scroll.MoveRight();
                }

                // keyboard
                // ------------------------------------------------------------------
                // screen movement
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
                {
                    this.MoveLeft();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                {
                    this.MoveRight();
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SLeft))
                {
                    for (var i = 0; i < this.ViewSettings.ColumnNumDisplay; i++)
                    {
                        this.MoveLeft();
                    }
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SRight))
                {
                    for (var i = 0; i < this.ViewSettings.ColumnNumDisplay; i++)
                    {
                        this.MoveRight();
                    }
                }

                // escape
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                {
                    this.Selection.Clear();
                }

                // list management
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.MotivationCreateItem))
                {
                    this.AddItem();

                    // auto select new item
                    this.Selection.Select(View.Items.Count - 1);
                }
            }

            // item input
            // -----------------------------------------------------------------
            foreach (var item in this.View.Items.ToArray())
            {
                item.UpdateInput(gameTime);
            }
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            base.UpdateStructure(gameTime);
            this.Region.UpdateStructure(gameTime);
        }

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

        #region Configurations

        private Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            var bannerSetting = new BannerSetting();
            if (ViewSettings.Direction == ViewSettings1D.ScrollDirection.Left)
            {
                return
                    RectangleExt.Rectangle(
                        viewSettings.StartPoint.X - viewSettings.RootMargin.X * (viewSettings.ColumnNumDisplay / 2),
                        viewSettings.StartPoint.Y,
                        viewSettings.RootMargin.X * viewSettings.ColumnNumDisplay,
                        bannerSetting.Height);
            }
            return
                RectangleExt.Rectangle(
                    viewSettings.StartPoint.X + viewSettings.RootMargin.X * (viewSettings.ColumnNumDisplay / 2),
                    viewSettings.StartPoint.Y,
                    viewSettings.RootMargin.X * viewSettings.ColumnNumDisplay,
                    bannerSetting.Height);
        }

        #endregion
    }
}