namespace MetaMind.Engine.Guis.Controls.Items.Layouts
{
    using Interactions;
    using Microsoft.Xna.Framework;

    public class PointViewHorizontalItemLayout : ViewItemLayout, IPointViewItemLayout
    {
        protected PointViewHorizontalItemLayout(
            IViewItem item,
            IViewItemLayoutInteraction itemLayoutInteraction) : base(item, itemLayoutInteraction)
        {
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Column = this.Id;
        }

        public int Row { get { return 0; } }


        public int Column { get; protected set; } 
    }
}