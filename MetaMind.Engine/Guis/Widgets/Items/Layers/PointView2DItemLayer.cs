namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;

    public class PointView2DItemLayer : ViewItemLayer, IViewItemLayer
    {
        protected PointView2DItemLayer(IViewItem item)
            : base(item)
        {
        }

        public new PointView2DItemLogic ItemLogic 
        {
            get
            {
                return (PointView2DItemLogic)base.ItemLogic;
            }
        }
    }
}