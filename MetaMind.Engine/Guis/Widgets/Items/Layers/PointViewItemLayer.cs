namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    using MetaMind.Engine.Guis.Widgets.Items.Logic;

    public class PointViewItemLayer : ViewItemLayer
    {
        protected PointViewItemLayer(IViewItem item)
            : base(item)
        {

        }

        public new IPointViewHorizontalItemLogic ItemLogic
        {
            get { return (IPointViewHorizontalItemLogic)this.Item.ItemLogic; }
        }
    }
}