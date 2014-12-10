namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;

    using Microsoft.Xna.Framework;

    public class TraceItemFrameControl : ViewItemFrameControl
    {
        public ItemEntryFrame NameFrame { get; private set; }

        public ItemEntryFrame IdFrame { get; private set; }

        public ItemEntryFrame ExperienceFrame { get; private set; }

        public TraceItemFrameControl(IViewItem item)
            : base(item)
        {
            this.NameFrame       = new ItemEntryFrame(item);
            this.IdFrame         = new ItemEntryFrame(item);
            this.ExperienceFrame = new ItemEntryFrame(item);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

            this.NameFrame      .UpdateInput(gameTime);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootFrame      .Location = Vector2Ext.ToPoint(this.RootFrameLocation);
            this.NameFrame      .Location = Vector2Ext.ToPoint(this.NameFrameLocation);
            this.IdFrame        .Location = Vector2Ext.ToPoint(this.IdFrameLocation);
            this.ExperienceFrame.Location = Vector2Ext.ToPoint(ExperienceFrameLocation);

            this.RootFrame      .Size = ItemSettings.RootFrameSize;
            this.NameFrame      .Size = ItemSettings.NameFrameSize;
            this.IdFrame        .Size = ItemSettings.IdFrameSize;
            this.ExperienceFrame.Size = ItemSettings.ExperienceFrameSize;
        }

        private Vector2 RootFrameLocation
        {
            get
            {
                if (!Item.IsEnabled(ItemState.Item_Dragging) && !Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return PointExt.ToVector2(ViewControl.Scroll.RootCenterPoint(ItemControl.Id))
                           + new Vector2(ItemSettings.IdFrameSize.X, 0)
                           + new Vector2(ItemSettings.ExperienceFrameSize.X, 0);
                }
                else if (Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return ViewControl.Swap.RootCenterPoint().ToVector2() + new Vector2(ItemSettings.IdFrameSize.X, 0)
                           + new Vector2(ItemSettings.ExperienceFrameSize.X, 0);
                }
                else
                {
                    return RootFrame.Location.ToVector2();
                }
            }
        }

        private Vector2 IdFrameLocation
        {
            get
            {
                return this.RootFrameLocation - new Vector2(ItemSettings.ExperienceFrameSize.X, 0)
                       - new Vector2(ItemSettings.IdFrameSize.X, 0);
            }
        }

        private Vector2 NameFrameLocation
        {
            get { return this.RootFrameLocation; }
        }

        private Vector2 ExperienceFrameLocation
        {
            get { return this.RootFrameLocation - new Vector2(ItemSettings.ExperienceFrameSize.X, 0); }
        }
    }
}