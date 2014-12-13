namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IVisualViewComponent : IViewComponent
    {
        void Draw(GameTime gameTime);
    }

    public class VisualViewComponent : ViewComponent, IVisualViewComponent
    {
        protected VisualViewComponent(IView view, ICloneable viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }
    }
}