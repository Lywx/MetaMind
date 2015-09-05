namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using MetaMind.Engine.Guis.Widgets.Items.Interactions;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;

    using Microsoft.Xna.Framework;

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