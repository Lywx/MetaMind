namespace MetaMind.Engine.Gui.Controls.Item.Layers
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