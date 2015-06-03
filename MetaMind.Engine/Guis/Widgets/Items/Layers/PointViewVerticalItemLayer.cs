namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    using Logic;

    public class PointViewVerticalItemLayer : PointViewHorizontalItemLayer
    {
        protected PointViewVerticalItemLayer(IViewItem item)
            : base(item)
        {

        }

        public new IPointViewVerticalItemLogic ItemLogic
        {
            get { return (IPointViewVerticalItemLogic)this.Item.ItemLogic; }
        }
    }
}