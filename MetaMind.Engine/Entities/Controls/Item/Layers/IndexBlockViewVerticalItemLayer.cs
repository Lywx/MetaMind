namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    using Interactions;
    using Layouts;
    using Logic;

    public class MMIndexBlockViewVerticalItemLayer : MMBlockViewVerticalItemLayer
    {
        public MMIndexBlockViewVerticalItemLayer(IMMViewItem item) : base(item)
        {
        }

        public new IMMIndexBlockViewVerticalItemController ItemLogic
        {
            get { return (IMMIndexBlockViewVerticalItemController)base.ItemLogic; }
        }

        public new IMMIndexBlockViewVerticalItemLayout ItemLayout
        {
            get { return this.ItemLogic.ItemLayout; }
        }

        public IMMIndexBlockViewVerticalItemInteraction ItemInteraction
        {
            get { return this.ItemLogic.ItemInteraction; }
        }
    }
}