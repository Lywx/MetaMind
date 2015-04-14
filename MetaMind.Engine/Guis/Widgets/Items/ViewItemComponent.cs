namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Widgets.Views;

    public interface IViewItemComponent
    {
        #region Item Components

        IViewItem Item { get; }

        dynamic ItemControl { get; }

        dynamic ItemData { get; }

        IItemGraphics ItemGraphics { get; }

        dynamic ItemSettings { get; }

        #endregion Item Components

        #region View Components

        IView View { get; }

        dynamic ViewControl { get; }

        dynamic ViewSettings { get; }

        #endregion View Components
    }

    public class ViewItemComponent : GameControllableEntity, IViewItemComponent
    {
        #region Constructors and Destructors

        protected ViewItemComponent(IViewItem item)
        {
            this.Item = item;
        }

        ~ViewItemComponent()
        {
            this.Dispose();
        }

        #endregion Constructors and Destructors

        #region Item Components

        public IViewItem Item { get; private set; }

        public dynamic ItemControl
        {
            get { return this.Item.ItemControl; }
        }

        public dynamic ItemData
        {
            get { return this.Item.ItemData; }
        }

        public IItemGraphics ItemGraphics
        {
            get { return this.Item.ItemGraphics; }
        }

        public dynamic ItemSettings
        {
            get { return this.Item.ItemSettings; }
        }

        #endregion Item Components

        #region View Components

        public IView View
        {
            get { return this.Item.View; }
        }

        public dynamic ViewControl
        {
            get { return this.View.Control; }
        }

        public dynamic ViewSettings
        {
            get { return this.Item.ViewSettings; }
        }

        #endregion View Components
    }
}