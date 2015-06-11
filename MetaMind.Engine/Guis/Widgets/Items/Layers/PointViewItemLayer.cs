namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    using Layouts;
    using Logic;

    public class PointViewItemLayer : ViewItemLayer
    {
        protected PointViewItemLayer(IViewItem item) : base(item)
        {
        }

        public IPointViewItemLayout ItemLayout
        {
            get { return this.ItemLogic.ItemLayout; }
        }

        public new IPointViewItemLogic ItemLogic
        {
            get { return (IPointViewItemLogic)base.ItemLogic; }
        }
    }
}