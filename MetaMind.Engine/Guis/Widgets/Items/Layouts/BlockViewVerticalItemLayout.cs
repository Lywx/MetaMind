namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using System.Diagnostics;
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
        private int currentBuffer = 0;

        private int[] row = new int[2];

        private int[] id = new int[2];

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
            this.viewLayout = this.ViewGetLayer<BlockViewVerticalLayer>().ViewLayout;
            this.itemSettings = this.View.ItemSettings;
        }

        public override void Update(GameTime time)
        {
            this.UpdateId();
            this.UpdateRow();

            //Debug.WriteLine("id: {0}, row: {1}", this.Id, this.Row);

            var label = this.itemSettings.Get<LabelSettings>(this.BlockData.BlockLabel);
            var frame = this.itemSettings.Get<FrameSettings>(this.BlockData.BlockFrame);

            this.BlockStringWrapped =
                StringUtils.BreakStringByWord(Font.ContentBold,
                    this.BlockData.BlockStringRaw,
                    label.TextSize,
                    frame.Size.X,
                    true);

            // Remove the last empty string element by - 1
            this.BlockRow = this.BlockStringWrapped.Split('\n').Length - 1;
        }

        public override int Id
        {
            get { return this.id[this.currentBuffer]; }
            set
            {

                this.id[this.NextBuffer()] = value;
            }
        }

        public override int Row
        {
            get { return this.row[this.currentBuffer]; }
            protected set
            {
                this.row[this.NextBuffer()] = value;

                Debug.WriteLine("id {0} -> {1}", this.id[this.currentBuffer], this.id[this.NextBuffer()]);
                Debug.WriteLine("row {0} -> {1}", this.row[this.currentBuffer], value);
                
            }
        }

        public int BlockRow { get; protected set; }

        public IBlockViewVerticalItemData BlockData
        {
            get { return this.Item.ItemData; }
        }

        public string BlockStringWrapped { get; set; }

        protected override void UpdateRow()
        {
            // Added a separating line between item by + 1
            this.Row = this.Id > 0 ? this.viewLayout.RowOf(this.Id - 1) + this.viewLayout.RowIn(this.Id - 1) + 1 : 0;
        }

        private int NextBuffer()
        {
            return 1 - this.currentBuffer;
        }

        public override void UpdateBuffer()
        {
            this.currentBuffer = this.NextBuffer();
        }
    }
}