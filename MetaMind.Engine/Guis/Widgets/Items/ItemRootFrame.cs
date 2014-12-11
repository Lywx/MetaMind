namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.ViewItems;

    public interface IItemRootFrame : IDraggableFrame
    {
        void Disable();

        void Enable();
    }

    public class ItemRootFrame : DraggableFrame, IItemRootFrame
    {
        public ItemRootFrame(IViewItem item)
        {
            this.Item = item;

            this.MouseLeftClicked += this.SelectItsItem;
            this.MouseLeftClickedOutside += this.UnselectItsItem;
        }

        private IViewItem Item { get; set; }

        public void Disable()
        {
            this.Disable(FrameState.Frame_Active);
        }

        public void Enable()
        {
            this.Enable(FrameState.Frame_Active);
        }

        private void SelectItsItem(object sender, FrameEventArgs e)
        {
            this.Item.ItemControl.MouseSelectsIt();
        }

        private void UnselectItsItem(object sender, FrameEventArgs e)
        {
            this.Item.ItemControl.MouseUnselectsIt();
        }
    }
}