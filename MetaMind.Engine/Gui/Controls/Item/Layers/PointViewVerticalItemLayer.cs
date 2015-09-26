namespace MetaMind.Engine.Gui.Controls.Item.Layers
{
    using Logic;

    public class PointViewVerticalItemLayer : PointViewItemLayer
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