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

        protected ViewItemRectangleVisual IdRectangle { get; set; }

        protected ViewItemRectangleVisual NameRectangle { get; set; }

        protected ViewItemRectangleVisual DescriptionRectangle { get; set; }

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
            this.ItemCenterPosition = () => this.itemFrame.RootImmRectangle.Center.ToVector2();

            this.IdCenterPosition = () => this.itemFrame.IdImmRectangle.Center.ToVector2();
            this.NamePosition = () => this.itemFrame.NameFrameLocation() + itemSettings.Get<Vector2>("NameMargin");
            this.DescriptionPosition = () => this.itemFrame.DescriptionFrameLocation() + itemSettings.Get<Vector2>("DescriptionMargin");

            // Components
            this.IdRectangle = new ViewItemRectangleVisual(this.Item,
                this.itemFrame.IdImmRectangle,
                itemSettings.Get<ViewItemVisualSettings>("IdFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("IdLabel");
                this.IdLabel = new ViewItemLabel(this.Item, labelSettings)
                {
                    AnchorLocation = this.IdCenterPosition,
                    Text = () => itemLayout.Id.ToString(),
                };
            }

            var nameFrameSettings = itemSettings.Get<ViewItemVisualSettings>("NameFrame");
            this.NameRectangle = new ViewItemRectangleVisual(this.Item,
                this.itemFrame.NameImmRectangle,
                nameFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("NameLabel");

                this.NameLabel = new ViewItemLabel(this.Item, labelSettings)
                {
                    AnchorLocation = this.NamePosition,
                    Text = () => itemData.Name,
                };
            }

            var descriptionFrameSettings = itemSettings.Get<ViewItemVisualSettings>("DescriptionFrame");
            this.DescriptionRectangle = new ViewItemRectangleVisual(this.Item,
                this.itemFrame.DescriptionImmRectangle,
                descriptionFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("DescriptionLabel");

                this.DescriptionLabel = new ViewItemLabel(this.Item, labelSettings)
                {
                    AnchorLocation = this.DescriptionPosition,
                    Text = () => itemLayer.ItemLayout.BlockStringWrapped
                };
            }

            this.itemFrame.RootImmRectangle.Add(this.IdLabel);
            this.itemFrame.RootImmRectangle.Add(this.NameLabel);
            this.itemFrame.RootImmRectangle.Add(this.DescriptionLabel);
        }

        #endregion

        #region Update and Draw

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ViewItemState.Item_Is_Active]() && 
                !this.Item[ViewItemState.Item_Is_Dragging]())
            {
            }

            // Frames
            this.IdRectangle.Draw(graphics, time, alpha);
            this.NameRectangle.Draw(graphics, time, alpha);
            this.DescriptionRectangle.Draw(graphics, time, alpha);

            // Labels
            this.IdLabel.Draw(graphics, time, alpha);
            this.NameLabel.Draw(graphics, time, alpha);
            this.DescriptionLabel.Draw(graphics, time, alpha);
        }

        #endregion
    }
}