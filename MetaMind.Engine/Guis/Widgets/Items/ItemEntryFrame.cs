namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Elements;

    public interface IItemEntryFrame : IPickableFrame
    {
        void Enable();

        void Disable();
    }

    public class ItemEntryFrame : PickableFrame, IItemEntryFrame
    {
        private IItemObject item;

        public ItemEntryFrame(IItemObject item)
        {
            this.item = item;

            this.MouseRightClicked += this.SwitchEditing;
            this.MouseRightClickedOutside += this.QuitEditing;
        }

        public void Enable()
        {
            this.Enable(FrameState.Frame_Active);
        }

        public void Disable()
        {
            this.Disable(FrameState.Frame_Active);
        }

        private void SwitchEditing(object sender, FrameEventArgs e)
        {
            if (!this.item.IsEnabled(ItemState.Item_Active))
            {
                return;
            }

            if (this.item.IsEnabled(ItemState.Item_Editing))
            {
                this.item.Disable(ItemState.Item_Editing);
            }
            else
            {
                this.item.Enable(ItemState.Item_Editing);
            }
        }

        private void QuitEditing(object sender, FrameEventArgs e)
        {
            this.item.Disable(ItemState.Item_Editing);
        }
    }
}