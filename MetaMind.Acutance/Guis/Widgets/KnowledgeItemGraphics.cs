namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class KnowledgeItemGraphics : TraceItemGraphics
    {
        #region Constructors

        public KnowledgeItemGraphics(IViewItem item)
            : base(item)
        {
        }

        #endregion Constructors

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            if (!this.ItemControl.Active && !this.Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(255);

            if (ItemData.IsTitle)
            {
                this.FillNameFrameWith(ItemSettings.NameFrameCommandColor, alpha);
            }

            this.FillNameFrameWith(ItemSettings.NameFrameRunningColor, alpha);

            this.DrawName(255);
            this.DrawIdFrame(255);
            this.DrawId(255);
        }

        #endregion Draw
    }
}