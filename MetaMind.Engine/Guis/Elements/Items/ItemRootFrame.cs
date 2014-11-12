namespace MetaMind.Engine.Guis.Elements.Items
{
    using MetaMind.Engine.Guis.Elements.Frames;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    public interface IItemRootFrame : IDraggableFrame
    {
        void Disable();

        void Enable();
    }

    public class ItemRootFrame : DraggableFrame, IItemRootFrame
    {
        private IViewItem item;

        public ItemRootFrame(IViewItem item)
        {
            this.item = item;

            this.MouseLeftClicked += this.SelectItsItem;
            this.MouseLeftClickedOutside += this.UnselectItsItem;
        }

        private void UnselectItsItem(object sender, FrameEventArgs e)
        {
            this.item.ItemControl.MouseUnselectIt();
        }

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
            this.item.ItemControl.MouseSelectIt();
        }
    }
}