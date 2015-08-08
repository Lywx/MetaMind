namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Layers;

    public class OperationItemLayer : IndexBlockViewVerticalItemLayer
    {
        public OperationItemLayer(IViewItem item) 
            : base(item)
        {
        }

        public new OperationItemLogic ItemLogic
        {
            get { return (OperationItemLogic)base.ItemLogic; }
        }

        public OperationItemFrame ItemFrame
        {
            get { return this.ItemLogic.ItemFrame; }
        }
    }
}