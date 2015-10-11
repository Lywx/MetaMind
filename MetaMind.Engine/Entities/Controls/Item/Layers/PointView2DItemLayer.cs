namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    using Logic;

    public class MMPointView2DItemLayer : MMPointViewHorizontalItemLayer, IMMViewItemLayer
    {
        protected MMPointView2DItemLayer(IMMViewItem item)
            : base(item)
        {
        }

        public new IMMPointView2DItemController ItemLogic 
        {
            get
            {
                return (IMMPointView2DItemController)base.ItemLogic;
            }
        }
    }
}