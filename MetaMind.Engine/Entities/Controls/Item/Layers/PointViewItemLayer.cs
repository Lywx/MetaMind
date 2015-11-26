namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    using Controllers;
    using Layouts;

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