namespace MetaMind.Session.Guis.Widgets.BlockViews.Options
{
    using System;
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Visuals;
    using Microsoft.Xna.Framework;
    using Operations;

    public class OptionItemRenderer : MMViewItemRendererComponent
    {
        private OptionItemFrameController itemFrame;

        public OptionItemRenderer(IMMViewItem item)
            : base(item)
        {
        }

        #region Components

        protected ViewItemLabel IdLabel { get; set; }

        protected ViewItemLabel NameLabel { get; set; }

        protected ViewItemLabel DescriptionLabel { get; set; }

        protected MMViewItemRectangleRender IdRectangle { get; set; }

        protected MMViewItemRectangleRender NameRectangle { get; set; }

        protected MMViewItemRectangleRender DescriptionRectangle { get; set; }

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
            this.IdRectangle = new MMViewItemRectangleRender(this.Item,
                this.itemFrame.IdImmRectangle,
                itemSettings.Get<MMViewItemRenderSettings>("IdFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("IdLabel");
                this.IdLabel = new ViewItemLabel(this.Item, labelSettings)
                {
                    AnchorLocation = this.IdCenterPosition,
                    Text = () => itemLayout.Id.ToString(),
                };
            }

            var nameFrameSettings = itemSettings.Get<MMViewItemRenderSettings>("NameFrame");
            this.NameRectangle = new MMViewItemRectangleRender(this.Item,
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

            var descriptionFrameSettings = itemSettings.Get<MMViewItemRenderSettings>("DescriptionFrame");
            this.DescriptionRectangle = new MMViewItemRectangleRender(this.Item,
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

        public override void Draw(GameTime time)
        {
            if (!this.Item[MMViewItemState.Item_Is_Active]() && 
                !this.Item[MMViewItemState.Item_Is_Dragging]())
            {
            }

            // Frames
            this.IdRectangle.Draw(time);
            this.NameRectangle.Draw(time);
            this.DescriptionRectangle.Draw(time);

            // Labels
            this.IdLabel.Draw(time);
            this.NameLabel.Draw(time);
            this.DescriptionLabel.Draw(time);
        }

        #endregion
    }
}