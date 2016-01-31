namespace MetaMind.Engine.Core.Entity.Control.Item.Layers
{
    using Controllers;

    public class MMPointViewVerticalItemLayer : MMPointViewItemLayer
    {
        protected MMPointViewVerticalItemLayer(IMMViewItem item)
            : base(item)
        {

        }

        public new IMMPointViewVerticalItemController ItemLogic
        {
            get { return (IMMPointViewVerticalItemController)this.Item.ItemLogic; }
        }
    }
}