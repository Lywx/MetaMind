namespace MetaMind.Engine.Guis.Widgets.Items
{
    public class SmartItemSwapProcess : ViewItemSwapProcess
    {
        public SmartItemSwapProcess()
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