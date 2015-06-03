namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
{
    using Interactions;
    using Microsoft.Xna.Framework;

    public class PointViewVerticalItemLayout : ViewItemLayout, IPointViewItemLayout
    {
        public PointViewVerticalItemLayout(
            IViewItem item,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayoutInteraction)
        {
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Row = this.Id;
        }

        public int Row { get; protected set; }

        public int Column
        {
            get { return 0; }
        }
    }
}