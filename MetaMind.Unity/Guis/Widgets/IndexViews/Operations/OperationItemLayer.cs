namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Layers;

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

        public OperationItemFrameController ItemFrame
        {
            get { return this.ItemLogic.ItemFrame; }
        }
    }
}