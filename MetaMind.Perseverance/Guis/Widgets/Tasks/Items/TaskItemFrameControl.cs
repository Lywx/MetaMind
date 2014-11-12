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
            NameFrame       = new ItemEntryFrame(item);
            IdFrame         = new ItemEntryFrame(item);
            ExperienceFrame = new ItemEntryFrame(item);
            ProgressFrame   = new ItemEntryFrame(item);
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

            NameFrame      .UpdateInput(gameTime);
            IdFrame        .UpdateInput(gameTime);
            ExperienceFrame.UpdateInput(gameTime);
            ProgressFrame  .UpdateInput(gameTime);
        }

        protected override void UpdateFrameGeometry()
        {
            ((TaskItemSettings)ItemSettings).ExperienceFrameSize.X = (ItemSettings.NameFrameSize.X - ItemSettings.IdFrameSize.X) / 2;
            ((TaskItemSettings)ItemSettings).ProgressFrameSize.X = (ItemSettings.NameFrameSize.X - ItemSettings.IdFrameSize.X) / 2;

            RootFrame      .Location = Vector2Ext.ToPoint(RootFrameLocation);
            NameFrame      .Location = Vector2Ext.ToPoint(NameFrameLocation);
            IdFrame        .Location = Vector2Ext.ToPoint(IdFrameLocation);
            ExperienceFrame.Location = Vector2Ext.ToPoint(ExperienceFrameLocation);
            ProgressFrame  .Location = Vector2Ext.ToPoint(ProgressFrameLocation);

            RootFrame      .Size = ItemSettings.RootFrameSize;
            NameFrame      .Size = ItemSettings.NameFrameSize;
            IdFrame        .Size = ItemSettings.IdFrameSize;
            ExperienceFrame.Size = ItemSettings.ExperienceFrameSize;
            ProgressFrame  .Size = ItemSettings.ProgressFrameSize;
        }

        private Vector2 RootFrameLocation
        {
            get
            {
                if (!Item.IsEnabled(ItemState.Item_Dragging) && !Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return PointExt.ToVector2(ViewControl.Scroll.RootCenterPoint(ItemControl.Id)) + new Vector2(0, ItemSettings.IdFrameSize.Y);
                }
                else if (Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return ViewControl.Swap.RootCenterPoint().ToVector2() + new Vector2(0, ItemSettings.IdFrameSize.Y);
                }
                else
                {
                    return RootFrame.Location.ToVector2();
                }
            }
        }

        private Vector2 ProgressFrameLocation
        {
            get { return ExperienceFrameLocation + new Vector2(ItemSettings.ExperienceFrameSize.X, 0); }
        }

        private Vector2 IdFrameLocation
        {
            get { return NameFrameLocation + new Vector2(0, -ItemSettings.IdFrameSize.Y); }
        }

        private Vector2 NameFrameLocation
        {
            get { return RootFrameLocation; }
        }

        private Vector2 ExperienceFrameLocation
        {
            get { return IdFrameLocation + new Vector2(ItemSettings.IdFrameSize.X, 0); }
        }
    }
}