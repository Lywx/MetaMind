// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestItemVisual.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Testimony.Guis.Widgets
{
    using System;
    using Engine.Components.Fonts;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Visuals;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Services;

    using Microsoft.Xna.Framework;

    public class TestItemVisual : ViewItemVisual
    {
        public TestItemVisual(IViewItem item)
            : base(item)
        {
        }

        #region Components

        protected ViewItemLabelVisual IdLabel { get; set; }

        protected ViewItemLabelVisual NameLabel { get; set; }

        protected ViewItemLabelVisual StatusLabel { get; set; }

        protected ViewItemFrameVisual IdFrame { get; set; }

        protected ViewItemFrameVisual NameFrame { get; set; }

        protected ViewItemFrameVisual StatusFrame { get; set; }

        #endregion

        #region Positions

        protected Func<Vector2> IdCenterPosition { get; set; }

        protected Func<Vector2> NamePosition { get; set; }

        protected Func<Vector2> StatusCenterPosition { get; set; }

        protected Func<Vector2> ItemCenterPosition { get; set; }

        #endregion

        #region Layering

        public override void SetupLayer()
        {
            // Layers
            var itemLayer = this.ItemGetLayer<TestItemLayer>();
            var itemSettings = itemLayer.ItemSettings;
            var itemFrame = itemLayer.ItemFrame;
            var itemLayout = itemLayer.ItemLayout;

            // Positions
            this.ItemCenterPosition = () => itemFrame.RootFrame.Center.ToVector2();

            this.IdCenterPosition = () => itemFrame.IdFrame.Center.ToVector2();
            this.NamePosition = () => itemFrame.NameFrameLocation() + itemSettings.Get<Vector2>("NameMargin");
            this.StatusCenterPosition = () => itemFrame.StatusFrame.Center.ToVector2();

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

            this.NameFrame = new ViewItemFrameVisual(this.Item,
                itemFrame.NameFrame,
                itemSettings.Get<FrameSettings>("NameFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("NameLabel");
                labelSettings.Text = () => this.Item.ItemData.Name;
                labelSettings.TextPosition = this.NamePosition;

                this.NameLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }

            this.StatusFrame = new ViewItemFrameVisual(this.Item,
                itemFrame.StatusFrame,
                itemSettings.Get<FrameSettings>("StatusFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("StatusLabel");
                labelSettings.Text = () => this.Item.ItemData.Status;
                labelSettings.TextPosition = this.StatusCenterPosition;

                this.StatusLabel = new ViewItemLabelVisual(this.Item, labelSettings);
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

            this.IdFrame.Draw(graphics, time, alpha);
            this.NameFrame.Draw(graphics, time, alpha);
            this.StatusFrame.Draw(graphics, time, alpha);

            this.IdLabel.Draw(graphics, time, alpha);
            this.NameLabel.Draw(graphics, time, alpha);
            this.StatusLabel.Draw(graphics, time, alpha);
        }

        #endregion
    }
}