namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using MetaMind.Engine.Guis.Widgets.Items.Interactions;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;

    using Microsoft.Xna.Framework;

    public class PointView2DItemLayout : ViewItemLayout, IPointViewItemLayout
    {
        private IPointView2DLayout viewLayout;

        public PointView2DItemLayout(IViewItem item, IViewItemLayoutInteraction interaction)
            : base(item, interaction)
        {
        }

        public override void SetupLayer()
        {
            this.viewLayout = this.ViewGetLayer<PointView2DLayer>().ViewLayout;
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Row    = this.viewLayout.RowFrom(this.Id);
            this.Column = this.viewLayout.ColumnFrom(this.Id);
        }

        public int Row { get; set; }

        public int Column { get; set; }
    }

    public class PointViewVerticalMultilineItemLayout : ViewItemLayout, IPointViewItemLayout
    {
        private IPointView2DLayout viewLayout;

        protected PointViewVerticalMultilineItemLayout(
            IViewItem item,
            IViewItemLayoutInteraction interaction) : base(item, interaction)
        {
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public int LineNum { get; set; }

        public override void SetupLayer()
        {
            this.viewLayout = this.ViewGetLayer<PointView2DLayer>().ViewLayout;
        }

        public override void Update(GameTime time)
        {
            var thisIndex = this.View.Items.IndexOf(this.Item);
            var previousId = (thisIndex != 0) ? this.View.Items[thisIndex - 1].ItemLogic.ItemLayout.Id : 0;

            this.Id = previousId + this.LineNum;

            this.Row    = this.viewLayout.RowFrom(this.Id);
            this.Column = this.viewLayout.ColumnFrom(this.Id);
        }
    }
}