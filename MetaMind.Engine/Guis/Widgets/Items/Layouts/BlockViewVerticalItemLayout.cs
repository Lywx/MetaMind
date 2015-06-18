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
        private ItemSettings itemSettings;

        private IPointViewVerticalLayout viewLayout;

        public BlockViewVerticalItemLayout(
            IViewItem item,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayoutInteraction)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            this.viewLayout = this.ViewGetLayer<BlockViewVerticalLayer>().ViewLayout;
            this.itemSettings = this.View.ItemSettings;
        }

        public override void Update(GameTime time)
        {
            this.UpdateId();
            this.UpdateRow();
            this.UpdateBlockString();
            this.UpdateBlockRow();
        }

        protected virtual void UpdateBlockRow()
        {
            // Remove the last empty string element by - 1
            this.BlockRow = this.BlockStringWrapped.Split('\n').Length - 1;
        }

        protected virtual void UpdateBlockString()
        {
            var label = this.itemSettings.Get<LabelSettings>(this.BlockData.BlockLabel);
            var frame = this.itemSettings.Get<FrameSettings>(this.BlockData.BlockFrame);

            this.BlockStringWrapped = StringUtils.BreakStringByWord(
                label.TextFont,
                this.BlockData.BlockStringRaw,
                label.TextSize,
                frame.Size.X,
                true);
        }

        public int BlockRow { get; protected set; }

        public IBlockViewVerticalItemData BlockData
        {
            get { return this.Item.ItemData; }
        }

        public string BlockStringWrapped { get; set; }

        protected override void UpdateRow()
        {
            this.Row = this.Id > 0
                           ? this.viewLayout.RowOf(this.Id - 1)
                             + this.viewLayout.RowIn(this.Id - 1)

                           // HACK: Avoid improperly located at first row when id < 0 which means it has not been added to ItemsReads
                           : this.Id == 0 ? 0 : int.MinValue;
        }
    }
}