namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Concepts.MotivationEntries;

    public class MotivationItemExchangeProcess : ViewItemExchangeProcess
    {
        public MotivationItemExchangeProcess(IViewItem draggingItem, IView targetView)
            : base(draggingItem, targetView)
        {
        }

        protected override void ExchangeToView()
        {
            // remove data in original view
            this.DraggingItem.ViewControl.ItemFactory.RemoveData(this.DraggingItem);

            // change data position
            MotivationSpace targetSpace = this.TargetView.ViewSettings.Space;
            var position = this.TargetSelection.PreviousSelectedId != null
                               ? (int)this.TargetSelection.PreviousSelectedId
                               : 0;

            this.DraggingItem.ItemData.CopyToSpace(targetSpace, position);

            base.ExchangeToView();
        }
    }
}