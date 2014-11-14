namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Processes;
    using MetaMind.Engine.Guis.Elements.Frames;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Regions;
    using MetaMind.Engine.Guis.Elements.Views;
    using Microsoft.Xna.Framework;
    using System.Diagnostics;

    public class ViewItemExchangeProcess : ProcessBase
    {
        private IItemRootFrame        draggingFrame;

        private IRegion               targetRegion;

        public ViewItemExchangeProcess(IViewItem draggingItem, IView targetView)
        {
            this.DraggingItem = draggingItem as IViewItemExchangable;

            Debug.Assert(this.DraggingItem != null, "Dragging item is not exchangable");

            this.draggingFrame     = draggingItem.ItemControl.RootFrame;

            this.TargetView      = targetView;
            this.targetRegion    = targetView.Control.Region;
            this.TargetSelection = targetView.Control.Selection;

            this.DraggingItem.Enable(ItemState.Item_Exchanging);
            this.draggingFrame.MouseDropped += this.DectectMouseInsideRegion;
        }

        protected IViewItemExchangable DraggingItem { get; private set; }

        protected IViewSelectionControl TargetSelection { get; private set; }

        protected IView TargetView { get; private set; }

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
            this.EndExchange();
        }

        public override void OnSuccess()
        {
            this.ExchangeToView();
            this.EndExchange();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (targetRegion.IsEnabled(RegionState.Region_Mouse_Over))
            {
                MessageManager.PopMessages("ASDASD");
            }
        }

        protected virtual void ExchangeToView()
        {
            this.DraggingItem.View.Disable(ViewState.View_Has_Focus);
            this.DraggingItem.ViewControl.Selection.Clear();

            var position = this.TargetSelection.PreviousSelectedId != null
                               ? (int)this.TargetSelection.PreviousSelectedId
                               : 0;

            this.DraggingItem.ExchangeTo(this.TargetView, position);
        }

        private void DectectMouseInsideRegion(object sender, FrameEventArgs e)
        {
            if (targetRegion.IsEnabled(RegionState.Region_Mouse_Over))
            {
                this.Succeed();
            }
            else
            {
                this.Fail();
            }
        }

        private void EndExchange()
        {
            // stop swapping state
            this.DraggingItem.Disable(ItemState.Item_Exchanging);
            this.draggingFrame.MouseDropped -= this.DectectMouseInsideRegion;
        }
    }
}