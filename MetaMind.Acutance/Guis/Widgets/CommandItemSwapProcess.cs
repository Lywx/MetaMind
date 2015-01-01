namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class CommandItemSwapProcess : ViewItemSwapProcess
    {
        public CommandItemSwapProcess(IViewItem draggingItem, IViewItem swappingItem, List<CommandEntry> source)
            : base(draggingItem, swappingItem, source)
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