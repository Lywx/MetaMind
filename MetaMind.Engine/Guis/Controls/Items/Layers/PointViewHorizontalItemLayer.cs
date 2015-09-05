namespace MetaMind.Engine.Guis.Controls.Items.Layers
{
    using Logic;

    public class PointViewHorizontalItemLayer : PointViewItemLayer
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