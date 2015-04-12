namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class TraceItemFrameControl : ViewItemFrameControl
    {
        public ItemDataFrame NameFrame { get; private set; }

        public ItemDataFrame IdFrame { get; private set; }

        public ItemDataFrame ExperienceFrame { get; private set; }

        public TraceItemFrameControl(IViewItem item)
            : base(item)
        {
            this.NameFrame       = new ItemDataFrame(item);
            this.IdFrame         = new ItemDataFrame(item);
            this.ExperienceFrame = new ItemDataFrame(item);
        }

        ~TraceItemFrameControl()
        {
            this.Dispose();
        }

        public override void Dispose()
        {
            try
            {
                if (this.NameFrame != null)
                {
                    this.NameFrame.Dispose();
                    this.NameFrame = null;
                }

                if (this.IdFrame != null)
                {
                    this.IdFrame.Dispose();
                    this.IdFrame = null;
                }

                if (this.ExperienceFrame != null)
                {
                    this.ExperienceFrame.Dispose();
                    this.ExperienceFrame = null;
                }
            }
            finally
            {
                base.Dispose();
            }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base.UpdateInput(gameTime);

            this.NameFrame      .Update(gameInput, gameTime);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootFrame      .Location = ExtVector2.ToPoint(this.RootFrameLocation);
            this.NameFrame      .Location = ExtVector2.ToPoint(this.NameFrameLocation);
            this.IdFrame        .Location = ExtVector2.ToPoint(this.IdFrameLocation);
            this.ExperienceFrame.Location = ExtVector2.ToPoint(ExperienceFrameLocation);

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
                    return ExtPoint.ToVector2(ViewControl.Scroll.RootCenterPoint(ItemControl.Id))
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