namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

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

        public override void Update(GameTime time)
        {
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (!this.ItemControl.Active && !this.Item.IsEnabled(ItemState.Item_Is_Dragging))
            {
                return;
            }

            this.DrawNameFrame(graphics, 255);

            if (ItemData.IsTitle)
            {
                this.FillNameFrameWith(graphics, this.ItemSettings.NameFrameTitleColor, alpha);
            }

            this.FillNameFrameWith(graphics, this.ItemSettings.NameFrameRunningColor, alpha);

            this.DrawName(255);
            this.DrawIdFrame(255);
            this.DrawId(TODO, 255);
        }

        #endregion Draw
    }
}