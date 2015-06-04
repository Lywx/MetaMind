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
            this.UpdateId();
            this.UpdateRow();
        }

        protected virtual void UpdateRow()
        {
            this.Row = this.Id;
        }

        public virtual int Row { get; protected set; }

        public int Column
        {
            get { return 0; }
        }
    }
}