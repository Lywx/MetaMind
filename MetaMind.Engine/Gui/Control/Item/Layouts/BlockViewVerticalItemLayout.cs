namespace MetaMind.Engine.Gui.Control.Item.Layouts
{
    using Data;
    using Extensions;
    using Frames;
    using Gui.Control.Visuals;
    using Interactions;
    using Layers;
    using Microsoft.Xna.Framework;
    using Settings;
    using Views.Layouts;

    public class BlockViewVerticalItemLayout : PointViewVerticalItemLayout, IBlockViewVerticalItemLayout
    {
        private ItemSettings itemSettings;

        private IPointViewVerticalLayout viewLayout;

        public BlockViewVerticalItemLayout(
            IViewItem item,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, 
                   itemLayoutInteraction)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            this.viewLayout = this.GetViewLayer<BlockViewVerticalLayer>().ViewLayout;
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
                label.Font,
                this.BlockData.BlockStringRaw,
                label.Size,
                frame.Size.X,
                true);
        }

        public int BlockRow { get; protected set; }

        public IBlockViewItemData BlockData => this.Item.ItemData;

        public string BlockStringWrapped { get; set; }

        protected override void UpdateRow()
        {
            this.Row = this.Id > 0
                           ? this.viewLayout.RowOf(this.Id - 1) + this.viewLayout.RowIn(this.Id - 1)

                           // HACK: Avoid improperly located at first row when id < 0 which means it has not been added to ItemsReads
                           : this.Id == 0 ? 0 : int.MinValue;
        }
    }
}