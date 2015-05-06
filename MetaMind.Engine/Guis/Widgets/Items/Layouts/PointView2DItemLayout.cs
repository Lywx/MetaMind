namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using MetaMind.Engine.Guis.Widgets.Items.Interactions;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;

    using Microsoft.Xna.Framework;

    public class PointView2DItemLayout : ViewItemLayout, IPointViewItemLayout
    {
        private readonly IPointView2DLayout viewLayout;

        protected PointView2DItemLayout(IViewItem item, IViewItemLayoutInteraction interaction)
            : base(item, interaction)
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
}