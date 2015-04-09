namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using Microsoft.Xna.Framework;

    public interface IViewComponent
    {
        dynamic ViewControl  { get; }

        IView   View         { get; }

        dynamic ViewSettings { get; }

        dynamic ItemSettings { get; }
    }

    public class ViewComponent : GameEngineAccess, IViewComponent
    {
        public dynamic ViewControl { get { return this.View.Control; } }

        public IView View { get; private set; }

        public dynamic ViewSettings { get; private set; }

        public dynamic ItemSettings { get; private set; }

        protected ViewComponent(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            this.View         = view;
            this.ViewSettings = viewSettings;
            this.ItemSettings = itemSettings;
        }

        public virtual void UpdateStructure(GameTime gameTime)
        {
        }

        public virtual void UpdateInput(GameTime gameTime)
        {
        }
    }
}