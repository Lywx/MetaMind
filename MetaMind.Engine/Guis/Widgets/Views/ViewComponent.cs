namespace MetaMind.Engine.Guis.Widgets.Views
{
    /// <summary>
    /// ViewComponent hooks all neccesary external information to the View object, 
    /// which allows view-wise substitution of settings. The dynamic typing allows 
    /// customization 
    /// </summary>
    public class ViewComponent : GameControllableEntity, IViewComponent
    {
        #region Constructors

        protected ViewComponent(IView view)
        {
            this.View = view;
        }

        #endregion

        #region Direct Dependency

        public IView View { get; private set; }

        #endregion

        #region IViewComponent

        public dynamic ItemSettings
        {
            get
            {
                return this.View.ItemSettings;
            }
        }

        public dynamic ViewLogic
        {
            get
            {
                return this.View.ViewLogic;
            }
        }

        public dynamic ViewSettings
        {
            get
            {
                return this.View.ViewSettings;
            }
        }

        #endregion
    }
}