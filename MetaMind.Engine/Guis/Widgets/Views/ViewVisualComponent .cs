namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IViewVisualComponent : IViewComponent
    {
        void Draw(GameTime gameTime, byte alpha);
    }

    public class ViewVisualComponent : ViewComponent, IViewVisualComponent
    {
        protected ViewVisualComponent(IView view, ICloneable viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public virtual void Draw(GameTime gameTime, byte alpha)
        {
        }
    }
}