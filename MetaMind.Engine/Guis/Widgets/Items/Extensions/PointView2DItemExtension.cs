namespace MetaMind.Engine.Guis.Widgets.Items.Extensions
{
    using MetaMind.Engine.Guis.Widgets.Items.Logic;

    public class PointView2DItemExtension : ViewItemExtension, IViewItemExtension
    {
        protected PointView2DItemExtension(IViewItem item)
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