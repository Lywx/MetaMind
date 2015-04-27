namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Elements;

    public class ItemRootFrame : DraggableFrame, IItemRootFrame
    {
        public ItemRootFrame(IViewItem item)
        {
            this.Item = item;

            this.MouseLeftPressed        += this.SelectItsItem;
            this.MouseLeftPressedOutside += this.UnselectItsItem;
        }

        ~ItemRootFrame()
        {
            this.Dispose();
        }

        private IViewItem Item { get; set; }

        public void Disable()
        {
            this[FrameState.Frame_Is_Active] = () => false;
        }

        public override void Dispose()
        {
            this.MouseLeftPressed        -= this.SelectItsItem;
            this.MouseLeftPressedOutside -= this.UnselectItsItem;

            this.Item = null;

            base.Dispose();
        }

        public void Enable()
        {
            this[FrameState.Frame_Is_Active] = () => true;
        }

        private void SelectItsItem(object sender, FrameEventArgs e)
        {
            ((ViewItemControl1D)this.Item.ItemControl).MouseSelectsIt();
        }

        private void UnselectItsItem(object sender, FrameEventArgs e)
        {
            ((ViewItemControl1D)this.Item.ItemControl).MouseUnselectsIt();
        }
    }
}