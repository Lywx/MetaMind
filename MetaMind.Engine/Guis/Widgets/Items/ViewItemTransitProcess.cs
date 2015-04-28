namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System.Diagnostics;

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
            // Source part
            this.SrcItem = srcItem as IViewItemExchangable;
            Debug.Assert(this.SrcItem != null, "Item is not exchangable.");

            this.srcFrame = srcItem.ItemLogic.RootFrame;

            // Destination part
            this.DesView      = desView;
            this.desRegion    = desView.Logic.Region;
            this.DesSelection = desView.Logic.Selection;

            this.Initialize();
        }

        private void Initialize()
        {
            this.SrcItem[ItemState.Item_Is_Transiting] = () => true;

            this.srcFrame.MouseDropped += this.TransitWhenMouseInsideRegion;
        }

        #endregion Constructors

        #region Transit Transition

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
            this.TransitTerminate();
        }

        public override void OnSuccess()
        {
            this.TransitTerminate();
        }

        #endregion Transit Transition

        #region Transit Update

        public override void Update(GameTime time)
        {
        }

        #endregion Exchange Update

        #region Transit Operations

        protected virtual void Transit()
        {
            this.SrcItem.View[ViewState.View_Has_Focus] = () => false;
            this.SrcItem.ViewLogic.Selection.Clear();

            var position = this.DesSelection.PreviousSelectedId != null ? (int)this.DesSelection.PreviousSelectedId : 0;

            this.SrcItem.ExchangeTo(this.DesView, position);
        }

        /// <summary>
        /// Stop item transiting.
        /// </summary>
        private void TransitTerminate()
        {
            this.SrcItem[ItemState.Item_Is_Transiting] = () => false;
        }

        #endregion Exchange Operations

        #region Transit Trigger

        private void TransitWhenMouseInsideRegion(object sender, FrameEventArgs e)
        {
            if (this.desRegion[RegionState.Mouse_Is_Over]())
            {
                // This is a event driven method which does not obey the 
                // normal update process or it will cause sudden graphical 
                // changes in screen.
                this.Transit();
                this.Succeed();
            }
            else
            {
                this.Fail();
            }
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            this.srcFrame.MouseDropped -= this.TransitWhenMouseInsideRegion;

            base.Dispose();
        }


        #endregion
    }
}