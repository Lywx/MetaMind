namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemFrameControl : ViewItemFrameControl
    {
        public KnowledgeItemFrameControl(IViewItem item)
            : base(item)
        {
            this.NameFrame = new ItemDataFrame(item);
            this.IdFrame   = new ItemDataFrame(item);
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

        public PickableFrame IdFrame { get; private set; }

        public PickableFrame NameFrame { get; private set; }

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
                    return ExtPoint.ToVector2(this.ViewControl.Scroll.RootCenterPoint(this.ItemControl.Id))
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

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base          .UpdateInput(input, time);

            this.NameFrame.Update(input, time);
        }

        protected override void UpdateFrameGeometry()
        {
            this.RootFrame.Location = ExtVector2.ToPoint(this.RootFrameLocation);
            this.NameFrame.Location = ExtVector2.ToPoint(this.NameFrameLocation);
            this.IdFrame  .Location = ExtVector2.ToPoint(this.IdFrameLocation);

            this.RootFrame.Size = this.ItemSettings.RootFrameSize;
            this.NameFrame.Size = this.ItemSettings.NameFrameSize;
            this.IdFrame  .Size = this.ItemSettings.IdFrameSize;
        }
    }
}