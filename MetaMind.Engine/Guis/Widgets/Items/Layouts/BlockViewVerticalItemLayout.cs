namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using Microsoft.Xna.Framework;

    using Components.Fonts;
    using Data;
    using Frames;
    using Interactions;
    using Layers;
    using Settings;
    using Views.Layouts;
    using Widgets.Visuals;

    public class BlockViewVerticalItemLayout : PointViewVerticalItemLayout, IBlockViewVerticalItemLayout
    {
        private IPointViewVerticalLayout viewLayout;

        private ItemSettings itemSettings;

        public BlockViewVerticalItemLayout(
            IViewItem item,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayoutInteraction)
        {
        }

        public override void SetupLayer()
        {
            this.viewLayout = this.ViewGetLayer<BlockViewVerticalLayer>().ViewLayout;
            this.itemSettings = this.View.ItemSettings;
        }

        public override void Update(GameTime time)
        {
            this.Id = this.View.Items.IndexOf(this.Item);

            this.Row = this.Id > 0
                ? this.viewLayout.RowOf(this.Id - 1)
                  + this.viewLayout.RowIn(this.Id - 1)
                : 0;

            var nameLabelSettings = this.itemSettings.Get<LabelSettings>("NameLabel");
            var nameFrameSettings = this.itemSettings.Get<FrameSettings>("NameFrame");

            this.BlockRow = StringUtils.BreakStringByWord(Font.ContentBold, ((IBlockViewVerticalItemData)this.Item.ItemData).BlockText, nameLabelSettings.TextSize, nameFrameSettings.Size.X, true).Split('\n').Length;
        }

        public int BlockRow { get; set; }

        public IBlockViewVerticalItemData 
    }
}