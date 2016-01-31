namespace MetaMind.Engine.Core.Entity.Control.Item.Layers
{
    using Controllers;
    using Layouts;

    public class MMBlockViewVerticalItemLayer : MMPointViewVerticalItemLayer
    {
        public MMBlockViewVerticalItemLayer(IMMViewItem item)
            : base(item)
        {
        }

        public new IMMBlockViewVerticalItemController ItemLogic
        {
            get { return (IMMBlockViewVerticalItemController)base.ItemLogic; }
        }

        public new IMMBlockViewVerticalItemLayout ItemLayout
        {
            get { return this.ItemLogic.ItemLayout; }
        }
    }
}