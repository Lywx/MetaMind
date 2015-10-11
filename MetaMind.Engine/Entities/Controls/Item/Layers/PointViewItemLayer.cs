namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    using Layouts;
    using Logic;

    public class MMPointViewItemLayer : MMViewItemLayer
    {
        protected MMPointViewItemLayer(IMMViewItem item) : base(item)
        {
        }

        public IMMPointViewItemLayout ItemLayout
        {
            get { return this.ItemLogic.ItemLayout; }
        }

        public new IMMPointViewItemController ItemLogic
        {
            get { return (IMMPointViewItemController)base.ItemLogic; }
        }
    }
}