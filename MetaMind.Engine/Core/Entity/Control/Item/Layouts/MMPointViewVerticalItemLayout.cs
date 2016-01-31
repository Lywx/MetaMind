namespace MetaMind.Engine.Core.Entity.Control.Item.Layouts
{
    using Interactions;
    using Microsoft.Xna.Framework;

    public class MMPointViewVerticalItemLayout : MMViewItemLayout, IMMPointViewItemLayout
    {
        public MMPointViewVerticalItemLayout(
            IMMViewItem item,
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
            this.Row = this.Id >= 0
                           ? this.Id

                           // HACK: Avoid improperly located at first row when id < 0 which means it has not been added to ItemsReads
                           : this.Id == 0 ? 0 : int.MinValue;
        }

        public virtual int Row { get; protected set; }

        public int Column
        {
            get { return 0; }
        }
    }
}