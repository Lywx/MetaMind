namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    public class ViewComponent : GameControllableEntity, IViewComponent
    {
        #region Constructors

        protected ViewComponent(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            this.View         = view;
            this.ViewSettings = viewSettings;

            this.ItemSettings = itemSettings;
        }

        #endregion

        #region IViewComponent

        public dynamic ViewControl
        {
            get
            {
                return this.View.Logic;
            }
        }

        public IView View { get; private set; }

        public dynamic ViewSettings { get; private set; }

        public dynamic ItemSettings { get; private set; }

        #endregion
    }
}