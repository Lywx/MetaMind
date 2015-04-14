namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System.Diagnostics;

    using MetaMind.Engine.Components.Processes;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    using Process = MetaMind.Engine.Components.Processes.Process;

    public class ViewItemTransitProcess : Process
    {
        #region Source Data

        private readonly IItemRootFrame srcFrame;

        protected IViewItemExchangable SrcItem { get; private set; }

        #endregion 

        #region Destination Data

        private readonly IRegion desRegion;

        protected IPointViewSelectionControl DesSelection { get; private set; }

        protected IView DesView { get; private set; }

        #endregion 

        #region Constructors

        public ViewItemTransitProcess(IViewItem srcItem, IView desView)
        {
            // source part
            this.SrcItem = srcItem as IViewItemExchangable;
            this.srcFrame = srcItem.ItemControl.RootFrame;

            // assert item exchangable
            {
                Debug.Assert(this.SrcItem != null, "Dragging item is not exchangable");
            }

            // destination part
            this.DesView = desView;
            this.desRegion = desView.Control.Region;
            this.DesSelection = desView.Control.Selection;

            this.SrcItem.Enable(ItemState.Item_Exchanging);
            this.srcFrame.MouseDropped += this.MouseInsideRegionThenTransit;
        }

        #endregion Constructors

        #region Transit Transition

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
            this.Terminate();
        }

        public override void OnSuccess()
        {
            this.Terminate();
        }

        #endregion Transit Transition

        #region Transit Update

        public override void Update(GameTime gameTime)
        {
        }

        #endregion Exchange Update

        #region Transit Operations

        protected virtual void Transit()
        {
            this.SrcItem.View.Disable(ViewState.View_Has_Focus);
            this.SrcItem.ViewControl.Selection.Clear();

            var position = this.DesSelection.PreviousSelectedId != null ? (int)this.DesSelection.PreviousSelectedId : 0;

            this.SrcItem.ExchangeTo(this.DesView, position);
        }

        private void Terminate()
        {
            // stop swapping state
            this.SrcItem.Disable(ItemState.Item_Exchanging);
            this.srcFrame.MouseDropped -= this.MouseInsideRegionThenTransit;
        }

        #endregion Exchange Operations

        #region Transit Trigger

        private void MouseInsideRegionThenTransit(object sender, FrameEventArgs e)
        {
            if (this.desRegion.IsEnabled(RegionState.Region_Mouse_Over))
            {
                // this is a event driven method
                // which does not obey the normal update process
                // or it will cause sudden graphical changes in screen
                this.Transit();
                this.Succeed();
            }
            else
            {
                this.Fail();
            }
        }

        #endregion
    }
}