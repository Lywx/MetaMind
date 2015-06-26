namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Operations
{
    using System;
    using Concepts.Operations;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Visuals;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Services;
    using Engine.Settings.Colors;
    using Microsoft.Xna.Framework;

    public class OperationItemVisual : ViewItemVisual
    {
        public OperationItemVisual(IViewItem item) : base(item)
        {
        }

        protected OperationItemFrame ItemFrame { get; set; }

        protected IIndexBlockViewVerticalItemInteraction ItemInteraction { get; set; }

        #region Components

        protected ViewItemLabelVisual IdLabel { get; set; }

        protected ViewItemLabelVisual PlusLabel { get; set; }

        protected ViewItemLabelVisual StatusLabel { get; set; }

        protected ViewItemLabelVisual NameLabel { get; set; }

        protected ViewItemLabelVisual DescriptionLabel { get; set; }

        protected ViewItemFrameVisual IdFrame { get; set; }

        protected ViewItemFrameVisual PlusFrame { get; set; }

        protected ViewItemFrameVisual StatusFrame { get; set; }

        protected ViewItemFrameVisual NameFrame { get; set; }

        protected ViewItemFrameVisual DescriptionFrame { get; set; }

        #endregion

        #region Positions

        protected Func<Vector2> ItemCenterPosition { get; set; }

        protected Func<Vector2> IdCenterPosition { get; set; }

        protected Func<Vector2> PlusCenterPosition { get; set; }

        protected Func<Vector2> StatusCenterPosition { get; set; }

        protected Func<Vector2> NamePosition { get; set; }

        protected Func<Vector2> DescriptionPosition { get; set; }

        #endregion

        public override void SetupLayer()
        {
            // Layers
            var itemLayer = this.ItemGetLayer<OperationItemLayer>();

            // Avoid the implicit closure warning in Resharper
            this.ItemFrame       = itemLayer.ItemFrame;
            var itemSettings     = itemLayer.ItemSettings;
            var itemLayout       = itemLayer.ItemLayout;
            this.ItemInteraction = itemLayer.ItemInteraction;

            IOperationDescription itemData = this.Item.ItemData;

            // Positions
            this.ItemCenterPosition = () => this.ItemFrame.RootFrame.Center.ToVector2();

            this.IdCenterPosition = () => this.ItemFrame.IdFrame.Center.ToVector2();
            this.PlusCenterPosition = () => this.ItemFrame.PlusFrame.Center.ToVector2();

            this.StatusCenterPosition = () => this.ItemFrame.StatusFrame.Center.ToVector2();

            this.NamePosition = () => this.ItemFrame.NameFrameLocation() + itemSettings.Get<Vector2>("NameMargin");
            this.DescriptionPosition = () => this.ItemFrame.DescriptionFrameLocation() + itemSettings.Get<Vector2>("DescriptionMargin");

            // Components
            this.IdFrame = new ViewItemFrameVisual(this.Item,
                this.ItemFrame.IdFrame,
                itemSettings.Get<FrameSettings>("IdFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("IdLabel");
                labelSettings.Text = () => itemLayout.Id.ToString();
                labelSettings.TextPosition = this.IdCenterPosition;

                this.IdLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }

            this.PlusFrame = new ViewItemFrameVisual(this.Item,
                this.ItemFrame.PlusFrame,
                itemSettings.Get<FrameSettings>("PlusFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("PlusLabel");
                labelSettings.Text = () => this.ItemInteraction.IndexedViewOpened ? "-" : "+";
                labelSettings.TextPosition = this.PlusCenterPosition;

                this.PlusLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }

            this.StatusFrame = new ViewItemFrameVisual(this.Item,
                this.ItemFrame.StatusFrame,
                itemSettings.Get<FrameSettings>("StatusFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("StatusLabel");
                labelSettings.Text = () =>
                    itemData.HasChildren
                        ? string.Format(
                            "{0} / {1}",
                            itemData.ChildrenOperationActivated,
                            itemData.Children.Count)
                        : itemData.OperationStatus;
                labelSettings.TextPosition = this.StatusCenterPosition;

                this.StatusLabel = new ViewItemLabelVisual(this.Item, labelSettings);
                this.StatusLabel.Label.TextColor = () =>
                    itemData.HasChildren
                        ? Color.White
                        : itemData.IsOperationActivated
                            ? Palette.LightGreen
                            : Palette.LightPink;
            }

            var nameFrameSettings = itemSettings.Get<FrameSettings>("NameFrame");
            this.NameFrame = new ViewItemFrameVisual(this.Item,
                this.ItemFrame.NameFrame,
                nameFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("NameLabel");
                labelSettings.Text = () => itemData.Name;
                labelSettings.TextPosition = this.NamePosition;

                this.NameLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }

            var descriptionFrameSettings = itemSettings.Get<FrameSettings>("DescriptionFrame");
            this.DescriptionFrame = new ViewItemFrameVisual(this.Item,
                this.ItemFrame.DescriptionFrame,
                descriptionFrameSettings);
            {
                var labelSettings = itemSettings.Get<LabelSettings>("DescriptionLabel");
                labelSettings.Text = () => itemLayer.ItemLayout.BlockStringWrapped;
                labelSettings.TextPosition = this.DescriptionPosition;

                this.DescriptionLabel = new ViewItemLabelVisual(this.Item, labelSettings);
            }
        }

        #region Update and Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.Item[ItemState.Item_Is_Active]() && 
                !this.Item[ItemState.Item_Is_Dragging]())
            {
                if (this.ItemInteraction.IndexedViewOpened)
                {
                    this.ItemInteraction.IndexedView.Draw(graphics, time, alpha);
                }

                return;
            }

            // Frames
            this.IdFrame.Draw(graphics, time, alpha);
            if (this.Item.ItemData.HasChildren)
            {
                this.PlusFrame.Draw(graphics, time, alpha);
            }

            this.StatusFrame.Draw(graphics, time, alpha);

            this.NameFrame.Draw(graphics, time, alpha);
            this.DescriptionFrame.Draw(graphics, time, alpha);

            // Labels
            this.IdLabel.Draw(graphics, time, alpha);

            if (this.Item.ItemData.HasChildren)
            {
                this.PlusLabel.Draw(graphics, time, alpha);
            }

            this.StatusLabel.Draw(graphics, time, alpha);

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
