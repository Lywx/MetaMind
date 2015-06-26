namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Operations
{
    using Engine.Guis.Widgets.Items;

    public class OperationItemLayer : StandardItemLayer
    {
        public OperationItemLayer(IViewItem item) : base(item)
        {
        }

        public new OperationItemLogic ItemLogic
        {
            get { return (OperationItemLogic)base.ItemLogic; }
        }
    }
}