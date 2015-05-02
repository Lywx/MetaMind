namespace MetaMind.Engine.Guis.Widgets.Items.Swaps
{
    public class ViewItemSmartSwapProcess : ViewItemSwapProcess
    {
        public ViewItemSmartSwapProcess()
        {
        }

        protected override void SwapAroundView()
        {
            this.SwapDataInList();
            base.SwapAroundView();
        }

        protected override void SwapInView()
        {
            this.SwapDataInList();
            base.SwapInView();
        }
    }
}