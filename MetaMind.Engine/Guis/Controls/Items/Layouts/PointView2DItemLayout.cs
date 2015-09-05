namespace MetaMind.Engine.Guis.Controls.Items.Layouts
{
    using Interactions;
    using Microsoft.Xna.Framework;
    using Views.Layers;
    using Views.Layouts;

    public class PointView2DItemLayout : ViewItemLayout, IPointViewItemLayout
    {
        private IPointView2DLayout viewLayout;

        public PointView2DItemLayout(IViewItem item, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayoutInteraction)
        {
        }

        public override void SetupLayer()
        {
            this.viewLayout = this.ViewGetLayer<PointView2DLayer>().ViewLayout;
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Row    = this.viewLayout.RowOf(this.Id);
            this.Column = this.viewLayout.ColumnOf(this.Id);
        }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}