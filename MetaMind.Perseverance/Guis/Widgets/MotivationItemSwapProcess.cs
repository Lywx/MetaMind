namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class MotivationItemSwapProcess : ViewItemSwapProcess
    {
        public MotivationItemSwapProcess(IViewItem draggingItem, IViewItem swappingItem)
            : base(draggingItem, swappingItem)
        {
        }

        protected override void SwapAroundView()
        {
            // remove data in original view
            this.SwappingItem.ViewControl.ItemFactory.RemoveData(this.SwappingItem);
            this.DraggingItem.ViewControl.ItemFactory.RemoveData(this.DraggingItem);

            // now the data source clean

            // change data position
            this.SwappingItem.ItemData.CopyToSpace(this.DraggingItem.ViewSettings.Space, this.DraggingItem.ItemControl.Id);
            this.DraggingItem.ItemData.CopyToSpace(this.SwappingItem.ViewSettings.Space, this.SwappingItem.ItemControl.Id);

            // now the data source ready
            // but the item not in the right view
            base.SwapAroundView();
        }

        protected override void SwapInView()
        {
            // change data position
            this.SwappingItem.ItemData.SwapWithInSpace(this.DraggingItem .ViewSettings.Space, this.DraggingItem.ItemData);

            base.SwapInView();
        }
    }
}