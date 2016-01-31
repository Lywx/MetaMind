namespace MetaMind.Engine.Core.Entity.Control.Item.Layouts
{
    using Interactions;
    using Microsoft.Xna.Framework;

    public class MMPointViewHorizontalItemLayout : MMViewItemLayout, IMMPointViewItemLayout
    {
        protected MMPointViewHorizontalItemLayout(
            IMMViewItem item,
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