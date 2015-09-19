namespace MetaMind.Engine.Gui.Control.Item.Layers
{
    using Logic;

    public class PointView2DItemLayer : PointViewHorizontalItemLayer, IViewItemLayer
    {
        protected PointView2DItemLayer(IViewItem item)
            : base(item)
        {
        }

        public new IPointView2DItemLogic ItemLogic 
        {
            get
            {
                return (IPointView2DItemLogic)base.ItemLogic;
            }
        }
    }
}