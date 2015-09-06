namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using Engine.Guis.Controls.Items;
    using Engine.Guis.Controls.Items.Layers;

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