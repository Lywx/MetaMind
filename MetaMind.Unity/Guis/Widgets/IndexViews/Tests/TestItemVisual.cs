// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestItemVisual.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity.Guis.Widgets.IndexViews.Tests
{
    using System;
    using Concepts.Tests;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Frames;
    using Engine.Gui.Controls.Item.Interactions;
    using Engine.Gui.Controls.Item.Visuals;
    using Engine.Gui.Controls.Labels;
    using Engine.Service;
    using Engine.Settings.Color;
    using Extensions;
    using Microsoft.Xna.Framework;

    public class TestItemVisual : ViewItemVisual
    {
        public TestItemVisual(IViewItem item)
            : base(item)
        {
        }

        protected TestItemFrameController ItemFrame { get; set; }

        protected IIndexBlockViewVerticalItemInteraction ItemInteraction { get; set; }

        #region Components

        protected ViewItemLabel IdLabel { get; set; }

        protected ViewItemLabel PlusLabel { get; set; }

        protected ViewItemLabel StatusLabel { get; set; }

        protected ViewItemLabel StatisticsLabel { get; set; }

        protected ViewItemLabel NameLabel { get; set; }

        protected ViewItemLabel DescriptionLabel { get; set; }

        protected ViewItemRectangleVisual IdRectangle { get; set; }

        protected ViewItemRectangleVisual PlusRectangle { get; set; }

        protected ViewItemRectangleVisual StatusRectangle { get; set; }

        protected ViewItemRectangleVisual StatisticsRectangle { get; set; }

        protected ViewItemRectangleVisual NameRectangle { get; set; }

        protected ViewItemRectangleVisual DescriptionRectangle { get; set; }

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

        public override void Initialize()
        {
            // Layers
            var itemLayer = this.GetItemLayer<TestItemLayer>();

            // Avoid the implicit closure warning in Resharper
            this.ItemFrame       = itemLayer.ItemFrame;
            var itemSettings     = itemLayer.ItemSettings;
            var itemLayout       = itemLayer.ItemLayout;
            this.ItemInteraction = itemLayer.ItemInteraction;

            ITest itemData = this.Item.ItemData;

            // Positions
            this.ItemCenterPosition = () => this.ItemFrame.RootRectangle.Center.ToVector2();

            this.IdCenterPosition = () => this.ItemFrame.IdRectangle.Center.ToVector2();
            this.PlusCenterPosition = () => this.ItemFrame.PlusRectangle.Center.ToVector2();

            this.StatusCenterPosition = () => this.ItemFrame.StatusRectangle.Center.ToVector2();
            this.StatisticsCenterPosition = () => this.ItemFrame.StatisticsRectangle.Center.ToVector2();

            this.NamePosition = () => this.ItemFrame.NameFrameLocation() + itemSettings.Get<Vector2>("NameMargin");
            this.DescriptionPosition = () => this.ItemFrame.DescriptionFrameLocation() + itemSettings.Get<Vector2>("DescriptionMargin");

            // Components
            this.IdRectangle = new ViewItemRectangleVisual(this.Item,
                this.ItemFrame.IdRectangle,
                itemSettings.Get<ViewItemVisualSettings>("IdFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("IdLabel");
                labelSettings.Text = () => itemLayout.Id.ToString();
                labelSettings.AnchorLocation = this.IdCenterPosition;

                this.IdLabel = new ViewItemLabel(this.Item, labelSettings);
            }

            this.PlusRectangle = new ViewItemRectangleVisual(this.Item,
                this.ItemFrame.PlusRectangle,
                itemSettings.Get<ViewItemVisualSettings>("PlusFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("PlusLabel");
                labelSettings.Text = () => this.ItemInteraction.IndexedViewOpened ? "-" : "+";
                labelSettings.AnchorLocation = this.PlusCenterPosition;

                this.PlusLabel = new ViewItemLabel(this.Item, labelSettings);
            }

            this.StatusRectangle = new ViewItemRectangleVisual(this.Item,
                this.ItemFrame.StatusRectangle,
                itemSettings.Get<ViewItemVisualSettings>("StatusFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("StatusLabel");
                labelSettings.Text = () => itemData.Evaluation.ResultStatus;
                labelSettings.AnchorLocation = this.StatusCenterPosition;

                this.StatusLabel = new ViewItemLabel(this.Item, labelSettings)
                {
                    TextColor = () =>
                                itemData.Evaluation.ResultPassed
                                    ? Palette.LightGreen
                                    : Palette.LightPink
                };
            }

            this.StatisticsRectangle = new ViewItemRectangleVisual(this.Item,
                this.ItemFrame.StatisticsRectangle,
                itemSettings.Get<ViewItemVisualSettings>("StatisticsFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("StatisticsLabel");
                labelSettings.AnchorLocation = this.StatisticsCenterPosition;

                this.StatisticsLabel = new ViewItemLabel(this.Item, labelSettings)
                {
                    TextColor = () =>
                                itemData.Evaluation.ResultChildrenPassed == itemData.Children.Count
                                    ? Palette.LightGreen
                                    : Palette.LightPink,
                    Text = () => itemData.Evaluation.ResultChanged
                                     ? $"{itemData.Evaluation.ResultChildrenPassed} ( {itemData.Evaluation.ResultChange.ToSummary()} ) / {itemData.Children.Count}"
                                     : $"{itemData.Evaluation.ResultChildrenPassed} / {itemData.Children.Count}"
                };
            }

            var nameFrameSettings = itemSettings.Get<ViewItemVisualSettings>("NameFrame");
            this.NameRectangle = new ViewItemRectangleVisual(this.Item,
                this.ItemFrame.NameRectangle,
                nameFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("NameLabel");
                labelSettings.Text = () => itemData.Name;
                labelSettings.AnchorLocation = this.NamePosition;

                this.NameLabel = new ViewItemLabel(this.Item, labelSettings);
            }

            var descriptionFrameSettings = itemSettings.Get<ViewItemVisualSettings>("DescriptionFrame");
            this.DescriptionRectangle = new ViewItemRectangleVisual(this.Item,
                this.ItemFrame.DescriptionRectangle,
                descriptionFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("DescriptionLabel");
                labelSettings.Text = () => itemLayer.ItemLayout.BlockStringWrapped;
                labelSettings.AnchorLocation = this.DescriptionPosition;

                this.DescriptionLabel = new ViewItemLabel(this.Item, labelSettings);
            }
        }

        #endregion

        #region Update and Draw

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ViewItemState.Item_Is_Active]() && 
                !this.Item[ViewItemState.Item_Is_Dragging]())
            {
                if (this.ItemInteraction.IndexedViewOpened)
                {
                    this.ItemInteraction.IndexedView.Draw(graphics, time, alpha);
                }

                return;
            }

            // Frames
            this.IdRectangle.Draw(graphics, time, alpha);
            if (this.Item.ItemData.HasChildren)
            {
                this.PlusRectangle.Draw(graphics, time, alpha);
            }

            this.StatusRectangle.Draw(graphics, time, alpha);

            if (this.Item.ItemData.HasChildren)
            {
                this.StatisticsRectangle.Draw(graphics, time, alpha);
            }

            this.NameRectangle.Draw(graphics, time, alpha);
            this.DescriptionRectangle.Draw(graphics, time, alpha);

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

            // Indexed view
            if (this.ItemInteraction.IndexedViewOpened)
            {
                this.ItemInteraction.IndexedView.Draw(graphics, time, alpha);
            }
        }

        #endregion
    }
}