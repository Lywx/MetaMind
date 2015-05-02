namespace MetaMind.Engine.Guis.Widgets.Views.Extensions
{
    using MetaMind.Engine.Guis.Widgets.Views.Logic;

    public class PointView2DExtension : PointViewHorizontalExtension
    {
        protected PointView2DExtension(IView view)
            : base(view)
        {
        }

        public new IPointView2DLogic ViewLogic
        {
            get
            {
                return (IPointView2DLogic)base.ViewLogic;
            }
        }
    }
}