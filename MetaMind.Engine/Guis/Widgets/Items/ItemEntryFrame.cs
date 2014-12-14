namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System.Diagnostics;

    using MetaMind.Engine.Guis.Elements;

    public interface IItemEntryFrame : IPickableFrame
    {
        void Disable();

        void Enable();
    }

    public class ItemEntryFrame : PickableFrame, IItemEntryFrame
    {
        public ItemEntryFrame(IItemObject item)
        {
            this.Item = item;
        }

        private IItemObject Item { get; set; }

        public void Disable()
        {
            this.Disable(FrameState.Frame_Active);
        }

        public override void Dispose()
        {
            try
            {
                this.Item = null;
            }
            finally
            {
                base.Dispose();
            }

            Debug.WriteLine("ItemRootFrame Destruction");
        }

        public void Enable()
        {
            this.Enable(FrameState.Frame_Active);
        }
    }
}