namespace MetaMind.Engine.Guis.Widgets.Items.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Swaps;
    using MetaMind.Engine.Guis.Widgets.Items.Transits;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Extensions;
    using MetaMind.Engine.Services;

    public class PointViewItemViewControl : ViewItemViewControl, IViewItemViewSelectionProvider, IViewItemViewSwapProvider, IViewItemViewTransitProvider
    {
        public PointViewItemViewControl(IViewItem item)
            : base(item)
        {
        }

        public override Func<bool> ItemIsActive
        {
            get
            {
                return () => this.View[ViewState.View_Is_Active]() && this.ViewExtension.Get<PointView1Dor2DExtension>().viewScroll.CanDisplay(this.ItemLogic.Id);
            }
        }

        public void ViewDoSelect()
        {
            this.ViewExtension.Get<PointView1Dor2DExtension>().ViewSelection.Select(this.ItemLogic.Id);
        }

        public void ViewDoUnselect()
        {
            if (this.ViewSelection.IsSelected(this.ItemLogic.Id))
            {
                this.ViewSelection.Clear();
            }
        }

        public void ViewSelectionUpdate()
        {
            if (this.ViewExtension.Get<PointViewExtension>().ViewSelection.IsSelected(this.ItemLogic.Id))
            {
                // Unify mouse and keyboard selection
                if (!this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemLogic.CommonSelectsIt();
                }

                this.Item[ItemState.Item_Is_Selected] = () => true;
            }
            else
            {
                // Unify mouse and keyboard selection
                if (this.Item[ItemState.Item_Is_Selected]())
                {
                    this.ItemLogic.CommonUnselectsIt();
                }

                this.Item[ItemState.Item_Is_Selected] = () => false;
            }
            
        }

        public void ViewSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            if (this.Item[ItemState.Item_Is_Swaping]())
            {
                return;
            }

            this.Item[ItemState.Item_Is_Swaping] = () => true;

            var originCenter = this.ViewLogic.ViewScroll.RootCenterPoint(this.ItemLogic.Id);
            var targetCenter = draggingItem.ViewLogic.ViewScroll.RootCenterPoint(draggingItem.ItemLogic.Id);

            this.ViewLogic.ViewSwap.Initialize(originCenter, targetCenter);

            interop.Process.AttachProcess(new ViewItemSwapProcess(draggingItem, this.Item));
        }

        public void ViewTransit(IGameInteropService interop, IViewItem draggingItem, IView targetView)
        {
            if (this.Item[ItemState.Item_Is_Transiting]())
            {
                return;
            }

            this.Item[ItemState.Item_Is_Transiting] = () => true;
            
            interop.Process.AttachProcess(new ViewItemTransitProcess(draggingItem, targetView));
        }
    }
}