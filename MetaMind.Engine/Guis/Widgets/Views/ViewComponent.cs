namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Guis.Widgets.Views.Extensions;

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

        public IViewExtension ViewExtension { get;private set; }

        #endregion
    }
}