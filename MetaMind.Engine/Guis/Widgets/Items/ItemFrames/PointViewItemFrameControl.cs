namespace MetaMind.Engine.Guis.Widgets.Items.ItemFrames
{
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.PointView.ViewScroll;
    using MetaMind.Engine.Guis.Widgets.Views.PointView.ViewSwap;

    public class PointViewItemFrameControl : ViewItemFrameControl
    {
        public PointViewItemFrameControl(IViewItem item, ItemSettings itemSettings)
            : base(item, itemSettings)
        {
        }

        protected IViewSwapControl ViewSwap
        {
            get
            {
                return ((IViewSwapSupport)this.ViewLogic).ViewSwap;
            }
        }

        protected IPointViewScrollControl ViewScroll
        {
            get
            {
                return ((IViewScrollSupport)this.ViewLogic).ViewScroll;
            }
        }

        protected override void UpdateFrameGeometry()
        {
            if (!this.Item[ItemState.Item_Is_Dragging]() && 
                !this.Item[ItemState.Item_Is_Swaping]())
            {
                this.RootFrame.Center = this.ViewScroll.RootCenterPosition(this.ItemLogic.Id);
            }
            else if (this.Item[ItemState.Item_Is_Swaping]())
            {
                this.RootFrame.Center = this.ViewSwap.RootCenterPosition;
            }
        }

    }
}