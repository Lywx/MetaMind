namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemFrameControl : ViewItemFrameControl
    {
        public KnowledgeItemFrameControl(IViewItem item)
            : base(item)
        {
            this.NameFrame = new ItemEntryFrame(item);
            this.IdFrame   = new ItemEntryFrame(item);
        }

        ~KnowledgeItemFrameControl()
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
                }

                this.NameFrame = null;

                if (this.IdFrame != null)
                {
                    this.IdFrame.Dispose();
                }

                this.IdFrame = null;
            }
            finally
            {
                base.Dispose();
            }
        }

        public ItemEntryFrame IdFrame { get; private set; }

        public ItemEntryFrame NameFrame { get; private set; }

        private Vector2 IdFrameLocation
        {
            get { return this.RootFrameLocation - new Vector2(this.ItemSettings.IdFrameSize.X, 0); }
        }

        private Vector2 NameFrameLocation
        {
            get { return this.RootFrameLocation; }
        }

        private Vector2 RootFrameLocation
        {
            get
            {
                if (!this.Item.IsEnabled(ItemState.Item_Dragging) && !this.Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return PointExt.ToVector2(this.ViewControl.Scroll.RootCenterPoint(this.ItemControl.Id))
                           + new Vector2(this.ItemSettings.IdFrameSize.X, 0);
                }
                else if (this.Item.IsEnabled(ItemState.Item_Swaping))
                {
                    return this.ViewControl.Swap.RootCenterPoint().ToVector2() + new Vector2(this.ItemSettings.IdFrameSize.X, 0);
                }
                else
                {
                    return this.RootFrame.Location.ToVector2();
                }
            }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            base          .UpdateInput(gameTime);

            this.NameFrame.UpdateInput(gameTime);
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
    }
}