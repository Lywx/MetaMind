namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;

    using Microsoft.Xna.Framework;

    public class TraceItemFrameControl : ViewItemFrameControl
    {
        public ItemEntryFrame NameFrame { get; private set; }

        public ItemEntryFrame IdFrame { get; private set; }

        public TraceItemFrameControl(IViewItem item)
            : base(item)
        {
            this.NameFrame = new ItemEntryFrame(item);
            this.IdFrame   = new ItemEntryFrame(item);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

            this.NameFrame.UpdateInput(gameTime);
            this.IdFrame  .UpdateInput(gameTime);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootFrame.Location = Vector2Ext.ToPoint(this.RootFrameLocation);
            this.NameFrame.Location = Vector2Ext.ToPoint(this.NameFrameLocation);
            this.IdFrame  .Location = Vector2Ext.ToPoint(this.IdFrameLocation);

            this.RootFrame.Size = this.ItemSettings.RootFrameSize;
            this.NameFrame.Size = this.ItemSettings.NameFrameSize;
            this.IdFrame  .Size = this.ItemSettings.IdFrameSize;
        }

        private Vector2 RootFrameLocation
        {
            get
            {
                if (!Item.IsEnabled(ItemState.Item_Dragging) && !Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return PointExt.ToVector2(ViewControl.Scroll.RootCenterPoint(ItemControl.Id)) + new Vector2(0, 50);
                }
                else if (Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return ViewControl.Swap.RootCenterPoint().ToVector2() + new Vector2(0, 50);
                }
                else
                {
                    return RootFrame.Location.ToVector2();
                }
            }
        }

        private Vector2 IdFrameLocation
        {
            get { return this.RootFrameLocation; }
        }

        private Vector2 NameFrameLocation
        {
            get { return this.RootFrameLocation + new Vector2(ItemSettings.IdFrameSize.X, 0); }
        }
    }
}