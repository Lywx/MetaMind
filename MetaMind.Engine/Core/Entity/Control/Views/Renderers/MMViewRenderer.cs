namespace MetaMind.Engine.Core.Entity.Control.Views.Renderers
{
    using Entity.Common;
    using Item;
    using Microsoft.Xna.Framework;

    public class MMViewRenderer : MMViewRenderComponent, IMMViewRenderer
    {
        public MMViewRenderer(IMMView view)
            : base(view)
        {
        }

        public override void Draw(GameTime time)
        {
            this.DrawComponents(time);
            this.DrawItems(time);
        }

        protected virtual void DrawComponents(GameTime time, byte alpha)
        {
            foreach (var component in this.ViewComponents)
            {
                ((IMMDrawable)component.Value).Draw(time);
            }
        }

        protected virtual void DrawItems(GameTime time, byte alpha)
        {
            foreach (var item in this.View.Items.ToArray())
            {
                // TODO: Possible separation of active and inactive storage and looping to improve CPU performance
                // TODO: Possible separate implementation for different views (1d or 2d views)
                if (item[MMViewItemState.Item_Is_Active]())
                {
                    item.Draw(time, alpha);
                }
            }
        }
    }
}