namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using Entities;
    using Item;
    using Microsoft.Xna.Framework;
    using Service;

    public class ViewVisual : ViewVisualComponent, IViewVisual
    {
        public ViewVisual(IMMViewNode view)
            : base(view)
        {
        }

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawComponents(graphics, time, alpha);
            this.DrawItems(graphics, time, alpha);
        }

        protected virtual void DrawComponents(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var component in this.ViewComponents)
            {
                ((IMMDrawable)component.Value).Draw(graphics, time, alpha);
            }
        }

        protected virtual void DrawItems(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var item in this.View.ItemsRead.ToArray())
            {
                // TODO: Possible separation of active and inactive storage and looping to improve CPU performance
                // TODO: Possible separate implementation for different views (1d or 2d views)
                if (item[ViewItemState.Item_Is_Active]())
                {
                    item.Draw(graphics, time, alpha);
                }
            }
        }
    }
}