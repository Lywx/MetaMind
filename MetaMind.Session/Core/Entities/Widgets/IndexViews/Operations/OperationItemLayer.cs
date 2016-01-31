namespace MetaMind.Session.Guis.Widgets.IndexViews.Operations
{
    using Engine.Core.Entity.Control.Item;
    using Engine.Core.Entity.Control.Item.Layers;

    public class OperationItemLayer : MMIndexBlockViewVerticalItemLayer
    {
        public OperationItemLayer(IMMViewItem item) 
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