namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;

    using Microsoft.Xna.Framework;

    public class TaskItemFrameControl : ViewItemFrameControl
    {
        public ItemEntryFrame NameFrame { get; private set; }

        public ItemEntryFrame IdFrame { get; private set; }

        public ItemEntryFrame ProgressFrame { get; private set; }

        public ItemEntryFrame ExperienceFrame { get; private set; }

        public TaskItemFrameControl(IViewItem item)
            : base(item)
        {
            this.NameFrame       = new ItemEntryFrame(item);
            this.IdFrame         = new ItemEntryFrame(item);
            this.ExperienceFrame = new ItemEntryFrame(item);
            this.ProgressFrame   = new ItemEntryFrame(item);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

            this.NameFrame      .UpdateInput(gameTime);
            this.IdFrame        .UpdateInput(gameTime);
            this.ExperienceFrame.UpdateInput(gameTime);
            this.ProgressFrame  .UpdateInput(gameTime);
        }

        protected override void UpdateFrameGeometry()
        {
            int middleWidth = (this.ItemSettings.NameFrameSize.X - this.ItemSettings.IdFrameSize.X) / 2;

            ((TaskItemSettings)this.ItemSettings).ExperienceFrameSize.X = middleWidth;
            ((TaskItemSettings)this.ItemSettings).ProgressFrameSize.X   = middleWidth;

            this.RootFrame         .Location = Vector2Ext.ToPoint(this.RootFrameLocation);
            this.NameFrame         .Location = Vector2Ext.ToPoint(this.NameFrameLocation);
            this.IdFrame           .Location = Vector2Ext.ToPoint(this.IdFrameLocation);
            this.ExperienceFrame   .Location = Vector2Ext.ToPoint(this.ExperienceFrameLocation);
            this.ProgressFrame     .Location = Vector2Ext.ToPoint(this.ProgressFrameLocation);

            this.RootFrame         .Size = this.ItemSettings.RootFrameSize;
            this.NameFrame         .Size = this.ItemSettings.NameFrameSize;
            this.IdFrame           .Size = this.ItemSettings.IdFrameSize;
            this.ExperienceFrame   .Size = this.ItemSettings.ExperienceFrameSize;
            this.ProgressFrame     .Size = this.ItemSettings.ProgressFrameSize;
        }

        private Vector2 RootFrameLocation
        {
            get
            {
                if (!this.Item.IsEnabled(ItemState.Item_Dragging) && !this.Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return PointExt.ToVector2(this.ViewControl.Scroll.RootCenterPoint(this.ItemControl.Id)) + new Vector2(0, this.ItemSettings.IdFrameSize.Y);
                }
                else if (this.Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return this.ViewControl.Swap.RootCenterPoint().ToVector2() + new Vector2(0, this.ItemSettings.IdFrameSize.Y);
                }
                else
                {
                    return this.RootFrame.Location.ToVector2();
                }
            }
        }

        private Vector2 RationFrameLocation
        {
            get { return this.ProgressFrameLocation + new Vector2(this.ItemSettings.ProgressFrameSize.X, 0); }
        }

        private Vector2 ProgressFrameLocation
        {
            get { return this.ExperienceFrameLocation + new Vector2(this.ItemSettings.ExperienceFrameSize.X, 0); }
        }

        private Vector2 IdFrameLocation
        {
            get { return this.NameFrameLocation + new Vector2(0, -this.ItemSettings.IdFrameSize.Y); }
        }

        private Vector2 NameFrameLocation
        {
            get { return this.RootFrameLocation; }
        }

        private Vector2 ExperienceFrameLocation
        {
            get { return this.IdFrameLocation + new Vector2(this.ItemSettings.IdFrameSize.X, 0); }
        }
    }
}