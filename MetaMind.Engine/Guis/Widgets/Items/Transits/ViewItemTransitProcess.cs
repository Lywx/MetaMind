namespace MetaMind.Engine.Guis.Widgets.Items.Transits
{
    using System;

    using MetaMind.Engine.Components.Processes;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;

    using Microsoft.Xna.Framework;

    public class ViewItemTransitProcess : Process
    {
        #region Source Data

        private IItemRootFrame SrcFrame { get; set; }

        protected IViewItem SrcItem { get; private set; }

        #endregion 

        #region Destination Data

        private IRegion DesRegion { get; set; }

        protected IViewSelectionControl DesSelection { get; private set; }

        protected IView DesView { get; private set; }

        #endregion 

        #region Constructors

        public ViewItemTransitProcess(IViewItem srcItem, IItemRootFrame srcFrame, IView desView, IRegion desRegion, IViewSelectionControl desSelection)
        {
            if (srcItem == null)
            {
                throw new ArgumentNullException("srcItem");
            }

            if (srcFrame == null)
            {
                throw new ArgumentNullException("srcFrame");
            }

            this.SrcItem  = srcItem;
            this.SrcFrame = srcFrame;

            if (desView == null)
            {
                throw new ArgumentNullException("desView");
            }

            if (desRegion == null)
            {
                throw new ArgumentNullException("desRegion");
            }

            this.DesView      = desView;
            this.DesRegion    = desRegion;
            this.DesSelection = desSelection;

            this.Initialize();
        }

        private void Initialize()
        {
            this.SrcItem[ItemState.Item_Is_Transiting] = () => true;

            this.SrcFrame.MouseDropped += this.TransitWhenMouseInsideRegion;
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
            this.SrcItem.ViewLogic.ViewSelection.Clear();

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
            if (this.DesRegion[RegionState.Mouse_Is_Over]())
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
            this.SrcFrame.MouseDropped -= this.TransitWhenMouseInsideRegion;

            base.Dispose();
        }

        #endregion
    }
}