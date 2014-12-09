namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Elements.ViewItems;

    public class TraceItemSwapProcess : ViewItemSwapProcess
    {
        public TraceItemSwapProcess(IViewItem draggedItem, IViewItem swappingItem)
            : base(draggedItem, swappingItem)
        {
        }

        protected override void SwapAroundView()
        {
            this.SwapDataInList(Acutance.Adventure.Tracelist.Traces);
            base.SwapAroundView();
        }

        protected override void SwapInView()
        {
            this.SwapDataInList(Acutance.Adventure.Tracelist.Traces);
            base.SwapInView();
        }
    }
}