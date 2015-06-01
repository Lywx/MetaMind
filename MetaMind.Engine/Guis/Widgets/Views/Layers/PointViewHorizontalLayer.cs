namespace MetaMind.Engine.Guis.Widgets.Views.Layers
{
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;

    public class PointViewHorizontalLayer : ViewLayer
    {
        public PointViewHorizontalLayer(IView view)
            : base(view)
        {
        }

        public new PointViewHorizontalSettings ViewSettings
        {
            get
            {
                return (PointViewHorizontalSettings)base.ViewSettings;
            }
        }

        public new IPointViewHorizontalLogic ViewLogic
        {
            get
            {
                return (IPointViewHorizontalLogic)base.ViewLogic;
            }
        }

        public IPointViewHorizontalSelectionController ViewSelection
        {
            get
            {
                return this.ViewLogic.ViewSelection;
            }
        }

        public IPointViewHorizontalSwapController ViewSwap
        {
            get
            {
                return this.ViewLogic.ViewSwap;
            }
        }

        public IPointViewHorizontalScrollController ViewScroll
        {
            get
            {
                return this.ViewLogic.ViewScroll;
            }
        }
    }
}