namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class TaskItemFrameControl : ViewItemFrameControl
    {
        public ItemDataFrame NameFrame { get; private set; }

        public ItemDataFrame IdFrame { get; private set; }

        public ItemDataFrame ProgressFrame { get; private set; }

        public ItemDataFrame ExperienceFrame { get; private set; }

        public TaskItemFrameControl(IViewItem item)
            : base(item)
        {
            this.NameFrame       = new ItemDataFrame(item);
            this.IdFrame         = new ItemDataFrame(item);
            this.ExperienceFrame = new ItemDataFrame(item);
            this.ProgressFrame   = new ItemDataFrame(item);
        }

        ~TaskItemFrameControl()
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

                if (this.ProgressFrame != null)
                {
                    this.ProgressFrame.Dispose();
                    this.ProgressFrame = null;
                }
            }
            finally
            {
                base.Dispose();
            }
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            base.UpdateInput(gameInput, gameTime);

            this.NameFrame      .UpdateInput(gameInput, gameTime);
            this.IdFrame        .UpdateInput(gameInput, gameTime);
            this.ExperienceFrame.UpdateInput(gameInput, gameTime);
            this.ProgressFrame  .UpdateInput(gameInput, gameTime);
        }

        protected override void UpdateFrameGeometry()
        {
            int middleWidth = (this.ItemSettings.NameFrameSize.X - this.ItemSettings.IdFrameSize.X) / 2;

            ((TaskItemSettings)this.ItemSettings).ExperienceFrameSize.X = middleWidth;
            ((TaskItemSettings)this.ItemSettings).ProgressFrameSize.X   = middleWidth;

            this.RootFrame         .Location = ExtVector2.ToPoint(this.RootFrameLocation);
            this.NameFrame         .Location = ExtVector2.ToPoint(this.NameFrameLocation);
            this.IdFrame           .Location = ExtVector2.ToPoint(this.IdFrameLocation);
            this.ExperienceFrame   .Location = ExtVector2.ToPoint(this.ExperienceFrameLocation);
            this.ProgressFrame     .Location = ExtVector2.ToPoint(this.ProgressFrameLocation);

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
                    return ExtPoint.ToVector2(this.ViewControl.Scroll.RootCenterPoint(this.ItemControl.Id)) + new Vector2(0, this.ItemSettings.IdFrameSize.Y);
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