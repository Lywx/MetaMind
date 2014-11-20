namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using MetaMind.Engine.Guis.Elements.ViewItems;

    public class MotivationItemSwapProcess : ViewItemSwapProcess
    {
        public MotivationItemSwapProcess(IViewItem draggedItem, IViewItem swappingItem)
            : base(draggedItem, swappingItem)
        {
        }

        protected override void SwapAroundView()
        {
            // remove data in original view
            this.SwappingItem.ViewControl.ItemFactory.RemoveData(this.SwappingItem);
            this.DraggedItem .ViewControl.ItemFactory.RemoveData(this.DraggedItem);

            // now the data source clean
            
            // change data position
            this.SwappingItem.ItemData.CopyToSpace(this.DraggedItem .ViewSettings.Space, this.DraggedItem .ItemControl.Id);
            this.DraggedItem .ItemData.CopyToSpace(this.SwappingItem.ViewSettings.Space, this.SwappingItem.ItemControl.Id);

            // now the data source ready
            // but the item not in the right view
            base.SwapAroundView();
        }

        protected override void SwapInView()
        {
            // change data position
            this.SwappingItem.ItemData.SwapWithInSpace(this.DraggedItem .ViewSettings.Space, this.DraggedItem.ItemData);

            base.SwapInView();
        }
    }
}