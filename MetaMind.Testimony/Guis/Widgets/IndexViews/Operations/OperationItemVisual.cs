namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Operations
{
    using Concepts.Operations;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Frames;
    using Engine.Guis.Widgets.Items.Visuals;
    using Engine.Guis.Widgets.Visuals;
    using Engine.Settings.Colors;
    using Microsoft.Xna.Framework;
    using Tests;

    public class OperationItemVisual : TestItemVisual
    {
        public OperationItemVisual(IViewItem item) : base(item)
        {
        }

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
            this.StatisticsCenterPosition = () => this.ItemFrame.StatisticsFrame.Center.ToVector2();

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
                labelSettings.Text = () => itemData.OperationStatus;
                labelSettings.TextPosition = this.StatusCenterPosition;

                this.StatusLabel = new ViewItemLabelVisual(this.Item, labelSettings);
                this.StatusLabel.Label.TextColor = () =>
                        itemData.IsOperationActivated
                            ? Palette.LightGreen
                            : Palette.LightPink;
            }

            this.StatisticsFrame = new ViewItemFrameVisual(this.Item,
                this.ItemFrame.StatisticsFrame,
                itemSettings.Get<FrameSettings>("StatisticsFrame"));
            {
                var labelSettings = itemSettings.Get<LabelSettings>("StatisticsLabel");
                labelSettings.TextPosition = this.StatisticsCenterPosition;

                this.StatisticsLabel = new ViewItemLabelVisual(this.Item, labelSettings);
                this.StatisticsLabel.Label.TextColor = () => Color.White;
                this.StatisticsLabel.Label.Text = () => string.Format("{0} / {1}", itemData.ChildrenOperationActivated, itemData.Children.Count);
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
    }
}
