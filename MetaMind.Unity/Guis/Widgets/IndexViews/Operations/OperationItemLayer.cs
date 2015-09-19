namespace MetaMind.Unity.Guis.Widgets.IndexViews.Operations
{
    using Engine.Gui.Control.Item;
    using Engine.Gui.Control.Item.Layers;

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