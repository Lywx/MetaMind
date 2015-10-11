namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    using Logic;

    public class MMPointViewHorizontalItemLayer : MMPointViewItemLayer
    {
        protected MMPointViewHorizontalItemLayer(IMMViewItem item)
            : base(item)
        {

        }

        public new IMMPointViewHorizontalItemController ItemLogic
        {
            get { return (IMMPointViewHorizontalItemController)this.Item.ItemLogic; }
        }
    }
}