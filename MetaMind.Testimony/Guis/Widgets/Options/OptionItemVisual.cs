namespace MetaMind.Testimony.Guis.Widgets.Options
{
    using System;
    using Concepts.Operations;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Visuals;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Services;
    using Microsoft.Xna.Framework;

    public class OptionItemVisual : ViewItemVisual
    {
        private OptionItemFrame itemFrame;

        public OptionItemVisual(IViewItem item)
            : base(item)
        {
        }

        #region Components

        protected ViewItemLabelVisual IdLabel { get; set; }

        protected ViewItemLabelVisual NameLabel { get; set; }

        protected ViewItemLabelVisual DescriptionLabel { get; set; }

        protected ViewItemFrameVisual IdFrame { get; set; }

        protected ViewItemFrameVisual NameFrame { get; set; }

        protected ViewItemFrameVisual DescriptionFrame { get; set; }

        #endregion

        #region Positions

        protected Func<Vector2> ItemCenterPosition { get; set; }

        protected Func<Vector2> IdCenterPosition { get; set; }

        protected Func<Vector2> NamePosition { get; set; }

        protected Func<Vector2> DescriptionPosition { get; set; }

        #endregion

        #region Layering

        public override void SetupLayer()
        {
            // Layers
            var itemLayer = this.ItemGetLayer<OptionItemLayer>();

            // Avoid the implicit closure warning in Resharper
            this.itemFrame       = itemLayer.ItemFrame;
            var itemSettings     = itemLayer.ItemSettings;
            var itemLayout       = itemLayer.ItemLayout;

            IOption itemData = Item.ItemData;
            // Positions
            this.ItemCenterPosition = () => this.itemFrame.RootFrame.Center.ToVector2();

            this.IdCenterPosition = () => this.itemFrame.IdFrame.Center.ToVector2();
            this.NamePosition = () => this.itemFrame.NameFrameLocation() + itemSettings.Get<Vector2>("NameMargin");
            this.DescriptionPosition = () => this.itemFrame.DescriptionFrameLocation() + itemSettings.Get<Vector2>("DescriptionMargin");

            // Components
            this.IdFrame = new ViewItemFrameVisual(this.Item,
                this.itemFrame.IdFrame,
                itemSettings.Get<FrameSettings>("IdFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("IdLabel");
                labelSettings.Text = () => itemLayout.Id.ToString();
                labelSettings.TextPosition = this.IdCenterPosition;

                this.IdLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }

            var nameFrameSettings = itemSettings.Get<FrameSettings>("NameFrame");
            this.NameFrame = new ViewItemFrameVisual(this.Item,
                this.itemFrame.NameFrame,
                nameFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("NameLabel");
                labelSettings.Text = () => itemData.Name;
                labelSettings.TextPosition = this.NamePosition;

                this.NameLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }

            var descriptionFrameSettings = itemSettings.Get<FrameSettings>("DescriptionFrame");
            this.DescriptionFrame = new ViewItemFrameVisual(this.Item,
                this.itemFrame.DescriptionFrame,
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
            }

            // Frames
            this.IdFrame.Draw(graphics, time, alpha);
            this.NameFrame.Draw(graphics, time, alpha);
            this.DescriptionFrame.Draw(graphics, time, alpha);

            // Labels
            this.IdLabel.Draw(graphics, time, alpha);
            this.NameLabel.Draw(graphics, time, alpha);
            this.DescriptionLabel.Draw(graphics, time, alpha);
        }

        #endregion
    }
}