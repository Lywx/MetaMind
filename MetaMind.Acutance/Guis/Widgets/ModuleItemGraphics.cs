namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class ModuleItemGraphics : TraceItemGraphics
    {
        #region Constructors

        public ModuleItemGraphics(IViewItem item)
            : base(item)
        {
        }

        #endregion Constructors

        #region Update and Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            if (!this.ItemControl.Active && !this.Item.IsEnabled(ItemState.Item_Dragging))
            {
                return;
            }

            this.DrawNameFrame(gameGraphics, 255);

            if (ItemData.IsPopulating)
            {
                this.FillNameFrameWith(gameGraphics, this.ItemSettings.NameFrameRunningColor, alpha);
            }

            this.DrawName(255);
            this.DrawExperienceFrame(255);
            this.DrawExperience(255);
            this.DrawIdFrame(255);
            this.DrawId(TODO, 255);
        }

        #endregion
    }
}