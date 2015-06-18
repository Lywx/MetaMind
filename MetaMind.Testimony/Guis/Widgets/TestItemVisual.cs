﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestItemVisual.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Testimony.Guis.Widgets
{
    using System;

    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Visuals;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Services;
    using Engine.Settings.Colors;
    using Microsoft.Xna.Framework;

    public class TestItemVisual : ViewItemVisual
    {
        private TestItemFrame itemFrame;

        private TestItemLogic itemLogic;

        public TestItemVisual(IViewItem item)
            : base(item)
        {
        }

        #region Components

        protected ViewItemLabelVisual IdLabel { get; set; }

        protected ViewItemLabelVisual PlusLabel { get; set; }

        protected ViewItemLabelVisual StatusLabel { get; set; }

        protected ViewItemLabelVisual StatisticsLabel { get; set; }

        protected ViewItemLabelVisual NameLabel { get; set; }

        protected ViewItemLabelVisual DescriptionLabel { get; set; }

        protected ViewItemFrameVisual IdFrame { get; set; }

        protected ViewItemFrameVisual PlusFrame { get; set; }

        protected ViewItemFrameVisual StatusFrame { get; set; }

        protected ViewItemFrameVisual StatisticsFrame { get; set; }

        protected ViewItemFrameVisual NameFrame { get; set; }

        protected ViewItemFrameVisual DescriptionFrame { get; set; }

        #endregion

        #region Positions

        protected Func<Vector2> ItemCenterPosition { get; set; }

        protected Func<Vector2> IdCenterPosition { get; set; }

        protected Func<Vector2> PlusCenterPosition { get; set; }

        protected Func<Vector2> StatusCenterPosition { get; set; }

        protected Func<Vector2> StatisticsCenterPosition { get; set;}

        protected Func<Vector2> NamePosition { get; set; }

        protected Func<Vector2> DescriptionPosition { get; set; }

        #endregion

        #region Layering

        public override void SetupLayer()
        {
            // Layers
            var itemLayer = this.ItemGetLayer<TestItemLayer>();
            this.itemLogic = itemLayer.ItemLogic;

            // Avoid the implicit closure warning in Resharper
            this.itemFrame = itemLayer.ItemFrame;
            var itemSettings = itemLayer.ItemSettings;
            var itemLayout = itemLayer.ItemLayout;

            // Positions
            this.ItemCenterPosition = () => itemFrame.RootFrame.Center.ToVector2();

            this.IdCenterPosition = () => itemFrame.IdFrame.Center.ToVector2();
            this.PlusCenterPosition = () => itemFrame.PlusFrame.Center.ToVector2();

            this.StatusCenterPosition = () => itemFrame.StatusFrame.Center.ToVector2();
            this.StatisticsCenterPosition = () => itemFrame.StatisticsFrame.Center.ToVector2();

            this.NamePosition = () => itemFrame.NameFrameLocation() + itemSettings.Get<Vector2>("NameMargin");
            this.DescriptionPosition = () => itemFrame.DescriptionFrameLocation() + itemSettings.Get<Vector2>("DescriptionMargin");

            // Components
            this.IdFrame = new ViewItemFrameVisual(this.Item,
                itemFrame.IdFrame,
                itemSettings.Get<FrameSettings>("IdFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("IdLabel");
                labelSettings.Text = () => itemLayout.Id.ToString();
                labelSettings.TextPosition = this.IdCenterPosition;

                this.IdLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }

            this.PlusFrame = new ViewItemFrameVisual(this.Item,
                itemFrame.PlusFrame,
                itemSettings.Get<FrameSettings>("PlusFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("PlusLabel");
                labelSettings.Text = () => itemLogic.IndexedViewOpened ? "-" : "+";
                labelSettings.TextPosition = this.PlusCenterPosition;

                this.PlusLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }

            this.StatusFrame = new ViewItemFrameVisual(this.Item,
                itemFrame.StatusFrame,
                itemSettings.Get<FrameSettings>("StatusFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("StatusLabel");
                labelSettings.Text = () => this.Item.ItemData.Status;
                labelSettings.TextPosition = this.StatusCenterPosition;

                this.StatusLabel = new ViewItemLabelVisual(this.Item, labelSettings);
                this.StatusLabel.Label.TextColor = () =>
                        this.Item.ItemData.Passed
                            ? Palette.LightGreen
                            : Palette.LightPink;
                this.StatusLabel.Label.Text = () => this.Item.ItemData.Status;
            }

            this.StatisticsFrame = new ViewItemFrameVisual(this.Item,
                itemFrame.StatisticsFrame,
                itemSettings.Get<FrameSettings>("StatisticsFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("StatisticsLabel");
                labelSettings.Text = () => this.Item.ItemData.Status;
                labelSettings.TextPosition = this.StatisticsCenterPosition;

                this.StatisticsLabel = new ViewItemLabelVisual(this.Item, labelSettings);
                this.StatisticsLabel.Label.TextColor = () =>
                        this.Item.ItemData.ChildrenPassed == this.Item.ItemData.Children.Count
                            ? Palette.LightGreen
                            : Palette.LightPink;
                this.StatisticsLabel.Label.Text =
                    () => string.Format("{0} / {1}",
                        this.Item.ItemData.ChildrenPassed,
                        this.Item.ItemData.Children.Count);
            }

            var nameFrameSettings = itemSettings.Get<FrameSettings>("NameFrame");
            this.NameFrame = new ViewItemFrameVisual(this.Item,
                itemFrame.NameFrame,
                nameFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("NameLabel");
                labelSettings.Text = () => this.Item.ItemData.Name;
                labelSettings.TextPosition = this.NamePosition;

                this.NameLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }

            var descriptionFrameSettings = itemSettings.Get<FrameSettings>("DescriptionFrame");
            this.DescriptionFrame = new ViewItemFrameVisual(this.Item,
                itemFrame.DescriptionFrame,
                descriptionFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("DescriptionLabel");
                labelSettings.Text = () => itemLayer.ItemLayout.BlockStringWrapped;
                labelSettings.TextPosition = this.DescriptionPosition;

                this.DescriptionLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }
        }

        #endregion

        #region Update and Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ItemState.Item_Is_Active]() &&
                !this.Item[ItemState.Item_Is_Dragging]())
            {
                return;
            }

            // Frames
            this.IdFrame.Draw(graphics, time, alpha);
            if (this.Item.ItemData.HasChildren)
            {
                this.PlusFrame.Draw(graphics, time, alpha);
            }

            this.StatusFrame.Draw(graphics, time, alpha);

            if (this.Item.ItemData.HasChildren)
            {
                this.StatisticsFrame.Draw(graphics, time, alpha);
            }

            this.NameFrame.Draw(graphics, time, alpha);
            this.DescriptionFrame.Draw(graphics, time, alpha);

            // Labels
            this.IdLabel.Draw(graphics, time, alpha);

            if (this.Item.ItemData.HasChildren)
            {
                this.PlusLabel.Draw(graphics, time, alpha);
            }

            this.StatusLabel.Draw(graphics, time, alpha);

            if (this.Item.ItemData.HasChildren)
            {
                this.StatisticsLabel.Draw(graphics, time, alpha);
            }

            this.NameLabel.Draw(graphics, time, alpha);
            this.DescriptionLabel.Draw(graphics, time, alpha);

            // Index view
            if (this.itemLogic.IndexedViewOpened)
            {
                this.itemLogic.IndexedView.Draw(graphics, time, alpha);
            }
        }

        #endregion
    }
}