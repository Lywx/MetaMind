namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    using MetaMind.Engine.Guis.Widgets.Items.Logic;

    public class PointViewHorizontalItemLayer : ViewItemLayer
    {
        protected PointViewHorizontalItemLayer(IViewItem item)
            : base(item)
        {

        }

        public new IPointViewHorizontalItemLogic ItemLogic
        {
            get { return (IPointViewHorizontalItemLogic)this.Item.ItemLogic; }
        }
    }
}