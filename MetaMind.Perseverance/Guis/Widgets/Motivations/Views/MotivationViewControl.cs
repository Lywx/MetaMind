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
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Concepts.MotivationEntries;
    using MetaMind.Perseverance.Guis.Widgets.Motivations.Banners;
    using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// The motivation view control.
    /// </summary>
    public class MotivationViewControl : ViewControl1D
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MotivationViewControl"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <param name="viewSettings">
        /// The view settings.
        /// </param>
        /// <param name="itemSettings">
        /// The item settings.
        /// </param>
        public MotivationViewControl(
            IView view, 
            MotivationViewSettings viewSettings, 
            MotivationItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.Region = new ViewRegion(view, viewSettings, itemSettings, this.RegionPositioning);

            this.ItemFactory = new MotivationItemFactory();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the item factory.
        /// </summary>
        public MotivationItemFactory ItemFactory { get; protected set; }

        /// <summary>
        /// Gets the region.
        /// </summary>
        public ViewRegion Region { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add item.
        /// </summary>
        /// <param name="entry">
        /// The entry.
        /// </param>
        public void AddItem(MotivationEntry entry)
        {
            this.View.Items.Add(
                new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry));
        }

        /// <summary>
        /// The add item.
        /// </summary>
        public void AddItem()
        {
            this.View.Items.Add(
                new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory));
        }

        /// <summary>
        /// The update input.
        /// </summary>
        /// <param name="gameTime">
        /// The game time.
        /// </param>
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
                if (this.InputSequenceManager.Mouse.IsWheelScrolledUp)
                {
                    this.Scroll.MoveLeft();
                }

                if (this.InputSequenceManager.Mouse.IsWheelScrolledDown)
                {
                    this.Scroll.MoveRight();
                }

                // keyboard
                // ------------------------------------------------------------------
                // screen movement
                if (this.InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
                {
                    this.MoveLeft();
                }

                if (this.InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                {
                    this.MoveRight();
                }

                if (this.InputSequenceManager.Keyboard.IsActionTriggered(Actions.SLeft))
                {
                    for (var i = 0; i < this.ViewSettings.ColumnNumDisplay; i++)
                    {
                        this.MoveLeft();
                    }
                }

                if (this.InputSequenceManager.Keyboard.IsActionTriggered(Actions.SRight))
                {
                    for (var i = 0; i < this.ViewSettings.ColumnNumDisplay; i++)
                    {
                        this.MoveRight();
                    }
                }

                // escape
                if (this.InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                {
                    this.Selection.Clear();
                }

                // list management
                if (this.InputSequenceManager.Keyboard.IsActionTriggered(Actions.MotivationCreateItem))
                {
                    this.AddItem();
                }
            }

            // item input
            // -----------------------------------------------------------------
            foreach (var item in this.View.Items.ToArray())
            {
                item.UpdateInput(gameTime);
            }
        }

        /// <summary>
        /// The update strucutre.
        /// </summary>
        /// <param name="gameTime">
        /// The game time.
        /// </param>
        public override void UpdateStrucutre(GameTime gameTime)
        {
            base.UpdateStrucutre(gameTime);
            this.Region.UpdateStructure(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The region positioning.
        /// </summary>
        /// <param name="viewSettings">
        /// The view settings.
        /// </param>
        /// <param name="itemSettings">
        /// The item settings.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        private Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            var bannerSetting = new ViewBannerSetting();
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