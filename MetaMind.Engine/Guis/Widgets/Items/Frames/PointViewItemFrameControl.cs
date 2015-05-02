namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using MetaMind.Engine.Guis.Widgets.Items.Extensions;

    public class PointViewItemFrameControl : ViewItemFrameControl
    {
        public PointViewItemFrameControl(IViewItem item)
            : base(item)
        {
        }

        protected override void UpdateFrameGeometry()
        {
            if (!this.Item[ItemState.Item_Is_Dragging]() && 
                !this.Item[ItemState.Item_Is_Swaping]())
            {
                this.RootFrame.Center = this.ItemExtension.Get<PointViewExtension>().ViewScroll.RootCenterPosition(this.ItemLogic.Id);
            }
            else if (this.Item[ItemState.Item_Is_Swaping]())
            {
                this.RootFrame.Center = this.ViewSwap.RootCenterPosition;
            }
        }
    }
}