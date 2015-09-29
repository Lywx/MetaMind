namespace MetaMind.Engine.Gui.Controls.Views.Visuals
{
    using Item;
    using Microsoft.Xna.Framework;
    using Service;
    using IDrawable = Engine.IDrawable;

    public class ViewVisual : ViewVisualComponent, IViewVisual
    {
        public ViewVisual(IView view)
            : base(view)
        {
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.DrawComponents(graphics, time, alpha);
            this.DrawItems(graphics, time, alpha);
        }

        protected virtual void DrawComponents(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var component in this.ViewComponents)
            {
                ((IDrawable)component.Value).Draw(graphics, time, alpha);
            }
        }

        protected virtual void DrawItems(IGameGraphicsService graphics, GameTime time, byte alpha)
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