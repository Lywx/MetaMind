namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Elements;

    public interface IItemRootFrame : IDraggableFrame
    {
    }

    public class ItemRootFrame : DraggableFrame, IItemRootFrame
    {
        public ItemRootFrame(IViewItem item)
        {
            this.Item = item;

            this.MouseLeftClicked        += this.SelectItsItem;
            this.MouseLeftClickedOutside += this.UnselectItsItem;
        }

        ~ItemRootFrame()
        {
            this.Dispose();
        }

        private IViewItem Item { get; set; }

        public void Disable()
        {
            this.Disable(FrameState.Frame_Active);
        }

        public override void Dispose()
        {
            this.MouseLeftClicked        -= this.SelectItsItem;
            this.MouseLeftClickedOutside -= this.UnselectItsItem;

            this.Item = null;

            base.Dispose();
        }

        public void Enable()
        {
            this.Enable(FrameState.Frame_Active);
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