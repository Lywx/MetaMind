namespace MetaMind.Unity.Guis.Widgets.BlockViews.Options
{
    using System;
    using Concepts.Operations;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Frames;
    using Engine.Gui.Controls.Item.Visuals;
    using Engine.Gui.Controls.Labels;
    using Engine.Service;
    using Microsoft.Xna.Framework;

    public class OptionItemVisual : ViewItemVisual
    {
        private OptionItemFrameController itemFrame;

        public OptionItemVisual(IViewItem item)
            : base(item)
        {
        }

        #region Components

        protected ViewItemLabel IdLabel { get; set; }

        protected ViewItemLabel NameLabel { get; set; }

        protected ViewItemLabel DescriptionLabel { get; set; }

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

        public override void Initialize()
        {
            // Layers
            var itemLayer = this.GetItemLayer<OptionItemLayer>();

            // Avoid the implicit closure warning in Resharper
            this.itemFrame   = itemLayer.ItemFrame;
            var itemSettings = itemLayer.ItemSettings;
            var itemLayout   = itemLayer.ItemLayout;

            IOption itemData = this.Item.ItemData;

            // Positions
            this.ItemCenterPosition = () => this.itemFrame.RootRectangle.Center.ToVector2();

            this.IdCenterPosition = () => this.itemFrame.IdRectangle.Center.ToVector2();
            this.NamePosition = () => this.itemFrame.NameFrameLocation() + itemSettings.Get<Vector2>("NameMargin");
            this.DescriptionPosition = () => this.itemFrame.DescriptionFrameLocation() + itemSettings.Get<Vector2>("DescriptionMargin");

            // Components
            this.IdFrame = new ViewItemFrameVisual(this.Item,
                this.itemFrame.IdRectangle,
                itemSettings.Get<FrameSettings>("IdFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("IdLabel");
                this.IdLabel = new ViewItemLabel(this.Item, labelSettings)
                {
                    AnchorLocation = this.IdCenterPosition,
                    Text = () => itemLayout.Id.ToString(),
                };
            }

            var nameFrameSettings = itemSettings.Get<FrameSettings>("NameFrame");
            this.NameFrame = new ViewItemFrameVisual(this.Item,
                this.itemFrame.NameRectangle,
                nameFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("NameLabel");

                this.NameLabel = new ViewItemLabel(this.Item, labelSettings)
                {
                    AnchorLocation = this.NamePosition,
                    Text = () => itemData.Name,
                };
            }

            var descriptionFrameSettings = itemSettings.Get<FrameSettings>("DescriptionFrame");
            this.DescriptionFrame = new ViewItemFrameVisual(this.Item,
                this.itemFrame.DescriptionRectangle,
                descriptionFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("DescriptionLabel");

                this.DescriptionLabel = new ViewItemLabel(this.Item, labelSettings)
                {
                    AnchorLocation = this.DescriptionPosition,
                    Text = () => itemLayer.ItemLayout.BlockStringWrapped
                };
            }

            this.itemFrame.RootRectangle.Add(this.IdLabel);
            this.itemFrame.RootRectangle.Add(this.NameLabel);
            this.itemFrame.RootRectangle.Add(this.DescriptionLabel);
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