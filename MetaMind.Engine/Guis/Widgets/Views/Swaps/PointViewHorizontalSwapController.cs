namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using System.Collections.Generic;

    public class PointViewHorizontalSwapController<T> : ViewSwapController<T>, IViewSwapController
    {
        public PointViewHorizontalSwapController(IView view, IList<T> viewData)
            : base(view, viewData)
        {
        }
    }
}