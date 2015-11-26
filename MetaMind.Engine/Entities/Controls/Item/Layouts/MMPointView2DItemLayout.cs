namespace MetaMind.Engine.Entities.Controls.Item.Layouts
{
    using Interactions;
    using Microsoft.Xna.Framework;
    using Views.Layers;
    using Views.Layouts;

    public class MMPointView2DItemLayout : MMViewItemLayout, IMMPointViewItemLayout
    {
        private IMMPointView2DLayout viewLayout;

        public MMPointView2DItemLayout(IMMViewItem item, IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayoutInteraction)
        {
        }

        public override void Initialize()
        {
            this.viewLayout = this.GetViewLayer<MMPointView2DLayer>().ViewLayout;
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