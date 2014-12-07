using MetaMind.Engine.Extensions;

using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;

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
            int middleWidth = (ItemSettings.NameFrameSize.X - ItemSettings.IdFrameSize.X) / 2;

            ((TaskItemSettings)ItemSettings).ExperienceFrameSize.X = middleWidth;
            ((TaskItemSettings)ItemSettings).ProgressFrameSize.X   = middleWidth;

            this.RootFrame         .Location = Vector2Ext.ToPoint(RootFrameLocation);
            this.NameFrame         .Location = Vector2Ext.ToPoint(NameFrameLocation);
            this.IdFrame           .Location = Vector2Ext.ToPoint(IdFrameLocation);
            this.ExperienceFrame   .Location = Vector2Ext.ToPoint(ExperienceFrameLocation);
            this.ProgressFrame     .Location = Vector2Ext.ToPoint(ProgressFrameLocation);

            this.RootFrame         .Size = ItemSettings.RootFrameSize;
            this.NameFrame         .Size = ItemSettings.NameFrameSize;
            this.IdFrame           .Size = ItemSettings.IdFrameSize;
            this.ExperienceFrame   .Size = ItemSettings.ExperienceFrameSize;
            this.ProgressFrame     .Size = ItemSettings.ProgressFrameSize;
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
            get { return this.ProgressFrameLocation + new Vector2(ItemSettings.ProgressFrameSize.X, 0); }
        }

        private Vector2 ProgressFrameLocation
        {
            get { return this.ExperienceFrameLocation + new Vector2(ItemSettings.ExperienceFrameSize.X, 0); }
        }

        private Vector2 IdFrameLocation
        {
            get { return this.NameFrameLocation + new Vector2(0, -ItemSettings.IdFrameSize.Y); }
        }

        private Vector2 NameFrameLocation
        {
            get { return this.RootFrameLocation; }
        }

        private Vector2 ExperienceFrameLocation
        {
            get { return this.IdFrameLocation + new Vector2(ItemSettings.IdFrameSize.X, 0); }
        }
    }
}